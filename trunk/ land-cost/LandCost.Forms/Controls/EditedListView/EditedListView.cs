using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LandCost.Entities;
using LandCost.Forms.Forms;
using System.Collections;

namespace LandCost.Forms
{
    public enum ActionType
    {
        AddEditForm = 0,
        AddEditControl = 1,
        AddSearchFormEditForm = 2
    }

    public enum EditEvent
    {
        DoubleClick = 0,
        SelectionChange = 1
    }

    public partial class EditedListView<T> : UserControl
    {
        bool m_bEnabled;

        List<T> m_aItemList;

        Type m_FormType;
        Type m_ControlType;
        SearchForm<T> m_SearchForm;

        Form m_EditForm;
        Control m_EditControl;
        Control m_OutputControl;

        ActionType m_ActionType;
        EditEvent m_EditEvent;

        object m_ParentObject;

        public event EventHandler SelectionChanged;
        public event EventHandler ListChanged;

        ListViewColumnSorter lvwColumnSorter = null;
        public EditedListView()
        {
            InitializeComponent();
            lvwColumnSorter = new ListViewColumnSorter();
            this.list.ListViewItemSorter = lvwColumnSorter;
            this.list.Sorting = SortOrder.Ascending;
            this.list.AutoArrange = true;

            lvwColumnSorter._SortModifier = ListViewColumnSorter.SortModifiers.SortByText;

            m_bEnabled = true;
            m_ActionType = ActionType.AddEditForm;
            m_EditEvent = EditEvent.DoubleClick;
            m_ParentObject = null;
            SetEnabled();
        }

        public void OnSelectionChanged(object sender, EventArgs e)
        {
            if (SelectionChanged != null)
            {
                SelectionChanged(sender, e);
            }
        }

        public void OnListChanged(object sender, EventArgs e)
        {
            if (ListChanged != null)
            {
                ListChanged(sender, e);
            }
        }

        private void SizeLastColumn(ListView lv)
        {
            if (lv != null && lv.Columns.Count > 0)
            {
                this.list.Resize -= new System.EventHandler(this.list_Resize);
                lv.Columns[lv.Columns.Count - 1].Width = -2;
                this.list.Resize += new System.EventHandler(this.list_Resize);
            }
        }

        private void EditedListView_Load(object sender, EventArgs e)
        {
            SizeLastColumn(list);
        }

        private void list_Resize(object sender, EventArgs e)
        {
            SizeLastColumn(list);
        }

        void SetEnabled()
        {
            if (list != null)
            {
                list.Enabled = m_bEnabled;
                addButton.Enabled = m_bEnabled;

                if (list.SelectedItems != null &&
                    list.SelectedItems.Count > 0)
                {
                    if (list.SelectedItems.Count == 1)
                    {
                        editButton.Enabled = true && m_bEnabled;
                    }
                    else
                    {
                        editButton.Enabled = false;
                    }

                    deleteButton.Enabled = true && m_bEnabled;
                }
                else
                {
                    editButton.Enabled = false;
                    deleteButton.Enabled = false;
                }
            }
        }

        void PopulateListView(List<T> m_aItemList)
        {
            list.Items.Clear();
            try
            {
                foreach (T item in m_aItemList)
                {
                    ListViewItem lvi = new ListViewItem(GetProperties(item));
                    lvi.Tag = item;
                    list.Items.Add(lvi);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void AddItem()
        {
            if (m_ActionType == ActionType.AddEditForm)
            {
                if (m_EditForm != null)
                {
                    ((IEditor<T>)m_EditForm).Entity = null;
                    ((IEditor<T>)m_EditForm).ParentObject = ParentObject;
                    m_EditForm.StartPosition = FormStartPosition.CenterParent;
                    DialogResult res = m_EditForm.ShowDialog(this);
                    if (res == DialogResult.OK)
                    {
                        T item = (T)((IEditor<T>)m_EditForm).Entity;

                        UpdateListViewItemByTag(item);
                        OnListChanged(this, null);
                    }
                }
            }
            else if (m_ActionType == ActionType.AddEditControl)
            {

                if (m_EditControl != null)
                {
                    T item = ((IEditor<T>)m_EditControl).NewItem;
                    UpdateListViewItemByTag(item);
                    OnListChanged(this, null);
                    //((IEditor<T>)m_EditControl).Entity = null;
                    //((IEditor<T>)m_EditControl).ParentObject = ParentObject;
                    //if (m_OutputControl != null)
                    //{
                    //    m_OutputControl.Controls.Clear();
                    //    m_EditControl.Dock = DockStyle.Fill;
                    //    m_OutputControl.Controls.Add(m_EditControl);
                    //}
                }
            }
            else if (m_ActionType == LandCost.Forms.ActionType.AddSearchFormEditForm)
            {
                if (m_SearchForm != null)
                {
                    if (m_SearchForm.ShownList.Count > 0)
                    {
                        m_SearchForm.StartPosition = FormStartPosition.CenterParent;
                        DialogResult res = m_SearchForm.ShowDialog(this);
                        if (res == DialogResult.OK)
                        {
                            List<T> items = m_SearchForm.SelectedObjects;
                            if (items != null)
                            {
                                list.BeginUpdate();
                                foreach (T item in items)
                                {
                                    UpdateListViewItemByTag(item);
                                }
                                list.EndUpdate();
                            }
                            OnListChanged(this, null);
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "Додано всі можливі елементи", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        void EditItem()
        {
            if (m_ActionType == ActionType.AddEditForm)
            {
                if (m_EditForm != null &&
                    list != null &&
                    list.SelectedItems != null &&
                    list.SelectedItems.Count > 0)
                {
                    ((IEditor<T>)m_EditForm).Entity = list.SelectedItems[0].Tag;
                    ((IEditor<T>)m_EditForm).ParentObject = ParentObject;

                    m_EditForm.StartPosition = FormStartPosition.CenterParent;
                    DialogResult res = m_EditForm.ShowDialog(this);
                    if (res == DialogResult.OK)
                    {
                        T item = (T)((IEditor<T>)m_EditForm).Entity;
                        UpdateListViewItemByTag(item);
                        OnListChanged(this, null);
                    }
                }
            }
            else if (m_ActionType == ActionType.AddEditControl)
            {
                ControlType = ControlType;
                if (m_EditControl != null &&
                    list != null &&
                    list.SelectedItems != null &&
                    list.SelectedItems.Count == 1)
                {

                    ((IEditor<T>)m_EditControl).Entity = list.SelectedItems[0].Tag;
                    ((IEditor<T>)m_EditControl).ParentObject = ParentObject;
                    if (m_OutputControl != null)
                    {
                        m_OutputControl.Controls.Clear();
                        m_EditControl.Dock = DockStyle.Fill;
                        m_OutputControl.Controls.Add(m_EditControl);
                    }
                }
                else
                {
                    if (m_OutputControl != null)
                    {
                        m_OutputControl.Controls.Clear();
                    }
                }
            }
        }

        private void UpdateListViewItemByTag(T item)
        {
            try
            {
                ListViewItem lvItem = null;
                if (list != null &&
                    list.Items != null)
                {
                    foreach (ListViewItem lvi in list.Items)
                    {
                        if (lvi.Tag.Equals(item))
                        {
                            lvItem = lvi;
                            break;
                        }
                    }

                    if (lvItem != null)
                    {
                        int i = 0;
                        foreach (ColumnHeader header in list.Columns)
                        {
                            string propName = header.Tag.ToString();

                            System.Windows.Forms.ListViewItem.ListViewSubItem subItem = lvItem.SubItems[i];
                            if (subItem != null)
                            {
                                object val = GetPropValue(item, propName);
                                if (val != null)
                                {
                                    subItem.Text = val.ToString();
                                }
                                else
                                {
                                    subItem.Text = "";
                                }
                            }
                            i++;
                        }
                    }
                    else
                    {
                        lvItem = new ListViewItem(GetProperties(item));
                        lvItem.Tag = item;
                        list.Items.Add(lvItem);
                        m_aItemList.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
      
        void DeleteItems()
        {
            //if (m_ActionType != LandCost.Forms.ActionType.AddSearchFormEditForm)
            //{
                string sQuestion = "Ви дійсно бажаєте видалити обраний об'єкт?";
                if (list != null &&
                    list.SelectedItems != null &&
                    list.SelectedItems.Count > 0)
                {
                    if (list.SelectedItems.Count > 1)
                    {
                        sQuestion = string.Format("Ви дійсно бажаєте видалити {0} об'єктів?", list.SelectedItems.Count);
                    }

                    if (MessageBox.Show(this, sQuestion, "Запитання", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        List<ListViewItem> selectedItems = new List<ListViewItem>();
                        list.BeginUpdate();
                        foreach (ListViewItem item in list.SelectedItems)
                        {
                            if (m_ParentObject is IControlledRemover)
                            {
                                ((IControlledRemover)m_ParentObject).PrepareRemoval(item.Tag);
                            }
                            m_aItemList.Remove((T)item.Tag);
                            selectedItems.Add(item);
                        }
                        try
                        {
                            foreach (ListViewItem item in selectedItems)
                            {
                                list.Items.Remove(item);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        list.EndUpdate();
                        OnListChanged(this, null);
                    }
                }
            //}
        }

        private string[] GetProperties(T item)
        {
            List<string> res = new List<string>();
            try
            {
                foreach (ColumnHeader header in list.Columns)
                {
                    if (header.Tag != null)
                    {
                        object prop = GetPropValue(item, header.Tag.ToString());
                        if (prop != null)
                        {
                            res.Add(prop.ToString());
                        }
                        else
                        {
                            res.Add("");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return res.ToArray();
        }

        private object GetPropValue(object src, string propName)
        {
            object res = null;
            try
            {
                res = src.GetType().GetProperty(propName).GetValue(src, null);
            }
            catch
            {
                res = null;
            }
            return res;
        }

        private void list_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnSelectionChanged(sender, e);
            SetEnabled();
            if (m_EditEvent == EditEvent.SelectionChange)
            {
                EditItem();
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            DeleteItems();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddItem();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            EditItem();
        }

        private void list_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (m_EditEvent == EditEvent.DoubleClick)
            {
                EditItem();
            }
        }

        private void list_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteItems();
            }
        }

        void m_EditControl_Validated(object sender, EventArgs e)
        {
            T item = (T)((IEditor<T>)sender).Entity;
            UpdateListViewItemByTag(item);
            OnListChanged(this, null);
        }

        #region Properties

        [Description("The columns of the list"), Category("Data")]
        public System.Windows.Forms.ListView.ColumnHeaderCollection Columns
        {
            get { return list.Columns; }
        }

        [Category("Data")]
        public List<T> ItemList
        {
            get { return m_aItemList; }
            set 
            { 
                m_aItemList = value;
                list.BeginUpdate();
                PopulateListView(m_aItemList);
                list.EndUpdate();

                if (m_EditForm != null)
                {
                    ((IEditor<T>)m_EditForm).CheckList = m_aItemList;
                }

                if (m_EditControl != null)
                {
                    ((IEditor<T>)m_EditControl).CheckList = m_aItemList;
                }

            }
        }

        [Category("Data")]
        public Type FormType
        {
            get { return m_FormType; }
            set 
            { 
                m_FormType = value;
                m_EditForm = (Form)Activator.CreateInstance(m_FormType);
                ((IEditor<T>)m_EditForm).CheckList = m_aItemList;
            }
        }

        [Category("Data")]
        public Type ControlType
        {
            get { return m_ControlType; }
            set
            {
                m_ControlType = value;
                m_EditControl = (Control)Activator.CreateInstance(m_ControlType);
                ((IEditor<T>)m_EditControl).CheckList = m_aItemList;
                m_EditControl.Validated += new EventHandler(m_EditControl_Validated);
                if (m_EditControl is IModificationAnnouncer)
                {
                    ((IModificationAnnouncer)m_EditControl).Modified += new EventHandler(EditedListView_Modified);
                }
            }
        }

        void EditedListView_Modified(object sender, EventArgs e)
        {
            OnListChanged(this, null);
        }

        [Category("Data")]
        public bool Enabled
        {
            get { return m_bEnabled; }
            set
            {
                m_bEnabled = value;
                SetEnabled();
            }
        }

        [Category("Data")]
        public Control OutputControl
        {
            get { return m_OutputControl; }
            set
            {
                m_OutputControl = value;
                m_OutputControl.Controls.Clear();
            }
        }

        [Category("Data")]
        public SearchForm<T>SearchForm
        {
            get { return m_SearchForm; }
            set
            {
                m_SearchForm = value;
            }
        }

        [Category("Data")]
        public bool ToolsVisible
        {
            get { return tools.Visible; }
            set
            {
                tools.Visible = value;
            }
        }

        [Category("Data")]
        public ActionType ActionType
        {
            get { return m_ActionType; }
            set
            {
                m_ActionType = value;
                if (m_ActionType == ActionType.AddEditControl || m_ActionType == LandCost.Forms.ActionType.AddSearchFormEditForm)
                {
                    editButton.Visible = false;
                }
                else
                {
                    editButton.Visible = true;
                }
            }
        }

        [Category("Data")]
        public EditEvent EditorEvent
        {
            get { return m_EditEvent; }
            set
            {
                m_EditEvent = value;
            }
        }

        [Category("Data")]
        public object ParentObject
        {
            get { return m_ParentObject; }
            set
            {
                m_ParentObject = value;
            }
        }

        [Description("Add-button text"), Category("Data")]
        public string AddButtonText
        {
            get { return addButton.Text; }
            set { addButton.Text = value; }
        }

        [Description("Edit-button text"), Category("Data")]
        public string EditButtonText
        {
            get { return editButton.Text; }
            set { editButton.Text = value; }
        }

        [Description("Delete-button text"), Category("Data")]
        public string DeleteButtonText
        {
            get { return deleteButton.Text; }
            set { deleteButton.Text = value; }
        }

        public List<T> SelectedObjects
        {
            get
            {
                List<T> ret = new List<T>();
                if (list != null &&
                    list.SelectedItems != null &&
                    list.SelectedItems.Count > 0)
                {
                    foreach (ListViewItem item in list.SelectedItems)
                    {
                        ret.Add((T)item.Tag);
                    }
                }
                return ret;
            }
        }

        #endregion Properties

        private void list_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView myListView = (ListView)sender;
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            myListView.Sort();
        }

        #region Public Methods

        #endregion Public Methods
    }
}
