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
    public partial class LocalCoefficientValueForm : Form, IEditor<LocalCoefficientValue>
    {
        LocalCoefficientValue m_Object;
        List<LocalCoefficientValue> m_aList;
        object m_ParentObject;

        public LocalCoefficientValueForm()
        {
            InitializeComponent();
            m_ParentObject = null;
            m_aList = new List<LocalCoefficientValue>();
        }

        void InitCoefBox()
        {
            coefBox.Items.Clear();
            if (m_ParentObject != null &&
                m_ParentObject is Profile)
            {
                Profile prof = m_ParentObject as Profile;
                if (prof.LocalCoefficients != null &&
                    prof.LocalCoefficients.Count > 0)
                {
                    List<LocalCoefficient> exist = new List<LocalCoefficient>();
                    if (m_aList != null)
                    {
                        foreach (LocalCoefficientValue val in m_aList)
                        {
                            exist.Add(val.Coefficient);
                        }
                    }
                    List<LocalCoefficient> shown = prof.LocalCoefficients.Except<LocalCoefficient>(exist).ToList<LocalCoefficient>();
                    if (m_Object != null)
                    {
                        shown.Add(m_Object.Coefficient);
                    }

                    coefBox.BeginUpdate();
                    foreach (LocalCoefficient coef in shown)
                    {
                        coefBox.Items.Add(coef);
                    }
                    coefBox.EndUpdate();
                }
            }
        }

        public bool ValidateValues()
        {
            bool bRet = true;
            if (coefBox.Items.Count == 0)
            {
                MessageBox.Show(this, "Не задано локальний коефіцієнт!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bRet = false;
            }
            return bRet;
        }

        void SetValues()
        {
            if (m_Object != null)
            {
                if (coefBox.Items.Count > 0)
                {
                    coefBox.SelectedItem = m_Object.Coefficient;
                }
                valBox.Text = m_Object.Value.ToString();
                this.Text = string.Format("Значення локального коефіцієнта {0}", m_Object.Coefficient.Name);
            }
            else
            {
                if (coefBox.Items.Count > 0)
                {
                    coefBox.SelectedIndex = 0;
                }
                valBox.Text = "1";
                this.Text = "Нове значення локального коефіцієнта";
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
                m_Object = value as LocalCoefficientValue;
            }
        }

        public List<LocalCoefficientValue> CheckList
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

        public LocalCoefficientValue NewItem
        {
            get { return null; }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (ValidateValues())
            {
                if (m_Object == null)
                {
                    m_Object = new LocalCoefficientValue(coefBox.SelectedItem as LocalCoefficient, valBox.Value);
                }
                else
                {
                    m_Object.Coefficient = coefBox.SelectedItem as LocalCoefficient;
                    m_Object.Value = valBox.Value;
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void LocalCoefficientValueForm_Load(object sender, EventArgs e)
        {
            InitCoefBox();
            SetValues();
            if (coefBox.Items.Count == 0)
            {
                MessageBox.Show(this, "Значення задані для всіх локальних коефіцієнтів", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void LocalCoefficientValueForm_Shown(object sender, EventArgs e)
        {
            coefBox.Focus();
        }
    }
}
