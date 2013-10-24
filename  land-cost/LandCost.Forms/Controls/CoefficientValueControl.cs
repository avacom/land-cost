using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LandCost.Entities;

namespace LandCost.Forms
{
    public partial class CoefficientValueControl : UserControl
    {
        ToolTip nameToolTip;
        LocalCoefficientValue m_Object;

        public event EventHandler ValueChanged;
        private void OnValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(sender, e);
            }
        }

        int toolTipTime = 4000;

        public CoefficientValueControl()
        {
            InitializeComponent();

            nameToolTip = new ToolTip();
        }

        public LocalCoefficientValue Object
        {
            get
            {
                return m_Object;
            }
            set
            {
                m_Object = value;
                if (m_Object != null)
                {
                    nameBox.Text = m_Object.Coefficient.Name;
                    nameToolTip.SetToolTip(nameBox, nameBox.Text);
                    valueBox.DataBindings.Clear();
                    valueBox.DataBindings.Add("Text", m_Object, "Value");
                }
            }
        }

        private void valueBox_Validating(object sender, CancelEventArgs e)
        {
            if (m_Object != null && m_Object.Value != valueBox.Value)
            {
                m_Object.Value = valueBox.Value;
                OnValueChanged(this, null);
            }
        }

        public bool ReadOnly
        {
            get
            {
                return valueBox.ReadOnly;
            }
            set
            {
                valueBox.ReadOnly = value;
            }
        }

    }
}
