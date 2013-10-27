using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace LandCost.Entities
{
    /// <summary>
    /// Represents the linking class for LocalCoefficient and its value
    /// </summary>
    public class LocalCoefficientValue: IEquatable<LocalCoefficientValue>, INotifyPropertyChanged
    {
        #region Members
        public event PropertyChangedEventHandler PropertyChanged;

        LocalCoefficient m_oCoef;
        double m_dValue;

        #endregion Members

        #region Constructors

        /// <summary>
        /// Creates the instance of a LocalCoefficientValue
        /// </summary>
        /// <param name="coef">The coefficient</param>
        /// <param name="value">The value</param>
        public LocalCoefficientValue(LocalCoefficient coef, double value)
        {
            m_oCoef = coef;
            m_dValue = value;
        }

        /// <summary>
        /// Creates the instance of a LocalCoefficientValue
        /// </summary>
        public LocalCoefficientValue()
        {
            m_oCoef = null;
            m_dValue = 0;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Get or set the local coefficient
        /// </summary>
        public LocalCoefficient Coefficient
        {
            get { return m_oCoef; }
            set { m_oCoef = value; }
        }

        /// <summary>
        /// Get or set the value for the local coefficient
        /// </summary>
        public double Value
        {
            get { return m_dValue; }
            set
            {
                m_dValue = value;
                NotifyPropertyChanged("Value");
            }
        }
        #endregion Properties

        #region Methods

        public LocalCoefficientValue Copy()
        {
            LocalCoefficientValue ret = new LocalCoefficientValue(this.Coefficient, this.Value);
            return ret;
        }

        public bool Equals(LocalCoefficientValue other)
        {
            return this.Coefficient.Equals(other.Coefficient) && this.Value == other.Value;
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Methods

    }
}
