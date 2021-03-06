﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LandCost.Database;
using LandCost.Forms;
using LandCost.Entities;
using GMap.NET.WindowsForms;
using GMap.NET.MapProviders;
using GMap.NET;
using LandCost.Entities.Helpers;
using GMap.NET.WindowsForms.Markers;
using LandCost.Licensing;
using System.Globalization;

namespace LandCost
{
    public partial class MainForm : Form, IActionForm
    {
        const string CONF_DB = "config.lcd";
        bool m_bLoaded = false;
        LandDB m_DB;

        ConfigurationForm m_ConfigForm;
        SplashScreen m_Splash;

        GMapOverlay regions;
        GMapOverlay markers;

        PointLatLng m_CurrentPoint;

        CertificationForm certForm;
        Certification2017Form certForm2017;
        ProgressForm m_ProgressForm;
        AboutForm aboutForm;

        LicenseChecker licChecker;

        public MainForm()
        {
            InitializeComponent();
            licChecker = new LicenseChecker("license.lcs");
            if (licChecker.CheckLicense())
            {
                m_bLoaded = false;
                m_Splash = new SplashScreen();
                m_Splash.StartPosition = FormStartPosition.CenterScreen;
                m_Splash.Show();
                certForm = new CertificationForm(this);
                certForm2017 = new Certification2017Form(this);
                regions = new GMapOverlay("regions");
                markers = new GMapOverlay("markers");

                GMapProvider.Language = LanguageType.Ukrainian;
                map.MapProvider = GMap.NET.MapProviders.YandexMapProviderUA.Instance;
                GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;

                map.MinZoom = 3;
                map.MaxZoom = 17;
                map.Zoom = 15;

                map.Overlays.Add(regions);
                map.Overlays.Add(markers);

                m_DB = new LandDB();
                m_DB.LoadConfiguration(CONF_DB);

                m_ConfigForm = new ConfigurationForm();
                m_ConfigForm.Config = m_DB.Config;

                m_ProgressForm = new ProgressForm();
                aboutForm = new AboutForm();
                aboutForm.License = licChecker.LicenseString;
                UpdateMenu();

                m_Splash.Close();
                m_bLoaded = true;
            }
        }

        private void ReloadRegionSelCtl()
        {
            if (m_DB != null &&
                m_DB.Config != null &&
                m_DB.Config.CurrentProfile != null)
            {
                regionSelCtl.RegionList = m_DB.Config.CurrentProfile.Regions;
                regionSelCtl.CurrentRegion = null;
                regionSelCtl.Visible = false;
            }
        }

        private void DrawRegions()
        {
            regions.Polygons.Clear();
            if (m_DB != null &&
                m_DB.Config != null &&
                m_DB.Config.CurrentProfile != null)
            {
                if (m_DB.Config.CurrentProfile.RegionMap != null)
                {
                    map.Position = m_DB.Config.CurrentProfile.RegionMap.Center;
                }
                else
                {
                    map.SetPositionByKeywords("Ковель");
                }

                List<LandPolygon> pols = m_DB.Config.CurrentProfile.RegionPolygons;
                pols.Sort(delegate(LandPolygon x, LandPolygon y)
                {
                    return x.Area.CompareTo(y.Area);
                });

                foreach (LandPolygon pol in pols)
                {
                    GMapPolygon gpol = PolygonHelper.GMapPolygonByEntity(pol);
                    gpol.Stroke = PolygonHelper.GeneralPen;
                    gpol.Fill = PolygonHelper.MainBrush;
                    regions.Polygons.Add(gpol);
                }
            }
            else
            {
                map.SetPositionByKeywords("Ковель");
            }
        }

        void ShowConfigForm()
        {
            m_ConfigForm.ShowDialog();
            if (m_DB.Config.Changed)
            {
                UpdateMenu();
                AsyncSave();
            }
        }

        private void profilesMenu_Click(object sender, EventArgs e)
        {
            ShowConfigForm();
        }

        private void AsyncSave()
        {
            if (m_DB.Config.Changed)
            {
                BackgroundWorker bg = new BackgroundWorker();
                bg.DoWork += new DoWorkEventHandler(SaveConfig);
                bg.RunWorkerCompleted += new RunWorkerCompletedEventHandler(SaveConfigCompleted);

                // Start the worker.
                bg.RunWorkerAsync();

                if (m_bLoaded)
                {
                    // Display the progress form.
                    m_ProgressForm.StartPosition = FormStartPosition.CenterParent;
                    m_ProgressForm.Text = "Зберігаю...";
                    m_ProgressForm.ShowDialog(this);
                }
            }
        }

        private void SaveConfig(object sender, DoWorkEventArgs e)
        {
            m_DB.SaveConfiguration(CONF_DB);
        }

        public void SaveConfigCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            object result = e.Result;

            // Close the loading form.
            m_ProgressForm.Hide();
        }

        private void UpdateMenu()
        {
            // Remove obsolete items
            if (curProfileMenu.HasDropDownItems)
            {
                int len = curProfileMenu.DropDownItems.Count;

                List<ToolStripItem> toDelete = new List<ToolStripItem>();
                foreach (ToolStripItem item in curProfileMenu.DropDownItems)
                {
                    if (!(m_DB.Config.Profiles.Contains(item.Tag as Profile)))
                    {
                        toDelete.Add(item);
                    }
                }

                foreach (ToolStripItem item in toDelete)
                {
                    curProfileMenu.DropDownItems.Remove(item);
                }
            }

            // Add new items and update the texts of existing
            if (m_DB != null &&
                m_DB.Config != null &&
                m_DB.Config.Profiles != null)
            {
                foreach (Profile prof in m_DB.Config.Profiles)
                {
                    ToolStripItem item = ToolStripItemByProfile(prof);
                    if (item == null)
                    {
                        ToolStripRadioButtonMenuItem newItem = new ToolStripRadioButtonMenuItem();
                        newItem.Click += new EventHandler(p_Click);
                        newItem.Text = prof.Name;
                        newItem.Tag = prof;
                        curProfileMenu.DropDownItems.Add(newItem);
                    }
                    else
                    {
                        if (item.Text != prof.Name)
                        {
                            item.Text = prof.Name;
                        }
                    }
                }
            }

            // Find out the working profile
            if (curProfileMenu.HasDropDownItems)
            {
                curProfileMenu.Enabled = true;
                ToolStripRadioButtonMenuItem sel = SelectedProfileMenu();
                if (m_DB != null &&
                    m_DB.Config != null &&
                    m_DB.Config.CurrentProfile != null)
                {
                    UseCurrentProfile();
                }

                if (m_DB != null &&
                    m_DB.Config != null &&
                    m_DB.Config.CurrentProfile == null)
                {
                    if (sel != null)
                    {
                        SetCurrentProfile(sel.Tag as Profile);
                    }
                    else
                    {
                        SelectFirstProfile();
                    }
                }
            }
            else
            {
                SetCurrentProfile(null);
                curProfileMenu.Enabled = false;
            }
        }

        private void SelectFirstProfile()
        {
            if (curProfileMenu.HasDropDownItems)
            {
                ToolStripRadioButtonMenuItem first = curProfileMenu.DropDownItems[0] as ToolStripRadioButtonMenuItem;
                first.Checked = true;
                SetCurrentProfile(first.Tag as Profile);
            }
        }

        private ToolStripRadioButtonMenuItem SelectedProfileMenu()
        {
            ToolStripRadioButtonMenuItem ret = null;
            if (curProfileMenu.HasDropDownItems)
            {
                foreach (ToolStripRadioButtonMenuItem item in curProfileMenu.DropDownItems)
                {
                    if (item.Checked == true)
                    {
                        ret = item;
                        break;
                    }
                }
            }
            return ret;
        }
        private void UseCurrentProfile()
        {
            ToolStripRadioButtonMenuItem item = ToolStripItemByProfile(m_DB.Config.CurrentProfile) as ToolStripRadioButtonMenuItem;
            if (item != null)
            {
                item.Checked = true;
            }
            else
            {
                m_DB.Config.CurrentProfile = null;
            }
            AsyncReloadWorkingArea();
        }

        private void AsyncReloadWorkingArea()
        {
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += new DoWorkEventHandler(ReloadWorkingArea);
            bg.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ReloadWorkingAreaCompleted);

            // Start the worker.
            bg.RunWorkerAsync();

            if (m_bLoaded)
            {
                // Display the progress form.
                m_ProgressForm.StartPosition = FormStartPosition.CenterParent;
                m_ProgressForm.Text = "Завантажую...";
                m_ProgressForm.ShowDialog(this);
            }
        }

        private void SetCurrentProfile(Profile profile)
        {
            if (m_DB != null &&
                m_DB.Config != null)
            {
                m_DB.Config.CurrentProfile = profile;
                AsyncSave();
                AsyncReloadWorkingArea();
            }
        }

        ToolStripItem ToolStripItemByProfile(Profile profile)
        {
            ToolStripItem ret = null;
            if (curProfileMenu.HasDropDownItems)
            {
                foreach (ToolStripItem item in curProfileMenu.DropDownItems)
                {
                    if (item.Tag == profile)
                    {
                        ret = item;
                        break;
                    }
                }
            }
            return ret;
        }

        void p_Click(object sender, EventArgs e)
        {
            ToolStripItem item = sender as ToolStripItem;
            SetCurrentProfile(item.Tag as Profile);
        }


        private void ReloadWorkingArea(object sender, DoWorkEventArgs e)
        {

            SetCtlEnabled(evalBtn, false);

            if (map.InvokeRequired)
            {
                map.Invoke(new MethodInvoker(delegate(){
                    ShowInfo(null);
                    DrawRegions();
                    ReloadRegionSelCtl();
                    addressBox.Text = string.Empty;
                    m_CurrentPoint = new PointLatLng();
                    markers.Markers.Clear();
                }));
            }
            else
            {
                ShowInfo(null);
                DrawRegions();
                ReloadRegionSelCtl();
                addressBox.Text = string.Empty;
                m_CurrentPoint = new PointLatLng();
                markers.Markers.Clear();
            }
        }

        public void ReloadWorkingAreaCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            object result = e.Result;
            // Close the loading form.
            m_ProgressForm.Hide();
        }

        private void map_OnPolygonEnter(GMapPolygon item)
        {
            item.Tag = item.Fill;
            item.Fill = PolygonHelper.SelectedBrush;

            List<LandPolygon> pols = m_DB.Config.CurrentProfile.RegionPolygons;
            LandRegion reg = m_DB.Config.CurrentProfile.GetRegionByPolygon(item);
            ShowInfo(reg);
        }

        private void map_OnPolygonLeave(GMapPolygon item)
        {
            if (item.Tag != null)
            {
                item.Fill = item.Tag as Brush;
            }
        }

        void ShowInfo(LandRegion region)
        {
            if (region != null)
            {
                Area a = region.ParentArea;
                areaBox.Text = a == null ? "(немає)" : a.Number.ToString();
                regionBox.Text = region.Number.ToString();
                km2Box.Text = a == null ? "0" : a.KM2.ToString();
                priceBox.Text = region.Price.ToString();
            }
            else
            {
                areaBox.Text = string.Empty;
                regionBox.Text = string.Empty;
                km2Box.Text = string.Empty;
                priceBox.Text = string.Empty;
            }
        }

        private void map_OnPolygonClick(GMapPolygon item, MouseEventArgs e)
        {
            if (m_DB != null &&
                m_DB.Config != null &&
                m_DB.Config.CurrentProfile != null)
            {
                markers.Markers.Clear();
                // Find the address
                double lat = map.FromLocalToLatLng(e.X, e.Y).Lat;
                double lng = map.FromLocalToLatLng(e.X, e.Y).Lng;

                PointLatLng coord = new PointLatLng(lat, lng);

                m_CurrentPoint = coord;

                Placemark? pl = null;
                GeoCoderStatusCode st = GeoCoderStatusCode.G_GEO_SUCCESS;
                pl = GMapProviders.GoogleMap.GetPlacemark(coord, out st);
                if (st == GeoCoderStatusCode.G_GEO_SUCCESS && pl != null)
                {
                    if (pl.HasValue)
                    {
                        addressBox.Text = GetAddressString(pl);
                    }
                }

                LandRegion selRegion = m_DB.Config.CurrentProfile.GetRegionByPolygon(item);
                regionSelCtl.CurrentRegion = selRegion;
                regionSelCtl.Visible = true;
            }
        }

        private void regionSelCtl_SelectionMade(object sender, EventArgs e)
        {
            if (regionSelCtl.CurrentRegion != null &&
                regionSelCtl.CurrentFunctionalUsageCoefficients != null)
            {
                SetCtlEnabled(evalBtn, true);
            }
            else
            {
                SetCtlEnabled(evalBtn, false);
            }
        }

        void SetCtlEnabled(Control ctl, bool enabled)
        {
            if (ctl.InvokeRequired)
            {
                ctl.Invoke(new MethodInvoker(delegate()
                {
                    ctl.Enabled = enabled;
                }));
            }
            else
            {
                ctl.Enabled = enabled;
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            addressBox.Text = string.Empty;
            m_CurrentPoint = new PointLatLng();
            regionSelCtl.CurrentRegion = null;
            regionSelCtl.Visible = false;
            markers.Markers.Clear();
        }

        private void FindAddress()
        {
            if (m_DB != null &&
                m_DB.Config != null &&
                m_DB.Config.CurrentProfile != null &&
                m_DB.Config.CurrentProfile.RegionMap != null)
            {
                GeoCoderStatusCode st = GeoCoderStatusCode.G_GEO_SUCCESS;
                Placemark? pl = null;
                var prov = GMapProviders.GoogleTerrainMap;
                var point = prov.GetPoint(addressBox.Text + " Ковель", out st);

                if (point != null)
                {
                    pl = prov.GetPlacemark((PointLatLng)point, out st);
                    //List<Placemark> pls = new List<Placemark>();
                    //prov.GetPlacemarks((PointLatLng)point, out pls);
                }
                
                bool bFound = false;
                if (pl != null)
                {
                    if (m_DB.Config.CurrentProfile.RegionMap.IsInside((PointLatLng)point))
                    {
                        addressBox.Text = GetAddressString(pl);
                        SetFoundLocation((PointLatLng)point);
                        bFound = true;
                    }
                }

                if (!bFound)
                {
                    MessageBox.Show(this, "Не можу знайти вказану адресу в межах завантаженої карти", "Невдача :(", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(this, "Не можу здійснити пошук! Обраний профіль не містить карти!", "Невдача :(", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SetFoundLocation(PointLatLng point)
        {
            map.Position = point;
            GMarkerGoogle marker = new GMarkerGoogle(point, GMarkerGoogleType.green);
            markers.Markers.Clear();
            markers.Markers.Add(marker);
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            FindAddress();
        }

        private string GetAddressString(Placemark? pl)
        {
            string s = string.Empty;
            if (pl != null)
            {
                s = pl.Value.Address;
            }
            //s = string.IsNullOrEmpty(pl.Value.LocalityName) ? s : pl.Value.LocalityName;

            //if (!string.IsNullOrEmpty(pl.Value.ThoroughfareName))
            //{
            //    if (!string.IsNullOrEmpty(s))
            //    {
            //        s += string.Format(", {0}", pl.Value.ThoroughfareName);
            //    }
            //    else
            //    {
            //        s += pl.Value.ThoroughfareName;
            //    }
            //}

            //if (!string.IsNullOrEmpty(pl.Value.HouseNo))
            //{
            //    if (!string.IsNullOrEmpty(s))
            //    {
            //        s += string.Format(", {0}", pl.Value.HouseNo);
            //    }
            //    else
            //    {
            //        s += pl.Value.HouseNo;
            //    }
            //}

            return s;
        }

        private void addressBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FindAddress();
            }
        }

        private void evalBtn_Click(object sender, EventArgs e)
        {
            certForm2017 = new Certification2017Form(this);
            certForm2017.StartPosition = FormStartPosition.CenterScreen;
            certForm2017.SetProfile(m_DB.Config.CurrentProfile);
            certForm2017.SetValues(regionSelCtl.CurrentRegion, regionSelCtl.CurrentFunctionalUsageCoefficients, addressBox.Text);
            certForm2017.ShowDialog();
        }

        private void exitMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openMenu_Click(object sender, EventArgs e)
        {
            if (openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                OpenFile(openDialog.FileName);
            }
        }

        public void OpenFile(string filename)
        {
            try
            {
                // Old
                if (filename.EndsWith(".lcc", true, CultureInfo.InvariantCulture))
                {
                    certForm.StartPosition = FormStartPosition.CenterScreen;
                    certForm.SetProfile(m_DB.Config.CurrentProfile);
                    certForm.LoadFromFile(filename);
                    certForm.ShowDialog();
                }
                else if (filename.EndsWith(".lce", true, CultureInfo.InvariantCulture))
                {
                    certForm2017.StartPosition = FormStartPosition.CenterScreen;
                    certForm2017.SetProfile(m_DB.Config.CurrentProfile);
                    certForm2017.LoadFromFile(filename);
                    certForm2017.ShowDialog();
                }
            }
            catch
            {
                MessageBox.Show(this, "Не можу завантажити витяг / довідку!", "Горечко :(", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void aboutMenu_Click(object sender, EventArgs e)
        {
            aboutForm.ShowDialog();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (!m_bLoaded)
            {
                this.Close();
            }
        }
    }
}
