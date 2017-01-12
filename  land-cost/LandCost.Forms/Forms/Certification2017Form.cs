using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LandCost.Entities;
using System.Xml.Serialization;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using System.Globalization;
using CrystalDecisions.Shared;

namespace LandCost.Forms
{
    public partial class Certification2017Form : Form
    {
        DateTimeFormatInfo dateTimeFormat;
        Profile m_Profile;
        Certification2017 cert;
        string m_sFileName;
        bool m_bChanged;

        public Certification2017Form()
        {
            InitializeComponent();
            dateTimeFormat = (new CultureInfo("uk-UA")).DateTimeFormat;
            dateBox.Value = DateTime.Now;
            m_sFileName = string.Empty;
            fileLabel.Text = "Новий витяг";
            cert = new Certification2017();
            cert.ShowDate = true;
            cert.UseCadasterNumber = true;
            SetBindings();
            cert.Changed += new EventHandler(cert_Changed);
            m_bChanged = true;
            saveMenu.Enabled = true;
            saveBtn.Enabled = true;
        }

        void cert_Changed(object sender, EventArgs e)
        {
            m_bChanged = true;
            saveMenu.Enabled = true;
            saveBtn.Enabled = true;
        }

        private void ClearBindings()
        {
            applicantBox.DataBindings.Clear();
            cadasterNumberCheckBox.DataBindings.Clear();
            landLocationBox.DataBindings.Clear();
            categoryBox.DataBindings.Clear();
            landPurposeBox.DataBindings.Clear();
            areaBox.DataBindings.Clear();
            landTypeBox.DataBindings.Clear();
            km2Box.DataBindings.Clear();
            squareBox.DataBindings.Clear();
            priceBox.DataBindings.Clear();
            squareAgricultureBox.DataBindings.Clear();
            normEvalAgricultureBox.DataBindings.Clear();
            km3Box.DataBindings.Clear();
            indexCoefBox.DataBindings.Clear();
            kfBox.DataBindings.Clear();
            evalTotalBox.DataBindings.Clear();
            executorBox.DataBindings.Clear();
            dateBox.DataBindings.Clear();
            showDateCheck.DataBindings.Clear();
        }

        private void SetBindings()
        {
            applicantBox.DataBindings.Add("Text", cert, "Applicant", false, DataSourceUpdateMode.OnPropertyChanged);
            cadasterNumberCheckBox.DataBindings.Add("Checked", cert, "UseCadasterNumber", false, DataSourceUpdateMode.OnPropertyChanged);
            landLocationBox.DataBindings.Add("Text", cert, "LandLocation", false, DataSourceUpdateMode.OnPropertyChanged);
            categoryBox.DataBindings.Add("Text", cert, "LandCategory", false, DataSourceUpdateMode.OnPropertyChanged);
            landPurposeBox.DataBindings.Add("Text", cert, "LandPurpose", false, DataSourceUpdateMode.OnPropertyChanged);
            areaBox.DataBindings.Add("Text", cert, "Area");
            landTypeBox.DataBindings.Add("SelectedIndex", cert, "LandType", false, DataSourceUpdateMode.OnPropertyChanged);
            km2Box.DataBindings.Add("Value", cert, "Km2");
            squareBox.DataBindings.Add("Value", cert, "Square", false, DataSourceUpdateMode.OnPropertyChanged);
            priceBox.DataBindings.Add("Value", cert, "Price");
            squareAgricultureBox.DataBindings.Add("Value", cert, "SquareAgriculture", false, DataSourceUpdateMode.OnPropertyChanged);
            normEvalAgricultureBox.DataBindings.Add("Value", cert, "NormEvalAgriculture", false, DataSourceUpdateMode.OnPropertyChanged);
            km3Box.DataBindings.Add("Value", cert, "Km3");
            indexCoefBox.DataBindings.Add("Value", cert, "IndexCoefficient");
            kfBox.DataBindings.Add("Value", cert, "Kf", false, DataSourceUpdateMode.OnPropertyChanged);
            evalTotalBox.DataBindings.Add("Value", cert, "NormEval");
            executorBox.DataBindings.Add("Text", cert, "Executor", false, DataSourceUpdateMode.OnPropertyChanged);
            dateBox.DataBindings.Add("Value", cert, "Date", false, DataSourceUpdateMode.OnPropertyChanged);
            showDateCheck.DataBindings.Add("Checked", cert, "ShowDate", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        public void SetProfile(Profile profile)
        {
            ClearBindings();
            SetBindings();
            categoryBox.Items.Clear();
            executorBox.Items.Clear();
            m_Profile = profile;
            if (profile != null)
            {
                categoryBox.Items.AddRange(profile.LandCategories.ToArray());
                executorBox.Items.AddRange(profile.Executors.ToArray());
            }
        }

        public void LoadFromFile(string filename)
        {
            cert.Changed -= new EventHandler(cert_Changed);
            ClearBindings();
            coefValSetCtl.CoefficientsChanged -= new EventHandler(coefValSetCtl_CoefficientsChanged);
            XmlSerializer serializer = new XmlSerializer(typeof(Certification2017));
            TextReader tr = new StreamReader(filename);
            cert = (Certification2017)serializer.Deserialize(tr);
            tr.Close();

            string[] cadasterParts = GetCadasterNumberParts(cert.CadasterNumber);
            cadBox1.Text = cadasterParts[0];
            cadBox2.Text = cadasterParts[1];
            cadBox3.Text = cadasterParts[2];

            SetBindings();
            coefValSetCtl.List = cert.CoefficientValues;
            coefValSetCtl.CoefficientsChanged += new EventHandler(coefValSetCtl_CoefficientsChanged);
            m_bChanged = false;
            saveMenu.Enabled = false;
            saveBtn.Enabled = false;
            m_sFileName = filename;
            fileLabel.Text = m_sFileName;
            cert.Changed += new EventHandler(cert_Changed);
        }

        public void SetValues(LandRegion selectedRegion, FunctionalUsageCoefficients selectedCoefs, string address)
        {
            cert.Changed -= new EventHandler(cert_Changed);
            ClearBindings();
            cert = new Certification2017();
            cert.ShowDate = true;
            SetBindings();
            cert.Changed += new EventHandler(cert_Changed);
            switch (landTypeBox.SelectedIndex)
            {
                case 0:
                    cert.IndexCoefficient = m_Profile.IndexCoefficient;
                    break;
                case 1:
                    cert.IndexCoefficient = m_Profile.IndexCoefficientAgriculture;
                    break;
                case 2:
                    cert.IndexCoefficient = m_Profile.IndexCoefficientArable;
                    break;
                default:
                    cert.IndexCoefficient = m_Profile.IndexCoefficient;
                    break;
            }
            m_sFileName = string.Empty;
            fileLabel.Text = "Новий витяг";
            List<LocalCoefficientValue> coefsList = new List<LocalCoefficientValue>();
            if (selectedCoefs != null)
            {
                foreach (LocalCoefficientValue val in selectedCoefs.LocalCoefficientValues)
                {
                    coefsList.Add(val.Copy());
                }
            }

            cert.CoefficientValues = coefsList;
            
            coefValSetCtl.CoefficientsChanged -= new EventHandler(coefValSetCtl_CoefficientsChanged);
            coefValSetCtl.List = cert.CoefficientValues;
            coefValSetCtl.CoefficientsChanged += new EventHandler(coefValSetCtl_CoefficientsChanged);

            cert.Date = DateTime.Now;
            cert.LandLocation = address;
            if (selectedRegion != null)
            {
                if (selectedRegion.ParentArea != null)
                {
                    cert.Area = selectedRegion.ParentArea.DisplayName;
                    cert.Km2 = selectedRegion.ParentArea.KM2;
                    priceBox.ReadOnly = true;
                }
                else
                {
                    cert.Area = "-";
                    priceBox.ReadOnly = false;
                }

                cert.Price = selectedRegion.Price;
            }

            if (selectedCoefs != null)
            {
                cert.Kf = selectedCoefs.Usage.Weight;
            }
        }

        void coefValSetCtl_CoefficientsChanged(object sender, EventArgs e)
        {
            cert.Recalculate();
        }


        bool SaveAs()
        {
            bool bRet = true;
            if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                m_sFileName = saveDialog.FileName;
                bRet =  Save();
            }
            return bRet;
        }

        bool Save()
        {
            bool bRet = true;
            if (!string.IsNullOrEmpty(m_sFileName))
            {
                try
                {
                    cert.Save(m_sFileName);
                    fileLabel.Text = m_sFileName;
                    m_bChanged = false;
                    saveMenu.Enabled = false;
                    saveBtn.Enabled = false;
                }
                catch
                {
                    bRet = false;
                    MessageBox.Show(this, "Не можу зберегти витяг", "Халепа!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            return bRet;
        }

        void Print()
        {
            try
            {
                ReportDocument doc = GetReport();
                if (printDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    processLbl.Text = "Друкую...";
                    System.Drawing.Printing.PageSettings pSets = new System.Drawing.Printing.PageSettings(printDialog.PrinterSettings);
                    PrintLayoutSettings pLs = new PrintLayoutSettings();
                    pLs.Scaling = PrintLayoutSettings.PrintScaling.Scale;
                    pLs.Centered = true;
                    doc.PrintToPrinter(printDialog.PrinterSettings, pSets, false, pLs);
                }
                doc.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, "Не вдалося надрукувати витяг! Помилка: " + ex.Message + " " + ex.StackTrace, "Халепа!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                processLbl.Text = string.Empty;
            }
        }

        void ExportPDF()
        {
            try
            {
                ReportDocument doc = GetReport();
                if (exportPDFDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    processLbl.Text = "Експортую...";
                    doc.ExportToDisk(ExportFormatType.PortableDocFormat, exportPDFDialog.FileName);
                }
                doc.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Не вдалося експортувати витяг! " + ex.Message + " " + ex.StackTrace, "Халепа!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                processLbl.Text = string.Empty;
            }
        }

        ReportDocument GetReport()
        {
            processLbl.Text = "Опрацьовую...";
            ReportDocument myDataReport = new ReportDocument();
            if (cert != null)
            {
               myDataReport.Load(Application.StartupPath + "\\Reports\\Certification2017.rpt");
            }
            processLbl.Text = string.Empty;
            return myDataReport;
        }

        void SaveCmd()
        {
            applicantBox.Focus();
            this.ValidateChildren();
            if (ValidateValues())
            {
                if (!string.IsNullOrEmpty(m_sFileName))
                {
                    Save();
                }
                else
                {
                    SaveAs();
                }
            }
        }

        void SaveAsCmd()
        {
            applicantBox.Focus();
            this.ValidateChildren();
            if (ValidateValues())
            {
                SaveAs();
            }
        }

        void PrintCmd()
        {
            applicantBox.Focus();
            this.ValidateChildren();
            if (ValidateValues())
            {
                Print();
            }
        }

        void PdfCmd()
        {
            applicantBox.Focus();
            this.ValidateChildren();
            if (ValidateValues())
            {
                ExportPDF();
            }
        }

        void RefreshCmd()
        {
            if (cert != null && m_Profile != null)
            {
                DialogResult res = MessageBox.Show(this, "Увага! Дана дія призведе до того, що"
                    + Environment.NewLine + Environment.NewLine
                    + "Коефіцієнт К(і)" 
                    + Environment.NewLine
                    + "Коефіцієнт Км2"
                    + Environment.NewLine
                    + "Середня вартісь земельної ділянки"
                    + Environment.NewLine + Environment.NewLine
                    + "будуть замінені на актуальні згідно з даними профілю. Ви впевнені, що хочете оновити ці значення?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    switch (landTypeBox.SelectedIndex)
                    {
                        case 0:
                            cert.IndexCoefficient = m_Profile.IndexCoefficient;
                            break;
                        case 1:
                            cert.IndexCoefficient = m_Profile.IndexCoefficientAgriculture;
                            break;
                        case 2:
                            cert.IndexCoefficient = m_Profile.IndexCoefficientArable;
                            break;
                        default:
                            cert.IndexCoefficient = m_Profile.IndexCoefficient;
                            break;
                    }

                    int i = -1;
                    if (int.TryParse(cert.Area, out i))
                    {
                        LandCost.Entities.Area a = m_Profile.GetAreaByNumber(i);
                        if (a != null)
                        {
                            cert.Km2 = a.KM2;
                            cert.Price = a.Price;
                        }
                    }
                }
            }
        }

        private void saveMenu_Click(object sender, EventArgs e)
        {
            SaveCmd();
        }

        private void saveAsMenu_Click(object sender, EventArgs e)
        {
            SaveAsCmd();
        }

        bool ValidateValues()
        {
            bool bRet = true;
            string sError = string.Empty;

            if (string.IsNullOrEmpty(applicantBox.Text.Trim()))
            {
                sError += "Вкажіть заявника" + Environment.NewLine;
            }
 
            // Address set
            if (string.IsNullOrEmpty(landLocationBox.Text.Trim()))
            {
                sError += "Вкажіть місце розташування земельної ділянки" + Environment.NewLine;
            }

            // Purpose set
            if (string.IsNullOrEmpty(landPurposeBox.Text.Trim()))
            {
                sError += "Вкажіть цільове призначення земельної ділянки" + Environment.NewLine;
            }
            
            // Square set
            if (landTypeBox.SelectedIndex == 0 && squareBox.Value <= 0)
            {
                sError += "Задайте площу земельної ділянки" + Environment.NewLine;
            }

            // Square Agriculture set
            if (landTypeBox.SelectedIndex != 0 && squareAgricultureBox.Value <= 0)
            {
                sError += "Задайте площу сільськогосподарських угідь" + Environment.NewLine;
            }

            // Norm Eval Agriculture set
            if (landTypeBox.SelectedIndex != 0 && normEvalAgricultureBox.Value <= 0)
            {
                sError += "Задайте нормативну грошову оцінку сільськогосподарських угідь" + Environment.NewLine;
            }

            // Cadaster number set
            if (cadasterNumberCheckBox.Checked &&
                (cadBox1.Text.Length != cadBox1.MaxLength ||
                cadBox2.Text.Length != cadBox2.MaxLength ||
                cadBox3.Text.Length != cadBox3.MaxLength))
            {
                sError += "Вкажіть коректний кадастровий номер" + Environment.NewLine;
            }

            // Executor set
            if (string.IsNullOrEmpty(executorBox.Text.Trim()))
            {
                sError += "Задайте виконавця" + Environment.NewLine;
            }

            if (!string.IsNullOrEmpty(sError))
            {
                bRet = false;
                MessageBox.Show(this, "Інформація введена некоректно! Будь ласка, скористайтеся порадами, аби виправити помилки: " + Environment.NewLine + Environment.NewLine + sError, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return bRet;
        }

        private void closeMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CertificationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            applicantBox.Focus();
            this.ValidateChildren();
            if (m_bChanged)
            {
                DialogResult res = MessageBox.Show(this, "Бажаєте зберегти зміни?", "Запитання", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    bool bOk = true;
                    bOk = ValidateValues();
                    if (bOk)
                    {
                        if (!string.IsNullOrEmpty(m_sFileName))
                        {
                            bOk = Save();
                        }
                        else
                        {
                            bOk = SaveAs();
                        }
                    }

                    if (!bOk)
                    {
                        e.Cancel = true;
                    }
                }
                else if (res == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void printMenu_Click(object sender, EventArgs e)
        {
            PrintCmd();
        }

        private void pdfMenu_Click(object sender, EventArgs e)
        {
            PdfCmd();
        }

        private void kfBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                applicantBox.Focus();
                kfBox.Focus();
            }
        }

        private void squareBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                applicantBox.Focus();
                squareBox.Focus();
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            SaveCmd();
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            PdfCmd();
        }

        private void printBtn_Click(object sender, EventArgs e)
        {
            PrintCmd();
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            RefreshCmd();
        }

        private void refreshMenu_Click(object sender, EventArgs e)
        {
            RefreshCmd();
        }

        private void landTypeBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (m_Profile != null)
            {
                landTypeBox.DataBindings[0].WriteValue();
                switch (landTypeBox.SelectedIndex)
                {
                    case 0:
                        cert.IndexCoefficient = m_Profile.IndexCoefficient;
                        break;
                    case 1:
                        cert.IndexCoefficient = m_Profile.IndexCoefficientAgriculture;
                        break;
                    case 2:
                        cert.IndexCoefficient = m_Profile.IndexCoefficientArable;
                        break;
                    default:
                        cert.IndexCoefficient = m_Profile.IndexCoefficient;
                        break;
                }
                EnableSquareAndPrices(cert.LandType);
            }
        }

        void EnableSquareAndPrices(int type)
        {
            label6.Enabled = type == 0;
            label10.Enabled = type == 0;
            squareBox.Enabled = type == 0;
            priceBox.Enabled = type == 0;

            label1.Enabled = type != 0;
            label3.Enabled = type != 0;
            squareAgricultureBox.Enabled = type != 0;
            normEvalAgricultureBox.Enabled = type != 0;
        }

        private void squareAgricultureBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                applicantBox.Focus();
                squareAgricultureBox.Focus();
            }
        }

        private void normEvalAgricultureBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                applicantBox.Focus();
                normEvalAgricultureBox.Focus();
            }
        }

        private void cadasterNumberCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            cadLabel1.Enabled = cadasterNumberCheckBox.Checked;
            cadLabel2.Enabled = cadasterNumberCheckBox.Checked;
            cadLabel3.Enabled = cadasterNumberCheckBox.Checked;

            cadBox1.Enabled = cadasterNumberCheckBox.Checked;
            cadBox2.Enabled = cadasterNumberCheckBox.Checked;
            cadBox3.Enabled = cadasterNumberCheckBox.Checked;
        }

        private string[] GetCadasterNumberParts(string cadasterNumber)
        {
            string[] ret = new string[3] { string.Empty, string.Empty, string.Empty };
            if (cadasterNumber != null)
            {
                string[] splitted = cadasterNumber.Split(':');
                if (splitted != null && splitted.Length > 1)
                {
                    for (int i = 0; i < Math.Min(splitted.Length, ret.Length); i++)
                    {
                        if (i + 1 < splitted.Length)
                        {
                            ret[i] = splitted[i + 1];
                        }
                    }
                }
            }
            return ret;
        }

        private string GetCadasterNumber()
        {
            return cadLabel1.Text + cadBox1.Text + cadLabel2.Text + cadBox2.Text + cadLabel3.Text + cadBox3.Text;
        }

        private void cadBox1_TextChanged(object sender, EventArgs e)
        {
            cert.CadasterNumber = GetCadasterNumber();
        }

        private void cadBox2_TextChanged(object sender, EventArgs e)
        {
            cert.CadasterNumber = GetCadasterNumber();
        }

        private void cadBox3_TextChanged(object sender, EventArgs e)
        {
            cert.CadasterNumber = GetCadasterNumber();
        }
    }
}
