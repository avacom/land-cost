using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LandCost.Forms
{
    public partial class LicenseForm : Form
    {
        public LicenseForm()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string Serial
        {
            get
            {
                return serialBox.Text;
            }
            set
            {
                serialBox.Text = value;
            }
        }
    }
}
