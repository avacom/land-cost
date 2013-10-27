﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.IO;

namespace LandCost.Entities
{
    [Serializable]
    public class Certification : INotifyPropertyChanged
    {
        #region Members
        bool m_bLoaded;

        string m_sAgency;
        string m_sAgencyAddress;

        string m_sNumber;
        DateTime m_dtDate;

        string m_sOwner;
        string m_sOwnerLocation;
        string m_sAddress;
        string m_sLandName;

        double m_dSquare;
        string m_sDocument;
        string m_sDocumentDetails;
        string m_sArea;
        double m_dKm2;
        double m_dPrice;

        bool m_bSideActive;

        List<LocalCoefficientValue> m_aCoefVals;

        double m_dKm3;

        double m_dKf_main;
        string m_sKfName_main;
        double m_dKf_side;
        string m_sKfName_side;

        double m_dKi;
        double m_dNormEvalM2_main;
        double m_dNormEvalM2_side;

        double m_dTotalNormEval_main;
        double m_dTotalNormEval_side;

        string m_sExecutor;
        string m_sChief;

        #endregion Members

        #region Constructors

        public Certification()
        {
            m_bLoaded = false;

            SetZero();

            m_bLoaded = true;
        }

        #endregion Constructors

        #region Properties

        public string Agency
        {
            get
            {
                return m_sAgency;
            }
            set
            {
                m_sAgency = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Agency"));
                OnChanged(this, null);
            }
        }

        public string AgencyAddress
        {
            get
            {
                return m_sAgencyAddress;
            }
            set
            {
                m_sAgencyAddress = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AgencyAddress"));
                OnChanged(this, null);
            }
        }

        public string Number
        {
            get
            {
                return m_sNumber;
            }
            set
            {
                m_sNumber = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Number"));
                OnChanged(this, null);
            }
        }

        public DateTime Date
        {
            get
            {
                return m_dtDate;
            }
            set
            {
                m_dtDate = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Date"));
                OnChanged(this, null);
            }
        }

        public string Owner
        {
            get
            {
                return m_sOwner;
            }
            set
            {
                m_sOwner = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Owner"));
                OnChanged(this, null);
            }
        }

        public string OwnerLocation
        {
            get
            {
                return m_sOwnerLocation;
            }
            set
            {
                m_sOwnerLocation = value;
                OnPropertyChanged(new PropertyChangedEventArgs("OwnerLocation"));
                OnChanged(this, null);
            }
        }

        public string Address
        {
            get
            {
                return m_sAddress;
            }
            set
            {
                m_sAddress = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Address"));
                OnChanged(this, null);
            }
        }

        public string LandName
        {
            get
            {
                return m_sLandName;
            }
            set
            {
                m_sLandName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LandName"));
                OnChanged(this, null);
            }
        }

        public string Document
        {
            get
            {
                return m_sDocument;
            }
            set
            {
                m_sDocument = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Document"));
                OnChanged(this, null);
            }
        }

        public string DocumentDetails
        {
            get
            {
                return m_sDocumentDetails;
            }
            set
            {
                m_sDocumentDetails = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DocumentDetails"));
                OnChanged(this, null);
            }
        }

        public double Square
        {
            get
            {
                return m_dSquare;
            }
            set
            {
                m_dSquare = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Square"));
                Recalculate();
                OnChanged(this, null);
            }
        }

        public string Area
        {
            get
            {
                return m_sArea;
            }
            set
            {
                m_sArea = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Area"));
                OnChanged(this, null);
            }
        }

        public double Km2
        {
            get
            {
                return m_dKm2;
            }
            set
            {
                m_dKm2 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Km2"));
                OnChanged(this, null);
            }
        }

        public double Price
        {
            get
            {
                return m_dPrice;
            }
            set
            {
                m_dPrice = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Price"));
                Recalculate();
                OnChanged(this, null);
            }
        }

        public List<LocalCoefficientValue> CoefficientValues
        {
            get
            {
                return m_aCoefVals;
            }
            set
            {
                m_aCoefVals = value;
                Recalculate();
                OnChanged(this, null);
            }
        }

        public double Km3
        {
            get
            {
                return m_dKm3;
            }
            set
            {
                m_dKm3 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Km3"));
                OnChanged(this, null);
            }
        }

        public double KfMain
        {
            get
            {
                return m_dKf_main;
            }
            set
            {
                m_dKf_main = value;
                OnPropertyChanged(new PropertyChangedEventArgs("KfMain"));
                Recalculate();
                OnChanged(this, null);
            }
        }

        public string KfNameMain
        {
            get
            {
                return m_sKfName_main;
            }
            set
            {
                m_sKfName_main = value;
                OnPropertyChanged(new PropertyChangedEventArgs("KfNameMain"));
                OnChanged(this, null);
            }
        }

        public double KfSide
        {
            get
            {
                return m_dKf_side;
            }
            set
            {
                m_dKf_side = value;
                OnPropertyChanged(new PropertyChangedEventArgs("KfSide"));
                Recalculate();
                OnChanged(this, null);
            }
        }

        public string KfNameSide
        {
            get
            {
                return m_sKfName_side;
            }
            set
            {
                m_sKfName_side = value;
                OnPropertyChanged(new PropertyChangedEventArgs("KfNameSide"));
                OnChanged(this, null);
            }
        }

        public double IndexCoefficient
        {
            get
            {
                return m_dKi;
            }
            set
            {
                m_dKi = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IndexCoefficient"));
                OnChanged(this, null);
            }
        }

        public double NormEvalM2Main
        {
            get
            {
                return m_dNormEvalM2_main;
            }
            set
            {
                m_dNormEvalM2_main = value;
                OnPropertyChanged(new PropertyChangedEventArgs("NormEvalM2Main"));
                OnChanged(this, null);
            }
        }

        public double NormEvalM2Side
        {
            get
            {
                return m_dNormEvalM2_side;
            }
            set
            {
                m_dNormEvalM2_side = value;
                OnPropertyChanged(new PropertyChangedEventArgs("NormEvalM2Side"));
                OnChanged(this, null);
            }
        }

        public double TotalNormEvalMain
        {
            get
            {
                return m_dTotalNormEval_main;
            }
            set
            {
                m_dTotalNormEval_main = value;
                OnPropertyChanged(new PropertyChangedEventArgs("TotalNormEvalMain"));
                OnChanged(this, null);
            }
        }

        public double TotalNormEvalSide
        {
            get
            {
                return m_dTotalNormEval_side;
            }
            set
            {
                m_dTotalNormEval_side = value;
                OnPropertyChanged(new PropertyChangedEventArgs("TotalNormEvalSide"));
                OnChanged(this, null);
            }
        }

        public bool SideActive
        {
            get
            {
                return m_bSideActive;
            }
            set
            {
                m_bSideActive = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SideActive"));
                OnChanged(this, null);
            }
        }

        public string Executor
        {
            get
            {
                return m_sExecutor;
            }
            set
            {
                m_sExecutor = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Executor"));
                OnChanged(this, null);
            }
        }

        public string Chief
        {
            get
            {
                return m_sChief;
            }
            set
            {
                m_sChief = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Chief"));
                OnChanged(this, null);
            }
        }

        #endregion Properties

        #region Methods

        public void Save(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Certification));
            TextWriter tw = new StreamWriter(filename);
            serializer.Serialize(tw, this);
            tw.Close(); 
        }

        public void Recalculate()
        {
            if (m_bLoaded)
            {
                // Calculate Km3
                double dKm3 = 1;
                if (m_aCoefVals != null)
                {
                    foreach (LocalCoefficientValue lcv in m_aCoefVals)
                    {
                        dKm3 *= lcv.Value;
                    }
                }
                Km3 = Math.Round(dKm3, 4);

                // Calculate NormEvalM2
                double dEvalM2Main = Price * dKm3 * KfMain * IndexCoefficient;
                double dEvalM2Side = Price * dKm3 * KfSide * IndexCoefficient;
                NormEvalM2Main = Math.Round(dEvalM2Main, 2);
                NormEvalM2Side = Math.Round(dEvalM2Side, 2);

                // Calculate TotalNormEval
                TotalNormEvalMain = Math.Round(dEvalM2Main * Square, 2);
                TotalNormEvalSide = Math.Round(dEvalM2Side * Square, 2);
            }
        }

        public void SetZero()
        {
            Agency = string.Empty;
            AgencyAddress = string.Empty;
            Number = string.Empty;
            Date = DateTime.Now;
            Owner = string.Empty;
            OwnerLocation = string.Empty;
            Address = string.Empty;
            LandName = string.Empty;
            Square = 0;
            Document = string.Empty;
            DocumentDetails = string.Empty;
            Area = string.Empty;
            Km2 = 0;
            Price = 0;
            SideActive = false;
            CoefficientValues = new List<LocalCoefficientValue>();
            Km3 = 0;
            KfMain = 0;
            KfNameMain = string.Empty;
            KfSide = 0;
            KfNameSide = string.Empty;
            IndexCoefficient = 0;
            NormEvalM2Main = 0;
            NormEvalM2Side = 0;
            TotalNormEvalMain = 0;
            TotalNormEvalSide = 0;
            Executor = string.Empty;
            Chief = string.Empty;
        }

        #endregion Methods

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, e);
        }

        public event EventHandler Changed;
        public void OnChanged(object sender, EventArgs e)
        {
            if (Changed != null)
            {
                Changed(sender, e);
            }
        }
        #endregion Events
    }
}