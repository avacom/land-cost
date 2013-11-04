using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.IO;
using LandCost.Entities.Helpers;

namespace LandCost.Entities
{
    [Serializable]
    public class Certification : INotifyPropertyChanged
    {
        #region Members
        bool m_bLoaded;

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

        public DateTime Date
        {
            get
            {
                return m_dtDate;
            }
            set
            {
                if (m_dtDate != value)
                {
                    m_dtDate = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Date"));
                    OnChanged(this, null);
                }
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
                if (m_sOwner != value)
                {
                    m_sOwner = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Owner"));
                    OnChanged(this, null);
                }
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
                if (m_sOwnerLocation != value)
                {
                    m_sOwnerLocation = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("OwnerLocation"));
                    OnChanged(this, null);
                }
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
                if (m_sAddress != value)
                {
                    m_sAddress = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Address"));
                    OnChanged(this, null);
                }
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
                if (m_sLandName != value)
                {
                    m_sLandName = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("LandName"));
                    OnChanged(this, null);
                }
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
                if (m_sDocument != value)
                {
                    m_sDocument = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Document"));
                    OnChanged(this, null);
                }
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
                if (m_sDocumentDetails != value)
                {
                    m_sDocumentDetails = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("DocumentDetails"));
                    OnChanged(this, null);
                }
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
                if (m_dSquare != value)
                {
                    m_dSquare = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Square"));
                    Recalculate();
                    OnChanged(this, null);
                }
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
                if (m_sArea != value)
                {
                    m_sArea = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Area"));
                    OnChanged(this, null);
                }
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
                if (m_dKm2 != value)
                {
                    m_dKm2 = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Km2"));
                    OnChanged(this, null);
                }
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
                if (m_dPrice != value)
                {
                    m_dPrice = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Price"));
                    Recalculate();
                    OnChanged(this, null);
                }
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
                if (m_dKm3 != value)
                {
                    m_dKm3 = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Km3"));
                    OnChanged(this, null);
                }
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
                if (m_dKf_main != value)
                {
                    m_dKf_main = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("KfMain"));
                    Recalculate();
                    OnChanged(this, null);
                }
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
                if (m_sKfName_main != value)
                {
                    m_sKfName_main = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("KfNameMain"));
                    OnChanged(this, null);
                }
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
                if (m_dKf_side != value)
                {
                    m_dKf_side = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("KfSide"));
                    Recalculate();
                    OnChanged(this, null);
                }
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
                if (m_sKfName_side != value)
                {
                    m_sKfName_side = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("KfNameSide"));
                    OnChanged(this, null);
                }
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
                if (m_dKi != value)
                {
                    m_dKi = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("IndexCoefficient"));
                    OnChanged(this, null);
                }
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
                if (m_dNormEvalM2_main != value)
                {
                    m_dNormEvalM2_main = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("NormEvalM2Main"));
                    OnChanged(this, null);
                }
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
                if (m_dNormEvalM2_side != value)
                {
                    m_dNormEvalM2_side = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("NormEvalM2Side"));
                    OnChanged(this, null);
                }
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
                if (m_dTotalNormEval_main != value)
                {
                    m_dTotalNormEval_main = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("TotalNormEvalMain"));
                    OnChanged(this, null);
                }
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
                if (m_dTotalNormEval_side != value)
                {
                    m_dTotalNormEval_side = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("TotalNormEvalSide"));
                    OnChanged(this, null);
                }
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
                if (m_bSideActive != value)
                {
                    m_bSideActive = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("SideActive"));
                    OnChanged(this, null);
                }
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
                if (m_sExecutor != value)
                {
                    m_sExecutor = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Executor"));
                    OnChanged(this, null);
                }
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
                if (m_sChief != value)
                {
                    m_sChief = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Chief"));
                    OnChanged(this, null);
                }
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
                Km3 = Math.Round(dKm3, 2);

                // Calculate NormEvalM2
                double dEvalM2Main = Price * Km3 * KfMain * IndexCoefficient;
                double dEvalM2Side = Price * Km3 * KfSide * IndexCoefficient;
                NormEvalM2Main = Math.Round(dEvalM2Main, 2);
                NormEvalM2Side = Math.Round(dEvalM2Side, 2);

                // Calculate TotalNormEval
                TotalNormEvalMain = Math.Round(NormEvalM2Main * Square, 2);
                TotalNormEvalSide = Math.Round(NormEvalM2Side * Square, 2);
            }
        }

        public void SetZero()
        {
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
            KfNameMain = "для земельної ділянки за цільовим призначенням згідно з проектом землеустрою";
            KfSide = 0;
            KfNameSide = "для земельної ділянки, відведеної під майбутнє будівництво та зайнятої поточним будівництвом";
            IndexCoefficient = 0;
            NormEvalM2Main = 0;
            NormEvalM2Side = 0;
            TotalNormEvalMain = 0;
            TotalNormEvalSide = 0;
            Executor = string.Empty;
            Chief = string.Empty;
        }

        public string TotalEvalMainLetters
        {
            get
            {
                return MoneyByWords.UAHPhrase((decimal)TotalNormEvalMain);
            }
        }

        public string TotalEvalSideLetters
        {
            get
            {
                return MoneyByWords.UAHPhrase((decimal)TotalNormEvalSide);
            }
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
