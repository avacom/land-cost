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
    public partial class FunctionalUsageCoefficientsForm : Form, IEditor<FunctionalUsageCoefficients>
    {
        FunctionalUsageCoefficients m_Object;
        List<FunctionalUsageCoefficients> m_aList;
        object m_ParentObject;

        List<LocalCoefficientValue> coefVals;
        EditedListView<LocalCoefficientValue> coefList;

        public FunctionalUsageCoefficientsForm()
        {
            InitializeComponent();
        }

        void InitUsageBox()
        {
            usageBox.Items.Clear();
            if (m_ParentObject != null &&
                m_ParentObject is Profile)
            {
                Profile prof = m_ParentObject as Profile;
                if (prof.FunctionalUsages != null &&
                    prof.FunctionalUsages.Count > 0)
                {
                    usageBox.BeginUpdate();
                    foreach (FunctionalUsage usage in prof.FunctionalUsages)
                    {
                        usageBox.Items.Add(usage);
                    }
                    usageBox.EndUpdate();
                }
            }
        }

        void InitCoefList()
        {
            ///
            /// coefList
            /// 
            if (coefList != null && !coefList.IsDisposed)
            {
                coefList.Dispose();
            }
            coefList = new LandCost.Forms.EditedListView<LocalCoefficientValue>();
            coefList.AddButtonText = "Додати значення локального коефіцієнта";
            coefList.DeleteButtonText = "Видалити значення локального коефіцієнта";
            coefList.Dock = System.Windows.Forms.DockStyle.Fill;
            coefList.EditButtonText = "Редагувати значення локального коефіцієнта";
            coefList.Name = "coefList";
            coefList.TabIndex = 0;
            coefPanel.Controls.Add(coefList);
            ColumnHeader lcot_nameHeader = new ColumnHeader();
            lcot_nameHeader.Width = 200;
            lcot_nameHeader.Text = "Коефіцієнт";
            lcot_nameHeader.Tag = "Coefficient";

            ColumnHeader lcot_valHeader = new ColumnHeader();
            lcot_valHeader.Width = -2;
            lcot_valHeader.Text = "Значення";
            lcot_valHeader.Tag = "Value";

            coefList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            lcot_nameHeader,
            lcot_valHeader});

            coefList.FormType = typeof(LocalCoefficientValueForm);
            coefList.EditorEvent = EditEvent.DoubleClick;
            coefList.ActionType = ActionType.AddEditForm;
            coefList.ItemList = coefVals;
            coefList.ParentObject = m_ParentObject;
        }

        void SetValues()
        {
            if (m_Object != null)
            {
                if (usageBox.Items.Count > 0)
                {
                    usageBox.SelectedItem = m_Object.Usage;
                }
                coefVals = m_Object.LocalCoefficientValues;
                this.Text = string.Format("Набір коефіцієнтів для {0}", m_Object.Usage.Name);
            }
            else
            {
                if (usageBox.Items.Count > 0)
                {
                    usageBox.SelectedIndex = 0;
                }
                coefVals = new List<LocalCoefficientValue>();
                this.Text = "Новий набір коефіцієнтів для функціонального призначення";
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
                m_Object = value as FunctionalUsageCoefficients;
            }
        }

        public List<FunctionalUsageCoefficients> CheckList
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

        public FunctionalUsageCoefficients NewItem
        {
            get { return null; }
        }

        private void FunctionalUsageCoefficientsForm_Load(object sender, EventArgs e)
        {
            InitUsageBox();
            SetValues();
            InitCoefList();
            if (usageBox.Items.Count == 0)
            {
                MessageBox.Show(this, "Не знайдено жодного функціонального призначення", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.Cancel;
                this.WindowState = FormWindowState.Minimized;
                this.Close();
            }
        }

        public bool ValidateValues()
        {
            bool bRet = true;
            if (usageBox.Items.Count == 0)
            {
                MessageBox.Show(this, "Не задано функціональне призначення!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bRet = false;
            }

            if (bRet)
            {
                if (coefVals == null ||
                    coefVals.Count == 0)
                {
                    MessageBox.Show(this, "Необхідно задати значення хоча б для одного локального коефіцієнта!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bRet = false;
                }
            }
            return bRet;
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
                if (m_Object == null)
                {
                    m_Object = new FunctionalUsageCoefficients(usageBox.SelectedItem as FunctionalUsage);
                }
                else
                {
                    m_Object.Usage = usageBox.SelectedItem as FunctionalUsage;
                }
                m_Object.LocalCoefficientValues = coefVals;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void FunctionalUsageCoefficientsForm_Shown(object sender, EventArgs e)
        {
            usageBox.Focus();
        }
    }
}
