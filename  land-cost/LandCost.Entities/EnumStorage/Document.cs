using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LandCost.Entities
{
    /// <summary>
    /// Represents the property document
    /// </summary>
    public class Document
    {
        #region Members

        string m_sName;
        string m_sMask;

        #endregion Members

        #region Constructors

        /// <summary>
        /// Creates the instance of a Document
        /// </summary>
        /// <param name="name">The name of a document</param>
        /// <param name="mask">The format mask</param>
        public Document(string name, string mask)
        {
            m_sName = name;
            m_sMask = mask;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Get or set the name of a document
        /// </summary>
        public string Name
        {
            get { return m_sName; }
            set { m_sName = value; }
        }

        /// <summary>
        /// Get or set the format mask for a document
        /// </summary>
        public string Mask
        {
            get { return m_sMask; }
            set { m_sMask = value; }
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
            return this.Name;
        }

        #endregion Methods
    }
}
