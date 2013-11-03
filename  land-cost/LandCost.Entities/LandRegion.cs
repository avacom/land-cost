using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET.WindowsForms;
using GMap.NET;
using netDxf.Entities;
using LandCost.Entities.Helpers;
using System.Drawing;

namespace LandCost.Entities
{
    /// <summary>
    /// Represents the regions in which the land is divided
    /// </summary>
    [Serializable]
    public class LandRegion
    {
        #region Members

        int m_nNumber;
        LandPolygon m_oPolygon;
        Area m_oArea;
        List<FunctionalUsageCoefficients> m_aUsagesCoefsVals;

        #endregion Members

        #region Constructors

        /// <summary>
        /// Create the basic instance of a Region
        /// </summary>
        /// <param name="number">The number of a region</param>
        public LandRegion(int number)
        {
            Initialize(number, null, null);
        }
        /// <summary>
        /// Create the instance of a Region
        /// </summary>
        /// <param name="number">The number of a region</param>
        /// <param name="area">The parent area of a region</param>
        /// <param name="polygon">The outline polygon of a region on a map (lat/long coordinates)</param>
        public LandRegion(int number, Area area, LandPolygon polygon)
        {
            Initialize(number, area, polygon);
        }

        /// <summary>
        /// Create the instance of a Region
        /// </summary>
        /// <param name="number">The number of a region</param>
        /// <param name="area">The parent area of a region</param>
        /// <param name="points">The vertices of a region on a map (lat/long coordinates)</param>
        public LandRegion(int number, Area area, List<PointLatLng> points)
        {
            LandPolygon polygon = new LandPolygon(number.ToString(), points);
            Initialize(number, area, polygon);
        }

        /// <summary>
        /// Create the instance of a Region
        /// </summary>
        /// <param name="number">The number of a region</param>
        /// <param name="area">The parent area of a region</param>
        /// <param name="polyline">The outline polyline of a region got from the DXF-file (Mercator projection coordinates)</param>
        /// <param name="deltaX">The shift for the X-axis</param>
        /// <param name="deltaY">The shift for the Y-axis</param>
        public LandRegion(int number, Area area, LwPolyline polyline, double deltaX, double deltaY)
        {
            LandPolygon polygon = GetPolygonByPolyline(polyline, deltaX, deltaY);
            Initialize(number, area, polygon);
        }

        /// <summary>
        /// Initialize a region
        /// </summary>
        /// <param name="number">The number of a region</param>
        /// <param name="area">The parent area of a region</param>
        /// <param name="polygon">The outline polygon of a region on a map (lat/long coordinates)</param>
        private void Initialize(int number, Area area, LandPolygon polygon)
        {
            m_aUsagesCoefsVals = new List<FunctionalUsageCoefficients>();
            m_nNumber = number;
            if (polygon != null)
            {
                polygon.Name = number.ToString();
            }
            m_oArea = area;
            m_oPolygon = polygon;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Get or set the number of a region
        /// </summary>
        public int Number
        {
            get { return m_nNumber; }
            set { m_nNumber = value; }
        }

        /// <summary>
        /// Get or set the parent area of a region
        /// </summary>
        public Area ParentArea
        {
            get { return m_oArea; }
            set { m_oArea = value; }
        }

        /// <summary>
        /// Get the price of a m^2 of a land
        /// </summary>
        public double Price
        {
            get { return m_oArea == null ? 0 : m_oArea.Price; }
        }

        /// <summary>
        /// Get or set the polygon of a region (lat/long coordinates)
        /// </summary>
        public LandPolygon Polygon
        {
            get { return m_oPolygon; }
            set { m_oPolygon = value; }
        }

        /// <summary>
        /// Get or set the functional usages + the list of local coefficients and their values
        /// </summary>
        public List<FunctionalUsageCoefficients> FunctionalUsagesCoefficients
        {
            get
            {
                return m_aUsagesCoefsVals;
            }
            set
            {
                m_aUsagesCoefsVals = value;
            }
        }

        /// <summary>
        /// Get the name for the object which is displayed in the search dialogs
        /// </summary>
        public string DisplayName
        {
            get { return m_nNumber.ToString(); }
        }

        // Returns 'Yes' or 'No' string depending on whether a polygon is bound to the region
        public string Bound
        {
            get
            {
                return m_oPolygon != null ? "Так" : "Ні";
            }
        }
        #endregion Properties

        #region Methods

        /// <summary>
        /// Returns the LandPolygon (lat/long coordinates) from a LwPolyline (Mercator projection coordinates) using the X / Y shifts
        /// </summary>
        /// <param name="name">The name of a polygon</param>
        /// <param name="polyline">Source polyline</param>
        /// <param name="deltaX">The shift for the X-axis</param>
        /// <param name="deltaY">The shift for the Y-axis</param>
        /// <returns></returns>
        private LandPolygon GetPolygonByPolyline(string name, LwPolyline polyline, double deltaX, double deltaY)
        {
            List<PointLatLng> points = new List<PointLatLng>();
            if (polyline != null)
            {
                foreach (LwPolylineVertex vertex in polyline.Vertexes)
                {
                    double[] coords = MercProj.ToGeoCoord(vertex.Location.X + deltaX, vertex.Location.Y + deltaY);
                    PointLatLng point = new PointLatLng(coords[1], coords[0]);
                    points.Add(point);
                }
            }
            LandPolygon polygon = new LandPolygon("region", points);
            return polygon;
        }

        /// <summary>
        /// Returns the LandPolygon (lat/long coordinates) from a LwPolyline (Mercator projection coordinates) using the X / Y shifts
        /// </summary>
        /// <param name="polyline">Source polyline</param>
        /// <param name="deltaX">The shift for the X-axis</param>
        /// <param name="deltaY">The shift for the Y-axis</param>
        /// <returns></returns>
        private LandPolygon GetPolygonByPolyline(LwPolyline polyline, double deltaX, double deltaY)
        {
            return GetPolygonByPolyline(m_nNumber.ToString(), polyline, deltaX, deltaY);
        }

        /// <summary>
        /// Sets the polygon (lat/long coordinates) for the region using a polyline (Mercator projection coordinates) and the X / Y shifts
        /// </summary>
        /// <param name="polyline">The polyline</param>
        /// <param name="deltaX">The shift for the X-axis</param>
        /// <param name="deltaY">The shift for the Y-axis</param>
        public void SetPolygon(LwPolyline polyline, double deltaX, double deltaY)
        {
            m_oPolygon = GetPolygonByPolyline(polyline, deltaX, deltaY);
        }

        /// <summary>
        /// Add the functional usage + the list of local coefficients and their values
        /// </summary>
        /// <param name="usageCoefs">The functional usage + the list of local coefficients and their values</param>
        /// <returns></returns>
        public bool AddFunctionalUsageCoefficients(FunctionalUsageCoefficients usageCoefs)
        {
            bool bRet = true;

            if (m_aUsagesCoefsVals.Contains(usageCoefs))
            {
                bRet = false;
            }
            else
            {
                m_aUsagesCoefsVals.Add(usageCoefs);
            }

            return bRet;
        }

        /// <summary>
        /// Remove the functional usage + the list of local coefficients and their values
        /// </summary>
        /// <param name="usageCoefs">The functional usage + the list of local coefficients and their values</param>
        /// <returns></returns>
        public bool RemoveFunctionalUsageCoefficients(FunctionalUsageCoefficients usageCoefs)
        {
            bool bRet = true;

            if (m_aUsagesCoefsVals.Contains(usageCoefs))
            {
                bRet = m_aUsagesCoefsVals.Remove(usageCoefs);
            }
            else
            {
                bRet = false;
            }

            return bRet;
        }

        /// <summary>
        /// Get the list of functional usages assigned to the region
        /// </summary>
        public List<FunctionalUsage> FunctionalUsages
        {
            get
            {
                List<FunctionalUsage> res = new List<FunctionalUsage>();
                
                foreach (FunctionalUsageCoefficients usageCoef in m_aUsagesCoefsVals)
                {
                    res.Add(usageCoef.Usage);
                }
                
                return res;
            }
        }

        /// <summary>
        /// Get lists of local coefficients + values for the required functional usage
        /// </summary>
        /// <param name="usage">The functional usage</param>
        /// <returns></returns>
        public List<List<LocalCoefficientValue>> GetCoefficientValuesByUsage(FunctionalUsage usage)
        {
            List<List<LocalCoefficientValue>> res = new List<List<LocalCoefficientValue>>();

            foreach (FunctionalUsageCoefficients usageCoef in m_aUsagesCoefsVals)
            {
                if (usageCoef.Usage.Equals(usage))
                {
                    res.Add(usageCoef.LocalCoefficientValues);
                }
            }

            return res;
        }

        /// <summary>
        /// Get lists of local functional usages + coefficients + values for the required functional usage
        /// </summary>
        /// <param name="usage">The functional usage</param>
        /// <returns></returns>
        public List<FunctionalUsageCoefficients> GetFUCByUsage(FunctionalUsage usage)
        {
            List<FunctionalUsageCoefficients> res = new List<FunctionalUsageCoefficients>();

            foreach (FunctionalUsageCoefficients usageCoef in m_aUsagesCoefsVals)
            {
                if (usageCoef.Usage.Equals(usage))
                {
                    res.Add(usageCoef);
                }
            }

            return res;
        }

        public override string ToString()
        {
            return m_nNumber.ToString();
        }

        #endregion Methods
    }
}
