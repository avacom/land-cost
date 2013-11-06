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
        double m_dSquare;
        
        #endregion Members

        #region Constructors

        public LandPolygon(string name, List<PointLatLng> points)
        {
            m_aPoints = points;
            m_sName = name;
            m_dSquare = GetSquare();
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

        /// <summary>
        /// Get the area of a polygon
        /// </summary>
        public double Area
        {
            get { return m_dSquare; }
        }

        #endregion Properties

        #region Methods
        private double GetSquare()
        {
            double ret = 0;
            if (m_aPoints != null)
            {
                double a = 0;
                double b = 0;
                for (int i = 0; i < m_aPoints.Count; i++)
                {
                    if (i + 1 < m_aPoints.Count)
                    {
                        a += m_aPoints[i].Lng * m_aPoints[i + 1].Lat;
                        b += m_aPoints[i].Lat * m_aPoints[i + 1].Lng;
                    }
                    else
                    {
                        a += m_aPoints[i].Lng * m_aPoints[0].Lat;
                        b += m_aPoints[i].Lat * m_aPoints[0].Lng;
                    }
                }
                ret = Math.Abs((a - b) / 2);
            }
            return ret;
        }
        #endregion Methods
    }
}
