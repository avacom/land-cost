using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GMap.NET.WindowsForms;
using LandCost.Entities;
using LandCost.Entities.Helpers;
using GMap.NET.MapProviders;
using GMap.NET;

namespace LandCost.Forms
{
    public partial class SelectPolygonForm : Form
    {
        LandPolygon curPol;
        Profile profile;

        GMapOverlay regions;

        List<LandPolygon> unbound;
        List<LandPolygon> bound;

        public SelectPolygonForm()
        {
            InitializeComponent();
            regions = new GMapOverlay("regions");

            GMapProvider.Language = LanguageType.Ukrainian;
            map.MapProvider = GMap.NET.MapProviders.YandexMapProviderUA.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;

            map.MinZoom = 3;
            map.MaxZoom = 17;
            map.Zoom = 13;

            map.Overlays.Add(regions);
        }

        public Profile CurrentProfile
        {
            get
            {
                return profile;
            }
            set
            {
                profile = value;
            }
        }

        public LandPolygon CurrentPolygon
        {
            get
            {
                return curPol;
            }
            set
            {
                curPol = value;
            }
        }

        private void DrawRegions()
        {
            regions.Polygons.Clear();
            if (profile != null)
            {
                if (profile.RegionMap != null)
                {
                    map.Position = profile.RegionMap.Center;
                }
                else
                {
                    map.SetPositionByKeywords("Ковель");
                }

                foreach (LandPolygon pol in profile.RegionMap.Polygons)
                {
                    GMapPolygon gpol = PolygonHelper.GMapPolygonByEntity(pol);
                    regions.Polygons.Add(gpol);
                }
            }
        }

        private void FormatPolygons()
        {
            if (profile != null)
            {
                unbound = profile.UnboundPolygons;
                bound = profile.BoundPolygons;

                foreach (LandPolygon pol in unbound)
                {
                    GMapPolygon gpol = PolygonHelper.FindGMapPolygon(pol, regions.Polygons);
                    if (!pol.Equals(curPol))
                    {
                        gpol.Stroke = PolygonHelper.GeneralPen;
                        gpol.Fill = PolygonHelper.ActiveBrush;
                    }
                    else
                    {
                        gpol.Stroke = PolygonHelper.GeneralPen;
                        gpol.Fill = PolygonHelper.CurrentBrush;
                    }
                }

                foreach (LandPolygon pol in bound)
                {
                    GMapPolygon gpol = PolygonHelper.FindGMapPolygon(pol, regions.Polygons);
                    if (!pol.Equals(curPol))
                    {
                        gpol.Stroke = PolygonHelper.GeneralPen;
                        gpol.Fill = PolygonHelper.InactiveBrush;
                    }
                    else
                    {
                        gpol.Stroke = PolygonHelper.GeneralPen;
                        gpol.Fill = PolygonHelper.CurrentBrush;
                    }
                }
            }
        }

        private void SelectPolygonForm_Load(object sender, EventArgs e)
        {
            DrawRegions();
            FormatPolygons();
        }

        private void map_OnPolygonEnter(GMapPolygon item)
        {
            item.Tag = item.Fill;
            item.Fill = PolygonHelper.SelectedBrush;
        }

        private void map_OnPolygonLeave(GMapPolygon item)
        {
            if (item.Tag != null)
            {
                item.Fill = item.Tag as Brush;
            }
        }

        private void map_OnPolygonClick(GMapPolygon item, MouseEventArgs e)
        {
            if (!item.Equals(curPol))
            {
                DialogResult res = DialogResult.Cancel;
                string sQuestion = "Прив'язати район до обраної території на карті?";
                LandPolygon lpol = PolygonHelper.FindPolygon(item, bound);
                if (lpol != null)
                {
                    LandRegion reg = null;
                    if (profile != null)
                    {
                        reg = profile.GetRegionByPolygon(lpol);
                        if (reg != null)
                        {
                            sQuestion = string.Format("Обрана територія уже прив'язана до району {0}. Ви впевнені, що хочете продовжити?", reg.Number);
                        }
                        else
                        {
                            sQuestion = "Обрана територія уже прив'язана до району. Ви впевнені, що хочете продовжити?";
                        }
                    }
                }
                else
                {
                    lpol = PolygonHelper.FindPolygon(item, profile.RegionMap.Polygons);
                }
                    
                res = MessageBox.Show(this, sQuestion, "Запитання", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    item.Tag = null;
                    curPol = lpol;
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
        }
    }
}
