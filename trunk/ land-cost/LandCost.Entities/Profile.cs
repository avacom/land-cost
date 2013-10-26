using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET.WindowsForms;
using LandCost.Entities.Helpers;

namespace LandCost.Entities
{
    /// <summary>
    /// Represents the evaluation profile - set of parameters used to evaluate the land
    /// </summary>
    public class Profile : IControlledRemover
    {
        #region Members

        string m_sName;
        string m_sAgencyName;
        string m_sAgencyAddress;
        double m_dIndexCoef;
        List<Area> m_aAreas;
        List<LandRegion> m_aRegions;
        List<FunctionalUsage> m_aFunctionalUsages;
        List<LocalCoefficient> m_aLocalCoefficients;
        List<Document> m_aDocuments;
        List<string> m_aExecutors;
        List<string> m_aChiefs;
        Map m_Map;

        #endregion Members

        #region Constructors

        /// <summary>
        /// Create the instance of a Profile
        /// </summary>
        /// <param name="name">The profile name</param>
        public Profile(string name)
        {
            m_sName = name;
            m_sAgencyAddress = string.Empty;
            m_sAgencyName = string.Empty;
            m_dIndexCoef = 0;
            m_aAreas = new List<Area>();
            m_aRegions = new List<LandRegion>();
            m_aFunctionalUsages = new List<FunctionalUsage>();
            m_aLocalCoefficients = new List<LocalCoefficient>();
            m_aDocuments = new List<Document>();
            m_aExecutors = new List<string>();
            m_aChiefs = new List<string>();
            m_Map = null;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Get or set the name of a profile
        /// </summary>
        public string Name
        {
            get { return m_sName; }
            set { m_sName = value; }
        }

        /// <summary>
        /// Get or set the agency name
        /// </summary>
        public string AgencyName
        {
            get { return m_sAgencyName; }
            set { m_sAgencyName = value; }
        }

        /// <summary>
        /// Get or set the agency address
        /// </summary>
        public string AgencyAddress
        {
            get { return m_sAgencyAddress; }
            set { m_sAgencyAddress = value; }
        }

        /// <summary>
        /// Get or set the coefficient of indexation
        /// </summary>
        public double IndexCoefficient
        {
            get { return m_dIndexCoef; }
            set { m_dIndexCoef = value; }
        }

        /// <summary>
        /// Get the list of areas
        /// </summary>
        public List<Area> Areas
        {
            get { return m_aAreas; }
        }

        /// <summary>
        /// Get the list of regions
        /// </summary>
        public List<LandRegion> Regions
        {
            get { return m_aRegions; }
        }

        /// <summary>
        /// Get the list of functional usages
        /// </summary>
        public List<FunctionalUsage> FunctionalUsages
        {
            get { return m_aFunctionalUsages; }
        }

        /// <summary>
        /// Get the list of local coefficients
        /// </summary>
        public List<LocalCoefficient> LocalCoefficients
        {
            get { return m_aLocalCoefficients; }
        }

        /// <summary>
        /// Get the list of property documents
        /// </summary>
        public List<Document> Documents
        {
            get { return m_aDocuments; }
        }

        /// <summary>
        /// Get the list of executors
        /// </summary>
        public List<string> Executors
        {
            get { return m_aExecutors; }
        }

        /// <summary>
        /// Get the list of chiefs
        /// </summary>
        public List<string> Chiefs
        {
            get { return m_aChiefs; }
        }

        /// <summary>
        /// Get or set the region map of a profile
        /// </summary>
        public Map RegionMap
        {
            get
            {
                return m_Map;
            }
            set
            {
                m_Map = value;
            }
        }

        /// <summary>
        /// Get the list of the Map polygons bound to the regions
        /// </summary>
        public List<LandPolygon> BoundPolygons
        {
            get
            {
                List<LandPolygon> regPols = RegionPolygons;
                List<LandPolygon> ret = new List<LandPolygon>();
                foreach (LandPolygon pol in m_Map.Polygons)
                {
                    if (regPols.Contains(pol))
                    {
                        ret.Add(pol);
                    }
                }
                return ret;
            }
        }

        /// <summary>
        /// Get the list of the Map polygons not bound to the regions
        /// </summary>
        public List<LandPolygon> UnboundPolygons
        {
            get
            {
                List<LandPolygon> regPols = RegionPolygons;
                List<LandPolygon> ret = new List<LandPolygon>();
                foreach (LandPolygon pol in m_Map.Polygons)
                {
                    if (!regPols.Contains(pol))
                    {
                        ret.Add(pol);
                    }
                }
                return ret;
            }
        }

        /// <summary>
        /// Get the list of the region polygons
        /// </summary>
        public List<LandPolygon> RegionPolygons
        {
            get
            {
                List<LandPolygon> ret = new List<LandPolygon>();
                if (m_aRegions != null)
                {
                    foreach (LandRegion reg in m_aRegions)
                    {
                        if (reg.Polygon != null)
                        {
                            ret.Add(reg.Polygon);
                        }
                    }
                }
                return ret;
            }
        }

        /// <summary>
        /// Get the name for the object which is displayed in the search dialogs
        /// </summary>
        public string DisplayName
        {
            get { return m_sName; }
        }

        #endregion Properties

        #region Methods

        public bool LoadXls(string path)
        {
            string[] cols = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R" };

            bool bRet = true;

            bRet = ExcelHelper.Init(path);
            if (bRet)
            {
                // Local coefficients
                LocalCoefficients.Clear();

                for (int i = 6; i < cols.Length; i++)
                {
                    string lcName = ExcelHelper.GetValue(cols[i] + "2");
                    LocalCoefficient coef = new LocalCoefficient(lcName);
                    LocalCoefficients.Add(coef);
                }

                // Go through the table
                int cnt = 3;
                while (true)
                {
                    string s = ExcelHelper.GetValue(string.Format("A{0}", cnt));
                    if (s.Contains("_____"))
                    {
                        break;
                    }

                    // Area
                    int areaNum = -1;
                    int.TryParse(ExcelHelper.GetValue(string.Format("A{0}", cnt)), out areaNum);
                    Area curArea = null;
                    if (areaNum > 0)
                    {
                        curArea = GetAreaByNumber(areaNum);

                        if (curArea == null)
                        {
                            double km2 = -1;
                            double price = -1;

                            double.TryParse(ExcelHelper.GetValue(string.Format("B{0}", cnt)), out km2);
                            double.TryParse(ExcelHelper.GetValue(string.Format("C{0}", cnt)), out price);

                            if (km2 >= 0 && price >= 0)
                            {
                                curArea = new Area(areaNum, price, km2);
                                Areas.Add(curArea);
                            }
                        }
                    }

                    // LandRegion
                    int regNum = -1;
                    int.TryParse(ExcelHelper.GetValue(string.Format("D{0}", cnt)), out regNum);
                    LandRegion curReg = 0;
                    if (regNum > 0)
                    {
                        curReg = GetRegionByNumber(regNum);

                        if (curReg == null)
                        {
                            curReg = new LandRegion(regNum);
                            Regions.Add(curReg);
                            if (curArea != null)
                            {
                                curArea.AddRegion(curReg);
                            }
                        }
                    }

                    // The rest is possible only when the region is not null
                    if (curReg != null)
                    {
                        string funcUsage = ExcelHelper.GetValue(string.Format("E{0}", cnt));
                        FunctionalUsage curFU = GetFunctionalUsageByName(funcUsage);
                        if (curFU == null)
                        {
                            double kf = -1;
                            double.TryParse(ExcelHelper.GetValue(string.Format("F{0}", cnt)), out kf);
                            if (kf >= 0)
                            {
                                curFU = new FunctionalUsage(funcUsage, kf);
                                FunctionalUsages.Add(curFU);
                            }
                        }

                        // If a functional usage exists - create FunctionalUsageCoefficients
                        if (curFU != null)
                        {
                            
                        }
                    }
                }
            }
            bRet = ExcelHelper.Close();

            return bRet;
        }

        /// <summary>
        /// Get the functional usage by its name
        /// </summary>
        /// <param name="number">required number</param>
        /// <returns></returns>
        private FunctionalUsage GetFunctionalUsageByName(string name)
        {
            FunctionalUsage ret = null;
            foreach (FunctionalUsage u in FunctionalUsages)
            {
                if (u.Name == name)
                {
                    ret = u;
                    break;
                }
            }
            return ret;
        }

        /// <summary>
        /// Get the area by its number
        /// </summary>
        /// <param name="number">required number</param>
        /// <returns></returns>
        private Area GetAreaByNumber(int number)
        {
            Area ret = null;
            foreach (Area a in Areas)
            {
                if (a.Number == number)
                {
                    ret = a;
                    break;
                }
            }
            return ret;
        }

        /// <summary>
        /// Get the region by its number
        /// </summary>
        /// <param name="number">required number</param>
        /// <returns></returns>
        private LandRegion GetRegionByNumber(int number)
        {
            LandRegion ret = null;
            foreach (LandRegion r in Regions)
            {
                if (r.Number == number)
                {
                    ret = r;
                    break;
                }
            }
            return ret;
        }
        /// <summary>
        /// Deletes all the references of the item
        /// </summary>
        /// <param name="item">the item that will be removed</param>
        public void PrepareRemoval(object item)
        {
            if (item != null)
            {
                if (item is Area)
                {
                    Area a = item as Area;

                    List<LandRegion> regs = new List<LandRegion>();
                    regs.AddRange(a.Regions);
                    foreach (LandRegion region in regs)
                    {
                        a.RemoveRegion(region);
                    }
                }
                else if (item is LandRegion)
                {
                    LandRegion r = item as LandRegion;
                    if (r.ParentArea != null)
                    {
                        r.ParentArea.RemoveRegion(r);
                    }
                }
                else if (item is FunctionalUsage)
                {
                    FunctionalUsage fu = item as FunctionalUsage;
                    foreach (LandRegion reg in this.Regions)
                    {
                        List<FunctionalUsageCoefficients> fucs = reg.GetFUCByUsage(fu);
                        foreach (FunctionalUsageCoefficients fuc in fucs)
                        {
                            reg.RemoveFunctionalUsageCoefficients(fuc);
                        }
                    }
                }
                else if (item is LocalCoefficient)
                {
                    LocalCoefficient lc = item as LocalCoefficient;
                    foreach (LandRegion reg in this.Regions)
                    {
                        foreach (FunctionalUsage fu in this.FunctionalUsages)
                        {
                            List<FunctionalUsageCoefficients> fucs = reg.GetFUCByUsage(fu);
                            foreach (FunctionalUsageCoefficients fuc in fucs)
                            {
                                fuc.RemoveCoefficientValueByCoef(lc);
                                if (fuc.LocalCoefficientValues.Count == 0)
                                {
                                    reg.RemoveFunctionalUsageCoefficients(fuc);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Unbinds the polygon from the LandRegion
        /// </summary>
        /// <param name="polygon">The polygon to unbind</param>
        public void UnbindPolygon(LandPolygon polygon)
        {
            if (polygon != null)
            {
                foreach (LandRegion region in m_aRegions)
                {
                    if (region.Polygon != null && region.Polygon.Equals(polygon))
                    {
                        region.Polygon = null;
                    }
                }
            }
        }

        /// <summary>
        /// Get the region which contains the specified LandPolygon
        /// </summary>
        /// <param name="polygon">required LandPolygon</param>
        /// <returns></returns>
        public LandRegion GetRegionByPolygon(LandPolygon polygon)
        {
            LandRegion res = null;
            if (polygon != null)
            {
                foreach (LandRegion reg in m_aRegions)
                {
                    if (reg.Polygon != null && reg.Polygon.Equals(polygon))
                    {
                        res = reg;
                        break;
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// Get the region which contains the specified GMapPolygon
        /// </summary>
        /// <param name="polygon">required GMapPolygon</param>
        /// <returns></returns>
        public LandRegion GetRegionByPolygon(GMapPolygon polygon)
        {
            LandPolygon pol = PolygonHelper.FindPolygon(polygon, RegionPolygons);
            return GetRegionByPolygon(pol);
        }

        #endregion Methods
    }
}
