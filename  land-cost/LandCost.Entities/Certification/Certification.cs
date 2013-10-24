using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

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
            }
        }

        #endregion Properties

        #region Methods

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
                Km3 = dKm3;

                // Calculate NormEvalM2
                NormEvalM2Main = Price * Km3 * KfMain * IndexCoefficient;
                NormEvalM2Side = Price * Km3 * KfSide * IndexCoefficient;

                // Calculate TotalNormEval
                TotalNormEvalMain = NormEvalM2Main * Square;
                TotalNormEvalSide = NormEvalM2Side * Square;
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, e);
        }
    }
}
