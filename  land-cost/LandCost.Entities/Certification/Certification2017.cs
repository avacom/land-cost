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
    public class Certification2017 : INotifyPropertyChanged
    {
        #region Members
        bool m_bLoaded;

        DateTime m_dtDate;
        bool m_bShowDate;
        bool m_bUserCadasterNumber;

        string m_sApplicant;
        string m_sCadastreNumber;
        string m_sLandLocation;
        string m_sLandCategory;
        string m_sLandPurpose;

        double m_dSquare;
        double m_dPrice;
        string m_sArea;
        double m_dKm2;

        int m_nType;

        List<LocalCoefficientValue> m_aCoefVals;

        double m_dKm3;

        double m_dKf;
        double m_dKfReal;
        double m_dKfAdditional;

        double m_dSquareAgriculture;
        double m_dNormEvalAgriculture;
        
        double m_dKi;
        double m_dNormEval;

        string m_sExecutor;
        
        #endregion Members

        #region Constructors

        public Certification2017()
        {
            m_bLoaded = false;

            SetZero();

            m_bLoaded = true;
        }

        public Certification2017(Certification old, string category, string executor)
        {
            m_bLoaded = false;

            Date = old.Date;
            ShowDate = !old.HideNumberDate;
            UseCadasterNumber = false;
            Applicant = old.Owner;
            CadasterNumber = "відсутній";
            LandLocation = old.Address;
            LandCategory = category;
            LandPurpose = old.LandName;
            LandType = 0;
            Square = old.Square;
            Price = old.Price;
            Area = old.Area;
            Km2 = old.Km2;
            CoefficientValues = old.CoefficientValues;
            Km3 = old.Km3;
            Kf = old.KfMain;
            KfReal = old.KfMain;
            KfAdditional = 0;
            SquareAgriculture = 0;
            NormEvalAgriculture = 0;
            IndexCoefficient = old.IndexCoefficient;
            Executor = executor;
            m_bLoaded = true;

            Recalculate();
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

        public bool ShowDate
        {
            get
            {
                return m_bShowDate;
            }
            set
            {
                if (m_bShowDate != value)
                {
                    m_bShowDate = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ShowDate"));
                    OnChanged(this, null);
                }
            }
        }

        public bool UseCadasterNumber
        {
            get
            {
                return m_bUserCadasterNumber;
            }
            set
            {
                if (m_bUserCadasterNumber != value)
                {
                    m_bUserCadasterNumber = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("UseCadasterNumber"));
                    OnChanged(this, null);
                }
            }
        }

        public string Applicant
        {
            get
            {
                return m_sApplicant;
            }
            set
            {
                if (m_sApplicant != value)
                {
                    m_sApplicant = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Applicant"));
                    OnChanged(this, null);
                }
            }
        }

        public string CadasterNumber
        {
            get
            {
                string ret = UseCadasterNumber ? m_sCadastreNumber : "відсутній";
                return ret;
            }
            set
            {
                if (m_sCadastreNumber != value)
                {
                    m_sCadastreNumber = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("CadasterNumber"));
                    OnChanged(this, null);
                }
            }
        }

        public string LandLocation
        {
            get
            {
                return m_sLandLocation ;
            }
            set
            {
                if (m_sLandLocation != value)
                {
                    m_sLandLocation = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("LandLocation"));
                    OnChanged(this, null);
                }
            }
        }

        public string LandCategory
        {
            get
            {
                return m_sLandCategory;
            }
            set
            {
                if (m_sLandCategory != value)
                {
                    m_sLandCategory = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("LandCategory"));
                    OnChanged(this, null);
                }
            }
        }

        public string LandPurpose
        {
            get
            {
                return m_sLandPurpose;
            }
            set
            {
                if (m_sLandPurpose != value)
                {
                    m_sLandPurpose = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("LandPurpose"));
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

        public double Kf
        {
            get
            {
                return m_dKfAdditional > 0 ? m_dKfAdditional : m_dKfReal;
            }
            set
            {
                if (m_dKf != value)
                {
                    m_dKf = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Kf"));
                    Recalculate();
                    OnChanged(this, null);
                }
            }
        }

        public double KfReal
        {
            get
            {
                return m_dKfReal;
            }
            set
            {
                if (m_dKfReal != value)
                {
                    m_dKfReal = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("KfReal"));
                    Recalculate();
                    OnChanged(this, null);
                }
            }
        }

        public double KfAdditional
        {
            get
            {
                return m_dKfAdditional;
            }
            set
            {
                if (m_dKfAdditional != value)
                {
                    m_dKfAdditional = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("KfAdditional"));
                    Recalculate();
                    OnChanged(this, null);
                }
            }
        }

        public double SquareAgriculture
        {
            get
            {
                double ret = LandType == 0 ? 0 : m_dSquareAgriculture;
                return ret;
            }
            set
            {
                if (m_dSquareAgriculture != value)
                {
                    m_dSquareAgriculture = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("SquareAgriculture"));
                    Recalculate();
                    OnChanged(this, null);
                }
            }
        }

        public double NormEvalAgriculture
        {
            get
            {
                double ret = LandType == 0 ? 0 : m_dNormEvalAgriculture;
                return ret;
            }
            set
            {
                if (m_dNormEvalAgriculture != value)
                {
                    m_dNormEvalAgriculture = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("NormEvalAgriculture"));
                    Recalculate();
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
                    Recalculate();
                    OnChanged(this, null);
                }
            }
        }

        public double NormEval
        {
            get
            {
                return m_dNormEval;
            }
            set
            {
                if (m_dNormEval != value)
                {
                    m_dNormEval = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("NormEval"));
                    OnChanged(this, null);
                }
            }
        }

        /// <summary>
        /// 0 - non-agriculture
        /// 1 - agriculture general
        /// 2 - agriculture arable
        /// </summary>
        public int LandType
        {
            get
            {
                return m_nType;
            }
            set
            {
                if (m_nType != value)
                {
                    m_nType = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("LandType"));
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

        #endregion Properties

        #region Methods

        public void Save(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Certification2017));
            TextWriter tw = new StreamWriter(filename);
            serializer.Serialize(tw, this);
            tw.Close(); 
        }

        public void Recalculate()
        {
            if (m_bLoaded)
            {
                double dKm3 = 1;
                if (m_aCoefVals != null)
                {
                    foreach (LocalCoefficientValue lcv in m_aCoefVals)
                    {
                        dKm3 *= lcv.Value;
                    }
                }
                Km3 = Math.Round(dKm3, 2);

                NormEval = LandType == 0 ? 
                    Math.Round(Square * Price * Km2 * Km3 * Kf * IndexCoefficient, 2):
                    Math.Round(SquareAgriculture * NormEvalAgriculture * Kf * IndexCoefficient, 2);
            }
        }

        public void SetZero()
        {
            Date = DateTime.Now;
            ShowDate = true;
            UseCadasterNumber = true;
            Applicant = string.Empty;
            CadasterNumber = string.Empty;
            LandLocation = string.Empty;
            LandCategory = string.Empty;
            LandPurpose = string.Empty;
            LandType = 0;
            Square = 0;
            Price = 0;
            Area = string.Empty;
            Km2 = 0;
            CoefficientValues = new List<LocalCoefficientValue>();
            Km3 = 0;
            Kf = 0;
            KfReal = 0;
            KfAdditional = 0;
            SquareAgriculture = 0;
            NormEvalAgriculture = 0;
            IndexCoefficient = 0;
            NormEval = 0;
            Executor = string.Empty;
        }

        public string NormEvalLetters
        {
            get
            {
                return MoneyByWords.UAHPhrase((decimal)NormEval);
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
