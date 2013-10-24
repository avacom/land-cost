using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace LandCost.Forms
{
    /// <summary> 
    /// Event arguments class for the PasteRejected event. 
    /// </summary> 
    public class PasteEventArgs : EventArgs
    {

        #region Private Variables

        private string m_OriginalText;
        private string m_ClipboardText;
        private string m_TextResult;
        private PasteRejectReasons m_RejectReason;

        #endregion

        #region Properties

        /// <summary> 
        /// Gets the original text. 
        /// </summary> 
        [ReadOnly(true)]
        public string OriginalText
        {
            get { return m_OriginalText; }
        }

        /// <summary> 
        /// Gets the text from the clipboard. 
        /// </summary> 
        [ReadOnly(true)]
        public string ClipboardText
        {
            get { return m_ClipboardText; }
        }

        /// <summary> 
        /// Gets the resulting text. 
        /// </summary> 
        [ReadOnly(true)]
        public string TextResult
        {
            get { return m_TextResult; }
        }

        /// <summary> 
        /// Gets the reason for the paste rejection. 
        /// </summary> 
        [ReadOnly(true)]
        public PasteRejectReasons RejectReason
        {
            get { return m_RejectReason; }
        }

        #endregion

        #region Constructor

        /// <summary> 
        /// Creates a new instance of the PasteRejectedEventArgs class. 
        /// </summary> 
        /// <param name="originalText">The original textl.</param> 
        /// <param name="clipboardText">The text from the clipboard.</param> 
        /// <param name="textResult">The resulting text.</param> 
        /// <param name="rejectReason">The reason for the paste rejection.</param> 
        public PasteEventArgs(string originalText, string clipboardText, string textResult,
            PasteRejectReasons rejectReason)
        {
            m_OriginalText = originalText;
            m_ClipboardText = clipboardText;
            m_TextResult = textResult;
            m_RejectReason = rejectReason;
        }

        #endregion

        #region Overridden Methods

        /// <summary> 
        /// Converts this PasteRejectedEventArgs instance into it's string representation. 
        /// </summary> 
        /// <returns>A string indicating the rejected reason.</returns> 
        public override string ToString()
        {
            return string.Format("Rejected reason: {0}", RejectReason.ToString());
        }

        #endregion

    }
}