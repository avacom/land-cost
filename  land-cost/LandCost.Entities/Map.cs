using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET.WindowsForms;
using netDxf;
using netDxf.Entities;
using GMap.NET;
using LandCost.Entities.Helpers;
using System.Drawing;

namespace LandCost.Entities
{
    /// <summary>
    /// Represents the Map class
    /// </summary>
    public class Map
    {
        #region Members

        string m_sFilename;
        List<LandPolygon> m_aPolygons;
        PointLatLng m_Center;

        double minLat;
        double minLng;
        double maxLat;
        double maxLng;

        #endregion Members

        #region Constructors

        public Map(string filename)
        {
            m_sFilename = filename;
            m_aPolygons = new List<LandPolygon>();

            minLat = -1;
            minLng = -1;
            maxLat = -1;
            maxLng = -1;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Loads the map
        /// </summary>
        /// <returns></returns>
        public bool Load()
        {
            bool bRet = true;
            try
            {
                m_aPolygons.Clear();
                DxfDocument dxf = DxfDocument.Load(m_sFilename);

                int i = 0;
                foreach (LwPolyline p in dxf.LwPolylines)
                {
                    List<PointLatLng> pnts = new List<PointLatLng>();

                    foreach (LwPolylineVertex vertex in p.Vertexes)
                    {
                        double[] coords = MercProj.ToGeoCoord(vertex.Location.X, vertex.Location.Y);
                        PointLatLng pnt = new PointLatLng(coords[1], coords[0]);
                        if (minLat < 0)
                        {
                            minLat = coords[1];
                        }
                        if (maxLat < 0)
                        {
                            maxLat = coords[1];
                        }

                        if (minLng < 0)
                        {
                            minLng = coords[0];
                        }
                        if (maxLng < 0)
                        {
                            maxLng = coords[0];
                        }

                        if (coords[1] < minLat)
                        {
                            minLat = coords[1];
                        }
                        if (coords[1] > maxLat)
                        {
                            maxLat = coords[1];
                        }

                        if (coords[0] < minLng)
                        {
                            minLng = coords[0];
                        }
                        if (coords[0] > maxLng)
                        {
                            maxLng = coords[0];
                        }

                        pnts.Add(pnt);
                    }
                    LandPolygon pol = new LandPolygon(i.ToString(), pnts);
                    m_aPolygons.Add(pol);
                    i++;
                }

                m_Center = new PointLatLng((minLat + maxLat) / 2, (minLng + maxLng) / 2);
            }
            catch
            {
                bRet = false;
            }
            return bRet;
        }

        #endregion Methods

        #region Properties

        /// <summary>
        /// Get the DXF filename of a map
        /// </summary>
        public string FileName
        {
            get
            {
                return m_sFilename;
            }
        }

        /// <summary>
        /// Get the polygon list of a map
        /// </summary>
        public List<LandPolygon> Polygons
        {
            get
            {
                return m_aPolygons;
            }
        }

        /// <summary>
        /// Get the center of a map
        /// </summary>
        public PointLatLng Center
        {
            get
            {
                return m_Center;
            }
        }

        #endregion Properties

        #region Methods

        public bool IsInside(PointLatLng point)
        {
            bool bRet = false;

            List<PointLatLng> pntList = new List<PointLatLng>();
            pntList.Add(new PointLatLng(minLat, minLng));
            pntList.Add(new PointLatLng(minLat, maxLng));
            pntList.Add(new PointLatLng(maxLat, maxLng));
            pntList.Add(new PointLatLng(maxLat, minLng));

            GMapPolygon pol = new GMapPolygon(pntList, "test_polygon");

            bRet = pol.IsInside(point);
            return bRet;
        }
        #endregion Methods
    }
}
