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
    public partial class ConfigurationForm : Form
    {
        Configuration m_oConfiguration;

        EditedListView<Profile> profileList;

        public ConfigurationForm()
        {
            InitializeComponent();
        }

        public Configuration Config
        {
            get
            {
                return m_oConfiguration;
            }
            set
            {
                m_oConfiguration = value;
            }
        }

        private void InitializeControls()
        {
            // 
            // profileList
            // 
            if (profileList != null && !profileList.IsDisposed)
            {
                profileList.Dispose();
            }

            this.profileList = new LandCost.Forms.EditedListView<Profile>();
            this.profileList.ListChanged += new EventHandler(profileList_ListChanged);
            this.profileList.AddButtonText = "Додати профіль";
            this.profileList.DeleteButtonText = "Видалити профіль";
            this.profileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.profileList.EditButtonText = "Редагувати профіль";
            this.profileList.Name = "profileList";
            this.profileList.TabIndex = 0;
            this.listPanel.Controls.Add(this.profileList);
            ColumnHeader nameHeader = new ColumnHeader();
            nameHeader.Width = -2;
            nameHeader.Text = "Назва профілю";
            nameHeader.Tag = "Name";
           
            this.profileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            nameHeader});

            this.profileList.ControlType = typeof(ProfileControl);
            this.profileList.EditorEvent = EditEvent.SelectionChange;
            this.profileList.ActionType = ActionType.AddEditControl;
            this.profileList.OutputControl = profilePanel;
            this.profileList.ItemList = m_oConfiguration.Profiles;
        }

        void profileList_ListChanged(object sender, EventArgs e)
        {
            if (m_oConfiguration != null)
            {
                m_oConfiguration.Changed = true;
            }
        }

        private void ConfigurationForm_Shown(object sender, EventArgs e)
        {
            if (m_oConfiguration != null)
            {
                InitializeControls();
            }
        }
    }
}
