using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace LandCost.Forms
{
    /// <summary> 
    /// Event arguments class for the KeyRejected event. 
    /// </summary> 
    public class KeyRejectedEventArgs : EventArgs
    {
        #region Private Variables

        private Keys m_Key;

        #endregion

        #region Properties

        /// <summary> 
        /// Gets the rejected key. 
        /// </summary> 
        [ReadOnly(true)]
        public Keys Key
        {
            get { return m_Key; }
        }

        #endregion

        #region Constructor

        /// <summary> 
        /// Creates a new instance of the KeyRejectedEventArgs class. 
        /// </summary> 
        /// <param name="key">The rejected key.</param> 
        public KeyRejectedEventArgs(Keys key)
        {
            m_Key = key;
        }

        #endregion

        #region Overridden Methods

        /// <summary> 
        /// Converts this KeyRejectedEventArgs instance into it's string representation. 
        /// </summary> 
        /// <returns>A string indicating the rejected key.</returns> 
        public override string ToString()
        {
            return string.Format("Rejected Key: {0}", Key.ToString());
        }

        #endregion
    }
}