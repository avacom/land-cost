using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LandCost.Entities
{
    /// <summary>
    /// Represents the coefficient that increases or decreases the land price
    /// </summary>
    public class LocalCoefficient
    {
        #region Members

        string m_sName;

        #endregion Members

        #region Constructors

        /// <summary>
        /// Create the instance of a LoalCoefficient
        /// </summary>
        /// <param name="name">The name of a local coefficient</param>
        public LocalCoefficient(string name)
        {
            m_sName = name;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Get or set the name of a local coefficient
        /// </summary>
        public string Name
        {
            get { return m_sName; }
            set { m_sName = value; }
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
