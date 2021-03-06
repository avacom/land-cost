﻿using System;
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
        double m_dIndexCoefAgriculture;
        double m_dIndexCoefArable;
        List<Area> m_aAreas;
        List<LandRegion> m_aRegions;
        List<FunctionalUsage> m_aFunctionalUsages;
        List<LocalCoefficient> m_aLocalCoefficients;
        List<Document> m_aDocuments;
        List<string> m_aExecutors;
        List<string> m_aChiefs;
        List<string> m_aLandCategories;
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
            m_dIndexCoefAgriculture = 0;
            m_dIndexCoefArable = 0;
            m_aAreas = new List<Area>();
            m_aRegions = new List<LandRegion>();
            m_aFunctionalUsages = new List<FunctionalUsage>();
            m_aLocalCoefficients = new List<LocalCoefficient>();
            m_aDocuments = new List<Document>();
            m_aExecutors = new List<string>();
            m_aChiefs = new List<string>();
            m_aLandCategories = new List<string>();
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
        /// Get or set the coefficient of indexation for the agricultural lands
        /// </summary>
        public double IndexCoefficientAgriculture
        {
            get { return m_dIndexCoefAgriculture; }
            set { m_dIndexCoefAgriculture = value; }
        }

        /// <summary>
        /// Get or set the coefficient of indexation for the arable lands
        /// </summary>
        public double IndexCoefficientArable
        {
            get { return m_dIndexCoefArable; }
            set { m_dIndexCoefArable = value; }
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
        /// Get or set the list of land categories
        /// </summary>
        public List<string> LandCategories
        {
            get { return m_aLandCategories; }
            set { m_aLandCategories = value; }
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
            LocalCoefficients.Clear();
            Areas.Clear();
            FunctionalUsages.Clear();
            Documents.Clear();
            Executors.Clear();
            Chiefs.Clear();
            Regions.Clear();

            string[] cols = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R" };

            bool bRet = true;

            bRet = ExcelHelper.Init(path);
            if (bRet)
            {
                // Sheet 1
                // Local coefficients
                for (int i = 6; i < cols.Length; i++)
                {
                    string lcName = ExcelHelper.GetValue(1, cols[i] + "1").Trim();
                    LocalCoefficient coef = new LocalCoefficient(lcName);
                    LocalCoefficients.Add(coef);
                }

                // Go through the table
                int cnt = 2;
                while (true)
                {
                    string s = ExcelHelper.GetValue(1, string.Format("A{0}", cnt)).Trim();
                    if (string.IsNullOrEmpty(s))
                    {
                        break;
                    }

                    // Area
                    int areaNum = -1;
                    int.TryParse(ExcelHelper.GetValue(1, string.Format("A{0}", cnt)).Trim(), out areaNum);
                    Area curArea = null;
                    if (areaNum > 0)
                    {
                        curArea = GetAreaByNumber(areaNum);

                        if (curArea == null)
                        {
                            double km2 = -1;
                            double price = -1;

                            double.TryParse(ExcelHelper.GetValue(1, string.Format("B{0}", cnt)).Trim(), out km2);
                            double.TryParse(ExcelHelper.GetValue(1, string.Format("C{0}", cnt)).Trim(), out price);

                            if (km2 >= 0 && price >= 0)
                            {
                                curArea = new Area(areaNum, price, km2);
                                Areas.Add(curArea);
                            }
                        }
                    }

                    // LandRegion
                    int regNum = -1;
                    int.TryParse(ExcelHelper.GetValue(1, string.Format("D{0}", cnt)).Trim(), out regNum);
                    LandRegion curReg = null;
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
                        string funcUsage = ExcelHelper.GetValue(1, string.Format("E{0}", cnt)).Trim();
                        FunctionalUsage curFU = GetFunctionalUsageByName(funcUsage);
                        if (curFU == null)
                        {
                            double kf = -1;
                            double.TryParse(ExcelHelper.GetValue(1, string.Format("F{0}", cnt)).Trim(), out kf);
                            if (kf >= 0)
                            {
                                curFU = new FunctionalUsage(funcUsage, kf);
                                FunctionalUsages.Add(curFU);
                            }
                        }

                        // If a functional usage exists - create FunctionalUsageCoefficients
                        if (curFU != null)
                        {
                            FunctionalUsageCoefficients fuc = new FunctionalUsageCoefficients(curFU);
                            for (int i = 6; i < cols.Length; i++)
                            {
                                double val = -1;
                                double.TryParse(ExcelHelper.GetValue(1, string.Format("{0}{1}", cols[i], cnt)).Trim(), out val);
                                if (val >= 0)
                                {
                                    LocalCoefficientValue lcv = new LocalCoefficientValue(LocalCoefficients[i - 6], val);
                                    fuc.AddCoefficientValue(lcv);
                                }
                            }
                            if (fuc.LocalCoefficientValues.Count > 0)
                            {
                                curReg.AddFunctionalUsageCoefficients(fuc);
                            }
                        }
                    }
                    cnt++;
                }

                // Sheet 2
                string sAgency = ExcelHelper.GetValue(2, "B1").Trim();
                string sAddress = ExcelHelper.GetValue(2, "B2").Trim();

                double dIndCoef = -1;
                double.TryParse(ExcelHelper.GetValue(2, "B3").Trim(), out dIndCoef);

                double dIndCoefAgriculture = -1;
                double.TryParse(ExcelHelper.GetValue(2, "B4").Trim(), out dIndCoefAgriculture);

                double dIndCoefArable = -1;
                double.TryParse(ExcelHelper.GetValue(2, "B5").Trim(), out dIndCoefArable);

                this.AgencyName = sAgency;
                this.AgencyAddress = sAddress;
                this.IndexCoefficient = dIndCoef;
                this.IndexCoefficientAgriculture = dIndCoefAgriculture;
                this.IndexCoefficientArable = dIndCoefArable;

                // Sheet 3
                cnt = 1;
                while (true)
                {
                    string s = ExcelHelper.GetValue(3, string.Format("A{0}", cnt)).Trim();
                    if (string.IsNullOrEmpty(s))
                    {
                        break;
                    }

                    if (!Executors.Contains(s))
                    {
                        Executors.Add(s);
                    }
                    cnt++;
                }

                // Sheet 4
                cnt = 1;
                while (true)
                {
                    string s = ExcelHelper.GetValue(4, string.Format("A{0}", cnt)).Trim();
                    if (string.IsNullOrEmpty(s))
                    {
                        break;
                    }

                    if (!Chiefs.Contains(s))
                    {
                        Chiefs.Add(s);
                    }
                    cnt++;
                }

                // Sheet 5
                cnt = 1;
                while (true)
                {
                    string s = ExcelHelper.GetValue(5, string.Format("A{0}", cnt)).Trim();
                    if (string.IsNullOrEmpty(s))
                    {
                        break;
                    }

                    Document d = GetDocumentByName(s);
                    if (d == null)
                    {
                        d = new Document(s, string.Empty);
                        Documents.Add(d);
                    }
                    cnt++;
                }

                // Sheet 6
                cnt = 1;
                while (true)
                {
                    string s = ExcelHelper.GetValue(6, string.Format("A{0}", cnt)).Trim();
                    if (string.IsNullOrEmpty(s))
                    {
                        break;
                    }

                    if (!LandCategories.Contains(s))
                    {
                        LandCategories.Add(s);
                    }
                    cnt++;
                }
            }

            bRet = ExcelHelper.Close();

            // Several retries to close Excel
            int reties = 0;
            if (!bRet)
            {
                bool b = false;
                while (!b)
                {
                    if (reties >= 10)
                    {
                        break;
                    }
                    b = ExcelHelper.Close();
                    reties++;
                }
                bRet = b;
            }

            return bRet;
        }

        /// <summary>
        /// Get the document by its name
        /// </summary>
        /// <param name="number">required number</param>
        /// <returns></returns>
        private Document GetDocumentByName(string name)
        {
            Document ret = null;
            foreach (Document d in Documents)
            {
                if (d.Name == name)
                {
                    ret = d;
                    break;
                }
            }
            return ret;
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
        public Area GetAreaByNumber(int number)
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

        private void AddMissing()
        {
            FunctionalUsage f1 = new FunctionalUsage("Земельна ділянка, інформація про яку не внесена до відомостей Державного земельного кадастру", 2);
            FunctionalUsage f2 = new FunctionalUsage("У відомостях Державного земельного кадастру відсутній код Класифікації видів цільового призначення земель", 2);

            FunctionalUsages.Add(f1);
            FunctionalUsages.Add(f2);

            FunctionalUsageCoefficients fc1 = new FunctionalUsageCoefficients(f1);
            FunctionalUsageCoefficients fc2 = new FunctionalUsageCoefficients(f2);
            foreach (LocalCoefficient lc in LocalCoefficients)
            {
                LocalCoefficientValue lcv1 = new LocalCoefficientValue(lc, 1);
                LocalCoefficientValue lcv2 = new LocalCoefficientValue(lc, 1);
                fc1.AddCoefficientValue(lcv1);
                fc2.AddCoefficientValue(lcv2);
            }

            foreach (LandRegion reg in Regions)
            {
                reg.AddFunctionalUsageCoefficients(fc1);
                reg.AddFunctionalUsageCoefficients(fc2);
            }
        }
        #endregion Methods
    }
}
