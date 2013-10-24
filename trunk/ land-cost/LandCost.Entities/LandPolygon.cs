using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;

namespace LandCost.Entities
{
    /// <summary>
    /// Represents the simplified polygon class
    /// </summary>
    public class LandPolygon
    {
        #region Members

        List<PointLatLng> m_aPoints;
        string m_sName;
        
        #endregion Members

        #region Constructors

        public LandPolygon(string name, List<PointLatLng> points)
        {
            m_aPoints = points;
            m_sName = name;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Get or set the points
        /// </summary>
        public List<PointLatLng> Points
        {
            get
            {
                return m_aPoints;
            }
            set
            {
                m_aPoints = value;
            }
        }

        /// <summary>
        /// Get or set the name of a polygon
        /// </summary>
        public string Name
        {
            get
            {
                return m_sName;
            }
            set
            {
                m_sName = value;
            }
        }

        #endregion Properties
    }
}
