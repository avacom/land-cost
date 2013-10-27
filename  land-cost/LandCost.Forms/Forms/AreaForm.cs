using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LandCost.Entities;

namespace LandCost.Forms
{
    public partial class AreaForm : Form, IEditor<Area>
    {
        Area m_Object;
        List<Area> m_aList;

        List<LandRegion> m_aRegions;
        EditedListView<LandRegion> regionList;

        object m_ParentObject;

        public AreaForm()
        {
            InitializeComponent();
        }

        void InitRegionList()
        {
            ///
            /// regionList
            /// 
            if (regionList != null && !regionList.IsDisposed)
            {
                regionList.Dispose();
            }
            regionList = new LandCost.Forms.EditedListView<LandRegion>();
            regionList.AddButtonText = "Додати район зі списку";
            regionList.DeleteButtonText = "Видалити район";
            regionList.Dock = System.Windows.Forms.DockStyle.Fill;
            regionList.EditButtonText = "Редагувати район";
            regionList.Name = "regionList";
            regionList.TabIndex = 0;
            regionPanel.Controls.Add(regionList);

            ColumnHeader nameHeader = new ColumnHeader();
            nameHeader.Width = -2;
            nameHeader.Text = "Номер району";
            nameHeader.Tag = "Number";

            regionList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            nameHeader});

            regionList.FormType = typeof(RegionForm);
            regionList.EditorEvent = EditEvent.DoubleClick;
            regionList.ActionType = ActionType.AddSearchFormEditForm;

            List<LandRegion> total = null;
            if (m_ParentObject != null)
            {
                total = ((Profile)m_ParentObject).Regions;
            }

            regionList.SearchForm = new Forms.SearchForm<LandRegion>(m_aRegions, total, "Район");
            regionList.ItemList = m_aRegions;
            regionList.ParentObject = m_ParentObject;
        }

        void SetValues()
        {
            m_aRegions = new List<LandRegion>();
            if (m_Object != null)
            {
                numberBox.Text = m_Object.Number.ToString();
                coefBox.Text = m_Object.KM2.ToString();
                priceBox.Text = m_Object.Price.ToString();
                m_aRegions.AddRange(m_Object.Regions);
                this.Text = string.Format("Економіко-планувальна зона {0}", m_Object.Number);
            }
            else
            {
                numberBox.Text = "";
                coefBox.Text = "0";
                priceBox.Text = "0";
                this.Text = "Нова економіко-планувальна зона";
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
                m_Object = value as Area;
            }
        }

        public List<Area> CheckList
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

        public Area NewItem
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
                MessageBox.Show(this, "Не заданий номер економіко-планувальної зони!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Incorrect number format
            int nValue = 0;
            if (!int.TryParse(numberBox.Text, out nValue))
            {
                bRet = false;
            }

            if (!bRet)
            {
                MessageBox.Show(this, "Некоректний формат номера економіко-планувальної зони! Задайте, будь ласка, ціле число", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Duplicate number
            if (bRet && m_aList != null)
            {
                if (m_Object == null)
                {
                    foreach (Area obj in m_aList)
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
                    foreach (Area obj in m_aList)
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

        private void AreaForm_Shown(object sender, EventArgs e)
        {
            numberBox.Focus();
            numberBox.SelectAll();
        }

        private void AreaForm_Load(object sender, EventArgs e)
        {
            SetValues();
            InitRegionList();
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
                double dPrice = 0;
                double dKm2 = 0;

                int.TryParse(numberBox.Text, out nValue);
                dPrice = priceBox.Value;
                dKm2 = coefBox.Value;

                if (m_Object == null)
                {
                    m_Object = new Area(nValue, dPrice, dKm2);
                }
                else
                {
                    m_Object.Number = nValue;
                    m_Object.Price = dPrice;
                    m_Object.KM2 = dKm2;
                }

                // Add non-existing
                foreach (LandRegion reg in m_aRegions)
                {
                    if (!m_Object.Regions.Contains(reg))
                    {
                        m_Object.AddRegion(reg);
                    }
                }

                List<LandRegion> regs = new List<LandRegion>();
                regs.AddRange(m_Object.Regions);

                // Remove obsolete
                foreach (LandRegion reg in regs)
                {
                    if (!m_aRegions.Contains(reg))
                    {
                        m_Object.RemoveRegion(reg);
                    }
                }

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
    }
}
