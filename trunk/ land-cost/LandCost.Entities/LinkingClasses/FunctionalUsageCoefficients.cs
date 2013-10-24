using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LandCost.Entities
{
    /// <summary>
    /// Links a FunctionalUsage to the list of the LocalCoefficientValue
    /// </summary>
    public class FunctionalUsageCoefficients: IEquatable<FunctionalUsageCoefficients>
    {
        #region Members

        FunctionalUsage m_oFuncUsage;
        List<LocalCoefficientValue> m_aCoefVals;

        #endregion Members

        #region Constructors

        /// <summary>
        /// Create the instance of a FunctionalUsageCoefficients
        /// </summary>
        /// <param name="usage">The functional usage</param>
        public FunctionalUsageCoefficients(FunctionalUsage usage)
        {
            m_oFuncUsage = usage;
            m_aCoefVals = new List<LocalCoefficientValue>();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Get or set the functional usage
        /// </summary>
        public FunctionalUsage Usage
        {
            get { return m_oFuncUsage; }
            set { m_oFuncUsage = value; }
        }

        /// <summary>
        /// Get or set the local coefficient values
        /// </summary>
        public List<LocalCoefficientValue> LocalCoefficientValues
        {
            get { return m_aCoefVals; }
            set { m_aCoefVals = value; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Add the local coeficient and its value
        /// </summary>
        /// <param name="coefVal">Coefficient + value</param>
        /// <returns></returns>
        public bool AddCoefficientValue(LocalCoefficientValue coefVal)
        {
            bool bRet = true;

            if (m_aCoefVals.Contains(coefVal))
            {
                bRet = false;
            }
            else
            {
                m_aCoefVals.Add(coefVal);
            }

            return bRet;
        }

        /// <summary>
        /// Remove a local coeficient and its value
        /// </summary>
        /// <param name="coefVal">Coefficient + value</param>
        /// <returns></returns>
        public bool RemoveCoefficientValue(LocalCoefficientValue coefVal)
        {
            bool bRet = true;

            if (m_aCoefVals.Contains(coefVal))
            {
                bRet = m_aCoefVals.Remove(coefVal);
            }
            else
            {
                bRet = false;
            }

            return bRet;
        }

        /// <summary>
        /// Removes a local coefficient and its value using the provided coefficient
        /// </summary>
        /// <param name="coef">Required coefficient</param>
        /// <returns></returns>
        public bool RemoveCoefficientValueByCoef(LocalCoefficient coef)
        {
            bool bRet = true;
            List<LocalCoefficientValue> lcvs = new List<LocalCoefficientValue>();
            lcvs.AddRange(LocalCoefficientValues);
            foreach (LocalCoefficientValue lcv in lcvs)
            {
                if (lcv.Coefficient == coef)
                {
                    bRet &= RemoveCoefficientValue(lcv);
                }
            }
            return bRet;
        }

        public bool Equals(FunctionalUsageCoefficients other)
        {
            bool bRet = this.Usage.Equals(other.Usage);
            if (bRet)
            {
                foreach (LocalCoefficientValue coefVal in this.LocalCoefficientValues)
                {
                    if (!other.LocalCoefficientValues.Contains(coefVal))
                    {
                        bRet = false;
                        break;
                    }
                }
            }
            return bRet;
        }

        public override string ToString()
        {
            return this.m_oFuncUsage.Name;
        }

        #endregion Methods
    }
}
