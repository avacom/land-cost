using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LandCost.Entities
{
    /// <summary>
    /// Represents the global areas which define the price of land and consist of regions
    /// </summary>
    public class Area
    {
        #region Members

        int m_nNumber;
        double m_dPrice;
        double m_dKm2;

        List<LandRegion> m_aRegions;

        #endregion Members

        #region Constructors

        /// <summary>
        /// Create the instance of an Area
        /// </summary>
        /// <param name="number">The number of an area</param>
        /// <param name="price">The price of a m^2 of a land</param>
        public Area(int number, double price, double km2)
        {
            m_nNumber = number;
            m_dPrice = price;
            m_dKm2 = km2;

            m_aRegions = new List<LandRegion>();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Get or set the number of and area
        /// </summary>
        public int Number
        {
            get { return m_nNumber; }
            set { m_nNumber = value; }
        }
        /// <summary>
        /// Get or set the price of a m^2 of a land
        /// </summary>
        public double Price
        {
            get { return m_dPrice; }
            set { m_dPrice = value; }
        }

        /// <summary>
        /// Get or set the KM2-coefficient
        /// </summary>
        public double KM2
        {
            get { return m_dKm2; }
            set { m_dKm2 = value; }
        }

        /// <summary>
        /// Get the region list. IMPROTANT: do not modify the collection using this property! Use AddRegion, RemoveRegion instead
        /// </summary>
        public List<LandRegion> Regions
        {
            get
            {
                return m_aRegions;
            }
        }

        /// <summary>
        /// Get the name for the object which is displayed in the search dialogs
        /// </summary>
        public string DisplayName
        {
            get { return m_nNumber.ToString(); }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Add a region to an area
        /// </summary>
        /// <param name="region">The region</param>
        /// <returns></returns>
        public bool AddRegion(LandRegion region)
        {
            bool bRet = true;

            if (m_aRegions.Contains(region))
            {
                bRet = false;
            }
            else
            {
                if (region.ParentArea != null)
                {
                    region.ParentArea.RemoveRegion(region);
                }
                m_aRegions.Add(region);
                region.ParentArea = this;
            }

            return bRet;
        }

        /// <summary>
        /// Remove a region from an area
        /// </summary>
        /// <param name="region">The region</param>
        /// <returns></returns>
        public bool RemoveRegion(LandRegion region)
        {
            bool bRet = true;

            if (m_aRegions.Contains(region))
            {
                bRet = m_aRegions.Remove(region);
                region.ParentArea = null;
            }
            else
            {
                bRet = false;
            }

            return bRet;
        }

        public override string ToString()
        {
            return m_nNumber.ToString();
        }

        #endregion Methods
    }
}
