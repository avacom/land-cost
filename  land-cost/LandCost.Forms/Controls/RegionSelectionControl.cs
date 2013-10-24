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
    public partial class RegionSelectionControl : UserControl
    {
        List<LandRegion> m_aRegionList;
        LandRegion m_curRegion;

        public event EventHandler SelectionMade;

        public void OnSelectionMade(object sender, EventArgs e)
        {
            if (SelectionMade != null)
            {
                SelectionMade(sender, e);
            }
        }

        public RegionSelectionControl()
        {
            InitializeComponent();
            coefValsCtl.ReadOnly = true;
            Reinit();
        }

        void Reinit()
        {
            m_aRegionList = new List<LandRegion>();
            m_curRegion = null;
            regionBox.Items.Clear();
            usageBox.Items.Clear();
            coefPanel.Controls.Clear();
            coefValsCtl.List = null;
        }

        [Browsable(false)]
        public List<LandRegion> RegionList
        {
            get
            {
                return m_aRegionList;
            }
            set
            {
                Reinit();
                m_aRegionList = value;
                InitRegionBox();
            }
        }

        [Browsable(false)]
        public LandRegion CurrentRegion
        {
            get
            {
                return m_curRegion;
            }
            set
            {
                m_curRegion = value;
                //if (m_curRegion != null &&
                //    regionBox.Items.Contains(m_curRegion))
                //{
                    regionBox.SelectedItem = m_curRegion;
                //}
            }
        }

        [Browsable(false)]
        public FunctionalUsageCoefficients CurrentFunctionalUsageCoefficients
        {
            get
            {
                if (usageBox.Items.Count > 0)
                {
                    return usageBox.SelectedItem as FunctionalUsageCoefficients;
                }
                else
                {
                    return null;
                }
            }
        }

        void InitUsageBox()
        {
            usageBox.Items.Clear();
            if (m_curRegion != null &&
                m_curRegion.FunctionalUsagesCoefficients != null &&
                m_curRegion.FunctionalUsagesCoefficients.Count > 0)
            {
                usageBox.BeginUpdate();
                foreach (FunctionalUsageCoefficients fuc in m_curRegion.FunctionalUsagesCoefficients)
                {
                    usageBox.Items.Add(fuc);
                }
                usageBox.EndUpdate();
                usageBox.SelectedIndex = 0;
            }
            else
            {
                OnSelectionMade(this, null);
            }
        }

        void InitRegionBox()
        {
            regionBox.Items.Clear();
            if (m_aRegionList != null &&
                m_aRegionList.Count > 0)
            {
                regionBox.BeginUpdate();
                foreach (LandRegion reg in m_aRegionList)
                {
                    regionBox.Items.Add(reg);
                }
                regionBox.EndUpdate();
                regionBox.SelectedIndex = 0;
            }
        }

        void ReinitCoefsControl()
        {
            coefPanel.Controls.Clear();
            if (usageBox.Items.Count > 0 &&
                usageBox.SelectedIndex >= 0 && usageBox.SelectedIndex < usageBox.Items.Count)
            {
                FunctionalUsageCoefficients fuc = usageBox.SelectedItem as FunctionalUsageCoefficients;
                coefValsCtl.List = fuc.LocalCoefficientValues;
                coefPanel.Controls.Add(coefValsCtl);
            }
            else
            {
                coefValsCtl.List = null;
            }
            OnSelectionMade(this, null);
        }

        private void regionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (regionBox.SelectedIndex >= 0 && regionBox.SelectedIndex < regionBox.Items.Count)
            {
                m_curRegion = regionBox.SelectedItem as LandRegion;
            }
            InitUsageBox();
        }

        private void usageBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReinitCoefsControl();
        }
    }
}
