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
    public partial class LocalCoefficientForm : Form, IEditor<LocalCoefficient>
    {
        LocalCoefficient m_Object;
        List<LocalCoefficient> m_aList;
        object m_ParentObject;

        public LocalCoefficientForm()
        {
            InitializeComponent();
            m_ParentObject = null;
            m_aList = new List<LocalCoefficient>();
        }

        void InitializeInternal()
        {
            if (m_Object != null)
            {
                nameBox.Text = m_Object.Name;
                this.Text = string.Format("Локальний коефіцієнт {0}", m_Object.Name);
            }
            else
            {
                nameBox.Text = string.Empty;
                this.Text = "Новий локальний коефіцієнт";
            }
        }

        public object Entity
        {
            get { return m_Object; }
            set
            {
                m_Object = value as LocalCoefficient;
                InitializeInternal();
            }
        }

        public LocalCoefficient NewItem
        {
            get { return new LocalCoefficient("New"); }
        }

        public List<LocalCoefficient> CheckList
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
                MessageBox.Show(this, "Назва локального коефіцієнта не може бути порожньою", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Duplicate name
            if (bRet && m_aList != null)
            {
                if (m_Object == null)
                {
                    foreach (LocalCoefficient item in m_aList)
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
                    foreach (LocalCoefficient item in m_aList)
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
                    m_Object = new LocalCoefficient(nameBox.Text);
                }
                else
                {
                    m_Object.Name = nameBox.Text;
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void LocalCoefficientForm_Shown(object sender, EventArgs e)
        {
            nameBox.Focus();
        }
    }
}
