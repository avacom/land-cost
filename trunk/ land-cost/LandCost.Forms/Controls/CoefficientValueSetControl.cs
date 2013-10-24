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
    public partial class CoefficientValueSetControl : UserControl
    {
        List<LocalCoefficientValue> m_aList;
        bool m_bReadOnly;

        public event EventHandler CoefficientsChanged;
        private void OnCoefficientsChanged(object sender, EventArgs e)
        {
            if (CoefficientsChanged != null)
            {
                CoefficientsChanged(sender, e);
            }
        }

        public CoefficientValueSetControl()
        {
            InitializeComponent();
            m_bReadOnly = false;
        }

        public List<LocalCoefficientValue> List
        {
            get
            {
                return m_aList;
            }
            set
            {
                m_aList = value;
                panel.Controls.Clear();
                if (m_aList != null)
                {
                    foreach (LocalCoefficientValue item in m_aList)
                    {
                        CoefficientValueControl ctl = new CoefficientValueControl();
                        ctl.ReadOnly = m_bReadOnly;
                        ctl.Object = item;
                        panel.Controls.Add(ctl);
                        ctl.ValueChanged += new EventHandler(ctl_ValueChanged);
                    }
                }
            }
        }

        void ctl_ValueChanged(object sender, EventArgs e)
        {
            OnCoefficientsChanged(this, null);
        }

        public bool ReadOnly
        {
            get
            {
                return m_bReadOnly;
            }
            set
            {
                m_bReadOnly = value;
                SetEnabled();
            }
        }

        private void SetEnabled()
        {
            foreach (Control c in panel.Controls)
            {
                if (c is CoefficientValueControl)
                {

                    ((CoefficientValueControl)c).ReadOnly = m_bReadOnly;
                }
            }
        }
    }
}
