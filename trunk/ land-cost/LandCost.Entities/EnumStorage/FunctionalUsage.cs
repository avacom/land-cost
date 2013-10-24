using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LandCost.Entities
{
    /// <summary>
    /// Represents the Functional Usage entity
    /// </summary>
    public class FunctionalUsage
    {
        #region Members

        string m_sName;
        double m_dWeight;

        #endregion Members

        #region Constructors

        /// <summary>
        /// Create the instance of a FunctionalUsage
        /// </summary>
        /// <param name="name">The name of a functional usage</param>
        /// <param name="weight">the weight-coefficient</param>
        public FunctionalUsage(string name, double weight)
        {
            m_sName = name;
            m_dWeight = weight;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Get or set the name of a functional usage
        /// </summary>
        public string Name
        {
            get { return m_sName; }
            set { m_sName = value; }
        }

        /// <summary>
        /// Get or set the weight-coefficient of a functional usage
        /// </summary>
        public double Weight
        {
            get { return m_dWeight; }
            set { m_dWeight = value; }
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

        public override string ToString()
        {
            return m_sName;
        }

        #endregion Methods
    }
}
