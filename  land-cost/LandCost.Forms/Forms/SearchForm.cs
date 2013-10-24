using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LandCost.Forms.Forms
{
    public partial class SearchForm<T> : Form
    {
        List<T> m_aPresentList;
        List<T> m_aCompleteList;
        List<T> m_aShownList;
        List<T> m_aSelectedItems;
        EditedListView<T> listView;
        string m_sObjectName;
        object m_ParentObject;

        public SearchForm(List<T> presentList, List<T> completeList, string objName)
        {
            InitializeComponent();
            this.Text = objName;
            m_aPresentList = presentList;
            m_aCompleteList = completeList;
            m_aSelectedItems = new List<T>();
            //m_aShownList = m_aCompleteList.Except<T>(m_aPresentList).ToList<T>();
            m_ParentObject = null;
            m_sObjectName = objName;
        }

        private void InitControls()
        {
            ///
            /// listView
            /// 
            if (listView != null && !listView.IsDisposed)
            {
                listView.Dispose();
            }
            this.listView = new LandCost.Forms.EditedListView<T>();
            this.listView.ToolsVisible = false;
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.Name = "listView";
            this.listView.TabIndex = 0;
            this.listPanel.Controls.Add(this.listView);
            ColumnHeader nameHeader = new ColumnHeader();
            nameHeader.Width = -2;
            nameHeader.Text = m_sObjectName;
            nameHeader.Tag = "DisplayName";

            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            nameHeader});

            this.listView.ItemList = m_aShownList;
            this.listView.EditorEvent = EditEvent.DoubleClick;
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

        public List<T> SelectedObjects
        {
            get
            {
                return m_aSelectedItems;
            }
        }

        public List<T> ShownList
        {
            get
            {
                List<T> res = new List<T>();
                if (m_aCompleteList != null &&
                    m_aPresentList != null)
                {
                    res = m_aCompleteList.Except<T>(m_aPresentList).ToList<T>();
                }

                return res;
            }
        }

        public void SetLists(List<T> complete, List<T> present)
        {
            List<T> sel = listView.SelectedObjects;
            if (sel != null && sel.Count > 0)
            {
                m_aSelectedItems = sel;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void SearchForm_Shown(object sender, EventArgs e)
        {
            m_aShownList = m_aCompleteList.Except<T>(m_aPresentList).ToList<T>();
            InitControls();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            m_aSelectedItems = listView.SelectedObjects;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
