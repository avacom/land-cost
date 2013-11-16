using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LandCost.Entities;
using GMap.NET.WindowsForms;

namespace LandCost.Forms
{
    public partial class RegionForm : Form, IEditor<LandRegion>
    {
        ToolTip polyTT;

        LandRegion m_Object;
        List<LandRegion> m_aList;

        List<FunctionalUsageCoefficients> m_aUsageCoefs;
        LandPolygon polygon;

        EditedListView<FunctionalUsageCoefficients> usageCoefsList;
        CoefficientValueSetControl coefsCtl;

        SelectPolygonForm polForm;

        object m_ParentObject;

        public RegionForm()
        {
            InitializeComponent();
            polyTT = new ToolTip();
            polyTT.SetToolTip(statusLbl, "Не встановлено!");
            coefsCtl = new CoefficientValueSetControl();
            coefsCtl.Dock = DockStyle.Fill;
            coefsCtl.ReadOnly = false;

            polForm = new SelectPolygonForm();
        }

        void InitAreaBox()
        {
            areaBox.Items.Clear();
            areaBox.Items.Add("(немає)");

            if (m_ParentObject != null &&
                m_ParentObject is Profile)
            {
                Profile prof = m_ParentObject as Profile;
                if (prof.Areas != null &&
                    prof.Areas.Count > 0)
                {
                    areaBox.BeginUpdate();
                    foreach (Area area in prof.Areas)
                    {
                        areaBox.Items.Add(area);
                    }
                    areaBox.EndUpdate();
                }
            }
        }

        void InitUsageCoefsList()
        {
            ///
            /// usageCoefsList
            /// 
            if (usageCoefsList != null && !usageCoefsList.IsDisposed)
            {
                usageCoefsList.Dispose();
            }
            usageCoefsList = new LandCost.Forms.EditedListView<FunctionalUsageCoefficients>();
            usageCoefsList.SelectionChanged += new EventHandler(usageCoefsList_SelectionChanged);
            usageCoefsList.ListChanged += new EventHandler(usageCoefsList_ListChanged);
            usageCoefsList.AddButtonText = "Додати значення локальних коефіцієнтів для функціонального призначення";
            usageCoefsList.DeleteButtonText = "Видалити значення локальних коефіцієнтів для функціонального призначення";
            usageCoefsList.Dock = System.Windows.Forms.DockStyle.Fill;
            usageCoefsList.EditButtonText = "Редагувати значення локальних коефіцієнтів для функціонального призначення";
            usageCoefsList.Name = "usageCoefsList";
            usageCoefsList.TabIndex = 0;
            usagePanel.Controls.Add(usageCoefsList);

            ColumnHeader nameHeader = new ColumnHeader();
            nameHeader.Width = -2;
            nameHeader.Text = "Функціональне призначення";
            nameHeader.Tag = "Usage";

            usageCoefsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            nameHeader});

            usageCoefsList.FormType = typeof(FunctionalUsageCoefficientsForm);
            usageCoefsList.EditorEvent = EditEvent.DoubleClick;
            usageCoefsList.ActionType = ActionType.AddEditForm;
            usageCoefsList.ItemList = m_aUsageCoefs;
            usageCoefsList.ParentObject = m_ParentObject;
        }

        void usageCoefsList_ListChanged(object sender, EventArgs e)
        {
            ReinitCoefsControl();
        }

        void usageCoefsList_SelectionChanged(object sender, EventArgs e)
        {
            ReinitCoefsControl();
        }

        void ReinitCoefsControl()
        {
            coefPanel.Controls.Clear();
            if (usageCoefsList != null)
            {
                List<FunctionalUsageCoefficients> selected = usageCoefsList.SelectedObjects;
                if (selected != null &&
                    selected.Count == 1)
                {
                    coefsCtl.List = selected[0].LocalCoefficientValues;
                    coefPanel.Controls.Add(coefsCtl);
                }
            }
        }

        void SetValues()
        {
            if (m_Object != null)
            {
                if (areaBox.Items.Count > 1)
                {
                    if (m_Object.ParentArea != null)
                    {
                        areaBox.SelectedItem = m_Object.ParentArea;
                        priceBox.Text = m_Object.Price.ToString();
                    }
                    else
                    {
                        areaBox.SelectedIndex = 0;
                        priceBox.Text = "0";
                    }
                }
                else
                {
                    areaBox.SelectedIndex = 0;
                    priceBox.Text = "0";
                }
                numberBox.Text = m_Object.Number.ToString();
                m_aUsageCoefs = m_Object.FunctionalUsagesCoefficients;
                polygon = m_Object.Polygon;
                UpdateRegionStatusLbl();
                this.Text = string.Format("Район {0}", m_Object.Number);
            }
            else
            {
                if (areaBox.Items.Count > 0)
                {
                    areaBox.SelectedIndex = 0;
                }
                priceBox.Text = "0";
                numberBox.Text = "";

                m_aUsageCoefs = new List<FunctionalUsageCoefficients>();
                polygon = null;
                statusLbl.Image = LandCost.Forms.Properties.Resources.error;
                polyTT.SetToolTip(statusLbl, "Не встановлено!");

                this.Text = "Новий район";
            }
        }

        private void UpdateRegionStatusLbl()
        {
            if (polygon != null)
            {
                statusLbl.Image = LandCost.Forms.Properties.Resources.ok;
                polyTT.SetToolTip(statusLbl, "Встановлено!");
            }
            else
            {
                statusLbl.Image = LandCost.Forms.Properties.Resources.error;
                polyTT.SetToolTip(statusLbl, "Не встановлено!");
            }
        }

        public object Entity
        {
            get
            {
                return m_Object;
            }
            set
            {
                m_Object = value as LandRegion;
            }
        }

        public List<LandRegion> CheckList
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

        public LandRegion NewItem
        {
            get { return null; }
        }

        public bool ValidateValues()
        {
            bool bRet = true;
            // Empty number
            if (string.IsNullOrEmpty(numberBox.Text.Trim()))
            {
                bRet = false;
            }

            if (!bRet)
            {
                MessageBox.Show(this, "Не заданий номер району!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Incorrect number format
            if (bRet)
            {
                int nValue = 0;
                if (!int.TryParse(numberBox.Text, out nValue))
                {
                    bRet = false;
                }

                if (!bRet)
                {
                    MessageBox.Show(this, "Некоректний формат номера району! Задайте, будь ласка, ціле число", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Duplicate number
            if (bRet && m_aList != null)
            {
                if (m_Object == null)
                {
                    foreach (LandRegion obj in m_aList)
                    {
                        if (obj.Number.ToString() == numberBox.Text)
                        {
                            bRet = false;
                            break;
                        }
                    }
                }
                else
                {
                    foreach (LandRegion obj in m_aList)
                    {
                        if (!obj.Equals(m_Object) && obj.Number.ToString() == numberBox.Text)
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

        private void RegionForm_Load(object sender, EventArgs e)
        {
            InitAreaBox();
            SetValues();
            InitUsageCoefsList();
        }

        private void areaBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (areaBox.SelectedIndex == 0)
            {
                priceBox.Text = "0";
            }
            else
            {
                try
                {
                    priceBox.Text = (areaBox.SelectedItem as Area).Price.ToString();
                }
                catch
                {
                    priceBox.Text = "0";
                }
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (ValidateValues())
            {
                int nValue = 0;
                int.TryParse(numberBox.Text, out nValue);
                if (m_Object == null)
                {
                    m_Object = new LandRegion(nValue);
                }
                else
                {
                    m_Object.Number = nValue;
                }

                if (m_ParentObject != null)
                {
                    ((Profile)m_ParentObject).UnbindPolygon(polygon);
                }

                m_Object.Polygon = polygon;
                if (areaBox.SelectedIndex > 0)
                {
                    Area a = areaBox.SelectedItem as Area;
                    a.AddRegion(m_Object);
                }
                else
                {
                    if (m_Object.ParentArea != null)
                    {
                        m_Object.ParentArea.RemoveRegion(m_Object);
                    }
                    else
                    {
                        m_Object.ParentArea = null;
                    }
                }
                m_Object.FunctionalUsagesCoefficients = m_aUsageCoefs;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void RegionForm_Shown(object sender, EventArgs e)
        {
            numberBox.Focus();
            numberBox.SelectAll();
            ReinitCoefsControl();
        }

        private void polyBtn_Click(object sender, EventArgs e)
        {
            Profile p = m_ParentObject as Profile;
            if (p != null &&
                p.RegionMap != null &&
                p.RegionMap.Polygons.Count > 0)
            {
                polForm.CurrentProfile = p;
                polForm.CurrentPolygon = polygon;
                if (polForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    polygon = polForm.CurrentPolygon;
                    UpdateRegionStatusLbl();
                }
            }
            else
            {
                MessageBox.Show(this, "Карта не завантажена!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
