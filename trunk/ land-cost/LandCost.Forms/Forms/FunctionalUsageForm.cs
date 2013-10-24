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
    public partial class FunctionalUsageForm : Form, IEditor<FunctionalUsage>
    {
        FunctionalUsage m_Object;
        List<FunctionalUsage> m_aList;
        object m_ParentObject;

        public FunctionalUsageForm()
        {
            InitializeComponent();
            m_ParentObject = null;
            m_aList = new List<FunctionalUsage>();
        }

        void InitializeInternal()
        {
            if (m_Object != null)
            {
                nameBox.Text = m_Object.Name;
                coefBox.Text = m_Object.Weight.ToString();
                this.Text = string.Format("Функціональне призначення {0}", m_Object.Name);
            }
            else
            {
                nameBox.Text = string.Empty;
                coefBox.Text = "0";
                this.Text = "Нове функціональне призначення";
            }
        }

        public FunctionalUsage NewItem
        {
            get { return new FunctionalUsage("New", 0); }
        }

        public object Entity
        {
            get { return m_Object; }
            set
            {
                m_Object = value as FunctionalUsage;
                InitializeInternal();
            }
        }

        public List<FunctionalUsage> CheckList
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

        public bool ValidateValues()
        {
            bool bRet = true;
            // Empty name
            if (string.IsNullOrEmpty(nameBox.Text.Trim()))
            {
                bRet = false;
            }

            if (!bRet)
            {
                MessageBox.Show(this, "Назва функціонального призначення не може бути порожньою", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Duplicate name
            if (bRet && m_aList != null)
            {
                if (m_Object == null)
                {
                    foreach (FunctionalUsage item in m_aList)
                    {
                        if (item.Name == nameBox.Text)
                        {
                            bRet = false;
                            break;
                        }
                    }
                }
                else
                {
                    foreach (FunctionalUsage item in m_aList)
                    {
                        if (!item.Equals(m_Object) && item.Name == nameBox.Text)
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

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (ValidateValues())
            {
                if (m_Object == null)
                {
                    m_Object = new FunctionalUsage(nameBox.Text, coefBox.Value);
                }
                else
                {
                    m_Object.Name = nameBox.Text;
                    m_Object.Weight = coefBox.Value;
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void FunctionalUsageForm_Shown(object sender, EventArgs e)
        {
            nameBox.Focus();
        }
    }
}
