using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LandCost.Entities;
using System.IO;
using LandCost.Entities.Helpers;

namespace LandCost.Forms
{
    public partial class ProfileControl : UserControl, IEditor<Profile>, IModificationAnnouncer
    {
        ProgressForm m_ProgressForm;

        Profile m_Profile;
        List<Profile> m_aList;
        object m_ParentObject;

        EditedListView<Document> documentList;
        EditedListView<FunctionalUsage> functionalUsageList;
        EditedListView<LocalCoefficient> localCoefList;
        EditedListView<Area> areaList;
        EditedListView<LandRegion> regionList;

        public event EventHandler Modified;

        public void OnModified(object sender, EventArgs e)
        {
            if (Modified != null)
            {
                Modified(sender, e);
            }
        }

        public ProfileControl()
        {
            m_ParentObject = null;
            InitializeComponent();
            m_ProgressForm = new ProgressForm();
            //InitializeInternal();
            //InitializeControls();
        }

        public Profile NewItem
        {
            get { return new Profile(NewName()); }
        }

        public object Entity
        {
            get { return m_Profile; }
            set
            {
                m_Profile = value as Profile;
                InitializeInternal();
                InitializeControls();
            }
        }

        private void InitializeInternal()
        {
            if (m_Profile != null)
            {
                profileNameEdit.Text = m_Profile.Name;
                agencyNameEdit.Text = m_Profile.AgencyName;
                agencyAddressEdit.Text = m_Profile.AgencyAddress;
                indexCoefEdit.Text = m_Profile.IndexCoefficient.ToString();

                chiefBox.Lines = m_Profile.Chiefs.ToArray();
                executorBox.Lines = m_Profile.Executors.ToArray();
            }
            this.Text = string.Format("Профіль {0}", m_Profile.Name);
        }

        public List<Profile> CheckList
        {
            get
            {
                return m_aList;
            }
            set
            {
                m_aList = value;
            }
        }

        public object ParentObject
        {
            get
            {
                return m_ParentObject;
            }
            set
            {
                m_ParentObject = value;
            }
        }

        public string NewName()
        {
            string name = string.Empty;
            int nMax = 0;
            if (m_aList != null)
            {
                foreach (Profile item in m_aList)
                {
                    string[] split = item.Name.Split('_');
                    if (split.Length == 2)
                    {
                        if (split[0] == "Новий")
                        {
                            int n = 0;
                            int.TryParse(split[1], out n);
                            if (n > nMax)
                            {
                                nMax = n;
                            }
                        }
                    }
                }
            }
            name = string.Format("Новий_{0}", nMax + 1);
            return name;
        }

        private void InitDocumetList()
        {
            // 
            // documentList
            // 
            if (documentList != null && !documentList.IsDisposed)
            {
                documentList.Dispose();
            }
            this.documentList = new LandCost.Forms.EditedListView<Document>();
            this.documentList.AddButtonText = "Додати документ";
            this.documentList.DeleteButtonText = "Видалити документ";
            this.documentList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentList.EditButtonText = "Редагувати документ";
            this.documentList.Name = "documentList";
            this.documentList.TabIndex = 0;
            this.documentTab.Controls.Add(this.documentList);
            ColumnHeader doc_nameHeader = new ColumnHeader();
            doc_nameHeader.Width = 250;
            doc_nameHeader.Text = "Назва документа";
            doc_nameHeader.Tag = "Name";

            //ColumnHeader doc_maskHeader = new ColumnHeader();
            //doc_maskHeader.Width = -2;
            //doc_maskHeader.Text = "Формат";
            //doc_maskHeader.Tag = "Mask";

            this.documentList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            doc_nameHeader
            //,doc_maskHeader
            });

            this.documentList.FormType = typeof(DocumentForm);
            this.documentList.EditorEvent = EditEvent.DoubleClick;
            this.documentList.ActionType = ActionType.AddEditForm;
            this.documentList.ItemList = m_Profile.Documents;
            this.documentList.ParentObject = m_Profile;
            this.documentList.ListChanged += new EventHandler(documentList_ListChanged);
        }

        void documentList_ListChanged(object sender, EventArgs e)
        {
            OnModified(this, null);
        }

        private void InitFunctionalUsageList()
        {
            ///
            /// functionalUsageList
            /// 
            if (functionalUsageList != null && !functionalUsageList.IsDisposed)
            {
                functionalUsageList.Dispose();
            }
            this.functionalUsageList = new LandCost.Forms.EditedListView<FunctionalUsage>();
            this.functionalUsageList.AddButtonText = "Додати функціональне призначення";
            this.functionalUsageList.DeleteButtonText = "Видалити функціональне призначення";
            this.functionalUsageList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.functionalUsageList.EditButtonText = "Редагувати функціональне призначення";
            this.functionalUsageList.Name = "functionalUsageList";
            this.functionalUsageList.TabIndex = 0;
            this.usageTab.Controls.Add(this.functionalUsageList);
            ColumnHeader fus_nameHeader = new ColumnHeader();
            fus_nameHeader.Width = 350;
            fus_nameHeader.Text = "Назва функціонального призначення";
            fus_nameHeader.Tag = "Name";

            ColumnHeader fus_weightHeader = new ColumnHeader();
            fus_weightHeader.Width = -2;
            fus_weightHeader.Text = "Коефіцієнт";
            fus_weightHeader.Tag = "Weight";

            this.functionalUsageList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            fus_nameHeader,
            fus_weightHeader});

            this.functionalUsageList.FormType = typeof(FunctionalUsageForm);
            this.functionalUsageList.EditorEvent = EditEvent.DoubleClick;
            this.functionalUsageList.ActionType = ActionType.AddEditForm;
            this.functionalUsageList.ItemList = m_Profile.FunctionalUsages;
            this.functionalUsageList.ParentObject = m_Profile;
            this.functionalUsageList.ListChanged += new EventHandler(functionalUsageList_ListChanged);
        }

        void functionalUsageList_ListChanged(object sender, EventArgs e)
        {
            OnModified(this, null);
        }

        private void InitLocalCoefList()
        {
            ///
            /// localCoefList
            /// 
            if (localCoefList != null && !localCoefList.IsDisposed)
            {
                localCoefList.Dispose();
            }
            this.localCoefList = new LandCost.Forms.EditedListView<LocalCoefficient>();
            this.localCoefList.AddButtonText = "Додати локальний коефіцієнт";
            this.localCoefList.DeleteButtonText = "Видалити локальний коефіцієнт";
            this.localCoefList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.localCoefList.EditButtonText = "Редагувати локальний коефіцієнт";
            this.localCoefList.Name = "localCoefList";
            this.localCoefList.TabIndex = 0;
            this.coefficientsTab.Controls.Add(this.localCoefList);
            ColumnHeader lco_nameHeader = new ColumnHeader();
            lco_nameHeader.Width = 350;
            lco_nameHeader.Text = "Назва локального коефіцієнта";
            lco_nameHeader.Tag = "Name";

            this.localCoefList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            lco_nameHeader});

            this.localCoefList.FormType = typeof(LocalCoefficientForm);
            this.localCoefList.EditorEvent = EditEvent.DoubleClick;
            this.localCoefList.ActionType = ActionType.AddEditForm;
            this.localCoefList.ItemList = m_Profile.LocalCoefficients;
            this.localCoefList.ParentObject = m_Profile;
            this.localCoefList.ListChanged += new EventHandler(localCoefList_ListChanged);
        }

        void localCoefList_ListChanged(object sender, EventArgs e)
        {
            OnModified(this, null);
        }

        private void InitAreaList()
        {
            ///
            /// areaList
            /// 
            if (areaList != null && !areaList.IsDisposed)
            {
                areaList.Dispose();
            }
            this.areaList = new LandCost.Forms.EditedListView<Area>();
            this.areaList.AddButtonText = "Додати економіко-планувальну зону";
            this.areaList.DeleteButtonText = "Видалити економіко-планувальну зону";
            this.areaList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.areaList.EditButtonText = "Редагувати економіко-планувальну зону";
            this.areaList.Name = "areaList";
            this.areaList.TabIndex = 0;
            this.areaTab.Controls.Add(this.areaList);
            ColumnHeader area_numberHeader = new ColumnHeader();
            area_numberHeader.Width = 100;
            area_numberHeader.Text = "Номер зони";
            area_numberHeader.Tag = "Number";

            ColumnHeader area_coefHeader = new ColumnHeader();
            area_coefHeader.Width = 200;
            area_coefHeader.Text = "Коефіцієнт Км2";
            area_coefHeader.Tag = "KM2";

            ColumnHeader area_priceHeader = new ColumnHeader();
            area_priceHeader.Width = -2;
            area_priceHeader.Text = "Середня вартість земельної ділянки, грн/м2";
            area_priceHeader.Tag = "Price";

            this.areaList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            area_numberHeader,
            area_coefHeader,
            area_priceHeader});

            this.areaList.FormType = typeof(AreaForm);
            this.areaList.EditorEvent = EditEvent.DoubleClick;
            this.areaList.ActionType = ActionType.AddEditForm;
            this.areaList.ItemList = m_Profile.Areas;
            this.areaList.ParentObject = m_Profile;
            this.areaList.ListChanged += new EventHandler(areaList_ListChanged);
        }

        void areaList_ListChanged(object sender, EventArgs e)
        {
            InitRegionList();
            OnModified(this, null);
        }

        private void InitRegionList()
        {
            ///
            /// regionList
            /// 
            if (regionList != null && !regionList.IsDisposed)
            {
                regionList.Dispose();
            }
            this.regionList = new LandCost.Forms.EditedListView<LandRegion>();
            this.regionList.AddButtonText = "Додати район";
            this.regionList.DeleteButtonText = "Видалити район";
            this.regionList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.regionList.EditButtonText = "Редагувати район";
            this.regionList.Name = "regionList";
            this.regionList.TabIndex = 0;
            //if (m_Profile != null &&
            //    m_Profile.RegionMap != null)
            //{
            //    this.regionList.Enabled = true;
            //}
            //else
            //{
            //    this.regionList.Enabled = false;
            //}
            this.regionPanel.Controls.Add(this.regionList);
            ColumnHeader reg_numberHeader = new ColumnHeader();
            reg_numberHeader.Width = 100;
            reg_numberHeader.Text = "Номер району";
            reg_numberHeader.Tag = "Number";

            ColumnHeader reg_areaHeader = new ColumnHeader();
            reg_areaHeader.Width = 200;
            reg_areaHeader.Text = "Номер економіко-планувальної зони";
            reg_areaHeader.Tag = "ParentArea";

            ColumnHeader reg_priceHeader = new ColumnHeader();
            reg_priceHeader.Width = -2;
            reg_priceHeader.Text = "Середня вартість ділянки, грн/м2";
            reg_priceHeader.Tag = "Price";

            this.regionList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            reg_numberHeader,
            reg_areaHeader,
            reg_priceHeader});

            this.regionList.FormType = typeof(RegionForm);
            this.regionList.EditorEvent = EditEvent.DoubleClick;
            this.regionList.ActionType = ActionType.AddEditForm;
            this.regionList.ItemList = m_Profile.Regions;
            this.regionList.ParentObject = m_Profile;
            this.regionList.ListChanged += new EventHandler(regionList_ListChanged);
        }

        void regionList_ListChanged(object sender, EventArgs e)
        {
            SetMapData();
        }

        private void InitializeControls()
        {
            InitDocumetList();
            InitFunctionalUsageList();
            InitLocalCoefList();
            InitAreaList();
            InitRegionList();
            
            if (m_Profile != null &&
                m_Profile.RegionMap != null)
            {
                SetMapData();
            }
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            if (m_Profile != null && ((m_Profile.RegionMap != null && MessageBox.Show(this, "Ви впевнені, що хочете завантажити нову карту? Усі прив'язки районів до попередньої карти будуть видалені", "Попередження", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) ||
                m_Profile.RegionMap == null))
            {
                if (loadMapDlg.ShowDialog() == DialogResult.OK)
                {
                    m_Profile.RegionMap = new Map(loadMapDlg.FileName);
                    if (!m_Profile.RegionMap.Load())
                    {
                        mapFileLabel.Text = "немає";
                        mapRegLabel.Text = "немає";
                        mapUnboundRegLabel.Text = "немає";
                        MessageBox.Show(this, "Неможливо завантажити карту!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (m_Profile.Regions != null)
                        {
                            foreach (LandRegion reg in m_Profile.Regions)
                            {
                                reg.Polygon = null;
                            }
                        }

                        SetMapData();
                    }
                }
            }
        }

        private void SetMapData()
        {
            mapFileLabel.Text = Path.GetFileName(m_Profile.RegionMap.FileName);
            mapRegLabel.Text = m_Profile.RegionMap.Polygons.Count.ToString();
            mapUnboundRegLabel.Text = m_Profile.UnboundPolygons.Count.ToString();
            //regionList.Enabled = true;
            OnModified(this, null);
        }


        private void profileNameEdit_Validating(object sender, CancelEventArgs e)
        {
            if (m_Profile != null)
            {
                if (ValidateName())
                {
                    m_Profile.Name = profileNameEdit.Text;
                    this.OnValidated(null);
                    OnModified(this, null);
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void agencyNameEdit_Validating(object sender, CancelEventArgs e)
        {
            if (m_Profile != null)
            {
                m_Profile.AgencyName = agencyNameEdit.Text;
                this.OnValidated(null);
                OnModified(this, null);
            }
        }

        private void agencyAddressEdit_Validating(object sender, CancelEventArgs e)
        {
            if (m_Profile != null)
            {
                m_Profile.AgencyAddress = agencyAddressEdit.Text;
                this.OnValidated(null);
                OnModified(this, null);
            }
        }

        private void indexCoefEdit_Validating(object sender, CancelEventArgs e)
        {
            if (m_Profile != null)
            {
                m_Profile.IndexCoefficient = indexCoefEdit.Value;
                this.OnValidated(null);
                OnModified(this, null);
            }
        }

        #region Validations
        private bool ValidateName()
        {
            bool bRet = true;
            // Empty name
            if (string.IsNullOrEmpty(profileNameEdit.Text.Trim()))
            {
                bRet = false;
            }

            if (!bRet)
            {
                MessageBox.Show(this, "Назва профілю не може бути порожньою", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Duplicate name
            if (bRet && m_aList != null)
            {
                if (m_Profile == null)
                {
                    foreach (Profile p in m_aList)
                    {
                        if (p.Name == profileNameEdit.Text)
                        {
                            bRet = false;
                            break;
                        }
                    }
                }
                else
                {
                    foreach (Profile p in m_aList)
                    {
                        if (!p.Equals(m_Profile) && p.Name == profileNameEdit.Text)
                        {
                            bRet = false;
                            break;
                        }
                    }
                }

                if (!bRet)
                {
                    MessageBox.Show(this, "Об'єкт зі вказаними атрибутами вже існує", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return bRet;
        }
        #endregion Validations


        private void executorBox_Validating(object sender, CancelEventArgs e)
        {
            if (m_Profile != null)
            {
                m_Profile.Executors.Clear();
                m_Profile.Executors.AddRange(executorBox.Lines);
                this.OnValidated(null);
                OnModified(this, null);
            }
        }

        private void chiefBox_Validating(object sender, CancelEventArgs e)
        {
            if (m_Profile != null)
            {
                m_Profile.Chiefs.Clear();
                m_Profile.Chiefs.AddRange(chiefBox.Lines);
                this.OnValidated(null);
                OnModified(this, null);
            }
        }

        private void loadXlsBtn_Click(object sender, EventArgs e)
        {
            LoadXlsAsync();
        }

        private void LoadXlsAsync()
        {
            if (xlsDialog.ShowDialog() == DialogResult.OK)
            {
                BackgroundWorker bg = new BackgroundWorker();
                bg.DoWork += new DoWorkEventHandler(LoadXls);
                bg.RunWorkerCompleted += new RunWorkerCompletedEventHandler(LoadXlsCompleted);

                // Start the worker.
                bg.RunWorkerAsync();

                // Display the progress form.
                m_ProgressForm.StartPosition = FormStartPosition.CenterParent;
                m_ProgressForm.Text = "Завантажую...";
                m_ProgressForm.ShowDialog(this);
            }
            this.Entity = Entity;
            OnModified(this, null);
        }

        private void LoadXls(object sender, DoWorkEventArgs e)
        {
            ((Profile)Entity).LoadXls(xlsDialog.FileName);
        }

        public void LoadXlsCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            object result = e.Result;

            // Close the loading form.
            m_ProgressForm.Hide();
        }

        private void ProfileControl_Validated(object sender, EventArgs e)
        {
            
        }
       
    }
}
