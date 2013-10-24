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
    public partial class DocumentForm : Form, IEditor<Document>
    {
        Document m_Object;
        List<Document> m_aList;
        object m_ParentObject;

        public DocumentForm()
        {
            InitializeComponent();
            m_ParentObject = null;
            m_aList = new List<Document>();
        }

        void InitializeInternal()
        {
            if (m_Object != null)
            {
                nameBox.Text = m_Object.Name;
                formatBox.Text = m_Object.Mask;
                this.Text = string.Format("Документ {0}", m_Object.Name);
            }
            else
            {
                nameBox.Text = string.Empty;
                formatBox.Text = string.Empty;
                this.Text = "Новий документ";
            }
        }

        public object Entity
        {
            get { return m_Object; }
            set
            {
                m_Object = value as Document;
                InitializeInternal();
            }
        }

        public Document NewItem
        {
            get { return new Document("New", ""); }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (ValidateValues())
            {
                if (m_Object == null)
                {
                    m_Object = new Document(nameBox.Text, formatBox.Text);
                }
                else
                {
                    m_Object.Name = nameBox.Text;
                    m_Object.Mask = formatBox.Text;
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
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
                MessageBox.Show(this, "Назва документа не може бути порожньою", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Duplicate name
            if (bRet && m_aList != null)
            {
                if (m_Object == null)
                {
                    foreach (Document item in m_aList)
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
                    foreach (Document item in m_aList)
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

        public List<Document> CheckList
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

        private void DocumentForm_Shown(object sender, EventArgs e)
        {
            nameBox.Focus();
        }
    }
}
