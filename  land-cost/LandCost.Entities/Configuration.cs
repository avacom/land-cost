using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LandCost.Entities
{
    /// <summary>
    /// Represents the global Configuration class
    /// </summary>
    public class Configuration
    {
        #region Members

        List<Profile> m_aProfiles;
        Profile m_oCurProfile;

        bool m_bChanged;

        #endregion Members

        #region Constructors

        /// <summary>
        /// Initializes the Configuration
        /// </summary>
        public Configuration()
        {
            m_aProfiles = new List<Profile>();
            m_oCurProfile = null;
            m_bChanged = false;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Get or set the profiles list
        /// </summary>
        public List<Profile> Profiles
        {
            get { return m_aProfiles; }
            set 
            { 
                m_aProfiles = value;
                m_bChanged = true;
            }
        }

        /// <summary>
        /// Get or set the current profile
        /// </summary>
        public Profile CurrentProfile
        {
            get
            {
                return m_oCurProfile;
            }
            set
            {
                m_oCurProfile = value;
                m_bChanged = true;
            }
        }

        /// <summary>
        /// Get or set the change-state
        /// </summary>
        public bool Changed
        {
            get
            {
                return m_bChanged;
            }
            set
            {
                m_bChanged = value;
            }
        }

        #endregion Properties

        #region Methods


        #endregion Methods
    }
}
