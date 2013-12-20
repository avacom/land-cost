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
    public partial class CertificationForm : Form
    {
        DateTimeFormatInfo dateTimeFormat;
        Profile m_Profile;
        Certification cert;
        string m_sFileName;
        bool m_bChanged;

        public CertificationForm()
        {
            InitializeComponent();
            dateTimeFormat = (new CultureInfo("uk-UA")).DateTimeFormat;
            dateBox.Value = DateTime.Now;
            m_sFileName = string.Empty;
            fileLabel.Text = "Нова довідка";
            cert = new Certification();
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
            dateBox.DataBindings.Clear();
            ownerBox.DataBindings.Clear();
            ownerLocationBox.DataBindings.Clear();
            addressBox.DataBindings.Clear();
            landNameBox.DataBindings.Clear();
            squareBox.DataBindings.Clear();
            docBox.DataBindings.Clear();
            docAttribBox.DataBindings.Clear();
            areaBox.DataBindings.Clear();
            priceBox.DataBindings.Clear();
            km2Box.DataBindings.Clear();
            km3Box.DataBindings.Clear();
            oneMoreCheck.DataBindings.Clear();
            indexCoefBox.DataBindings.Clear();
            buildSquareBox.DataBindings.Clear();
            totalSquareBox.DataBindings.Clear();
            kfBox.DataBindings.Clear();
            kfBox2.DataBindings.Clear();
            evalM2Box.DataBindings.Clear();
            evalM2Box2.DataBindings.Clear();
            evalBox.DataBindings.Clear();
            evalBox2.DataBindings.Clear();
            evalTotalBox.DataBindings.Clear();
            evalTotalBox2.DataBindings.Clear();
            executorBox.DataBindings.Clear();
            chiefBox.DataBindings.Clear();
        }

        private void SetBindings()
        {
            dateBox.DataBindings.Add("Value", cert, "Date", false, DataSourceUpdateMode.OnPropertyChanged);
            ownerBox.DataBindings.Add("Text", cert, "Owner", false, DataSourceUpdateMode.OnPropertyChanged);
            ownerLocationBox.DataBindings.Add("Text", cert, "OwnerLocation", false, DataSourceUpdateMode.OnPropertyChanged);
            addressBox.DataBindings.Add("Text", cert, "Address", false, DataSourceUpdateMode.OnPropertyChanged);
            landNameBox.DataBindings.Add("Text", cert, "LandName", false, DataSourceUpdateMode.OnPropertyChanged);
            squareBox.DataBindings.Add("Value", cert, "Square", false, DataSourceUpdateMode.OnPropertyChanged);
            docBox.DataBindings.Add("Text", cert, "Document", false, DataSourceUpdateMode.OnPropertyChanged);
            docAttribBox.DataBindings.Add("Text", cert, "DocumentDetails", false, DataSourceUpdateMode.OnPropertyChanged);
            areaBox.DataBindings.Add("Text", cert, "Area");
            priceBox.DataBindings.Add("Value", cert, "Price");
            km2Box.DataBindings.Add("Value", cert, "Km2");
            km3Box.DataBindings.Add("Value", cert, "Km3");
            oneMoreCheck.DataBindings.Add("Checked", cert, "SideActive", false, DataSourceUpdateMode.OnPropertyChanged);
            indexCoefBox.DataBindings.Add("Value", cert, "IndexCoefficient");
            buildSquareBox.DataBindings.Add("Value", cert, "Square");
            totalSquareBox.DataBindings.Add("Value", cert, "Square");
            kfBox.DataBindings.Add("Value", cert, "KfMain", false, DataSourceUpdateMode.OnPropertyChanged);
            kfBox2.DataBindings.Add("Value", cert, "KfSide", false, DataSourceUpdateMode.OnPropertyChanged);
            evalM2Box.DataBindings.Add("Value", cert, "NormEvalM2Main");
            evalM2Box2.DataBindings.Add("Value", cert, "NormEvalM2Side");
            evalBox.DataBindings.Add("Value", cert, "TotalNormEvalMain");
            evalBox2.DataBindings.Add("Value", cert, "TotalNormEvalSide");
            evalTotalBox.DataBindings.Add("Value", cert, "TotalNormEvalMain");
            evalTotalBox2.DataBindings.Add("Value", cert, "TotalNormEvalSide");
            executorBox.DataBindings.Add("Text", cert, "Executor", false, DataSourceUpdateMode.OnPropertyChanged);
            chiefBox.DataBindings.Add("Text", cert, "Chief", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        public void SetProfile(Profile profile)
        {
            ClearBindings();
            SetBindings();
            docBox.Items.Clear();
            chiefBox.Items.Clear();
            executorBox.Items.Clear();
            m_Profile = profile;
            if (profile != null)
            {
                docBox.Items.Add("(відсутній)");
                docBox.Items.AddRange(profile.Documents.ToArray());
                docBox.SelectedIndex = 0;

                chiefBox.Items.AddRange(profile.Chiefs.ToArray());
                executorBox.Items.AddRange(profile.Executors.ToArray());
            }
        }

        public void LoadFromFile(string filename)
        {
            cert.Changed -= new EventHandler(cert_Changed);
            ClearBindings();
            coefValSetCtl.CoefficientsChanged -= new EventHandler(coefValSetCtl_CoefficientsChanged);
            XmlSerializer serializer = new XmlSerializer(typeof(Certification));
            TextReader tr = new StreamReader(filename);
            cert = (Certification)serializer.Deserialize(tr);
            tr.Close();

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
            cert = new Certification();
            SetBindings();
            cert.Changed += new EventHandler(cert_Changed);
            cert.IndexCoefficient = m_Profile.IndexCoefficient;
            m_sFileName = string.Empty;
            fileLabel.Text = "Нова довідка";
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
            cert.Address = address;
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
                cert.KfMain = selectedCoefs.Usage.Weight;
            }
            cert.KfSide = 0.5;
        }

        void coefValSetCtl_CoefficientsChanged(object sender, EventArgs e)
        {
            cert.Recalculate();
        }

        private void docBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (docBox.SelectedIndex > 0)
            {
                docAttribBox.BackColor = Color.FromArgb(255, 255, 192);
                docAttribBox.Enabled = true;
            }
            else
            {
                docAttribBox.Text = string.Empty;
                docAttribBox.BackColor = SystemColors.Control;
                docAttribBox.Enabled = false;
            }
        }

        private void oneMoreCheck_CheckedChanged(object sender, EventArgs e)
        {
            SetSideEnabled(oneMoreCheck.Checked);
        }

        void SetSideEnabled(bool enabled)
        {
            kfBox2.Enabled = enabled;
            evalM2Box2.Enabled = enabled;
            evalBox2.Enabled = enabled;
            evalTotalBox2.Enabled = enabled;
            
            kfBox.ReadOnly = enabled;
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
                    MessageBox.Show(this, "Не можу зберегти довідку", "Халепа!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                MessageBox.Show(this, "Не вдалося надрукувати довідку! Помилка: " + ex.Message + " " + ex.StackTrace, "Халепа!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                MessageBox.Show(this, "Не вдалося експортувати довідку! " + ex.Message + " " + ex.StackTrace, "Халепа!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                if (cert.SideActive)
                {
                    
                    myDataReport.Load(Application.StartupPath + "\\Reports\\CertificationExt.rpt");

                    myDataReport.SetParameterValue("txtKfSide", cert.KfSide);
                    myDataReport.SetParameterValue("txtKfNameSide", cert.KfNameSide);
                    myDataReport.SetParameterValue("txtEvalM2Side", cert.NormEvalM2Side);
                    myDataReport.SetParameterValue("txtEvalSide", cert.TotalNormEvalSide);
                    myDataReport.SetParameterValue("txtSideLetters", cert.TotalEvalSideLetters + " - Кф = " + cert.KfSide);
                    myDataReport.SetParameterValue("txtMainLetters", cert.TotalEvalMainLetters + " - Кф = " + cert.KfMain);
                    myDataReport.SetParameterValue("txtKfNameMain", cert.KfNameMain);
                }
                else
                {
                    myDataReport.Load(Application.StartupPath + "\\Reports\\Certification.rpt");
                    myDataReport.SetParameterValue("txtMainLetters", cert.TotalEvalMainLetters);
                }
                myDataReport.SetParameterValue("txtAgencyName", m_Profile.AgencyName.ToUpper());
                myDataReport.SetParameterValue("txtAgencyAddress", m_Profile.AgencyAddress);
                myDataReport.SetParameterValue("txtDate", cert.Date.ToString("D", dateTimeFormat));
                myDataReport.SetParameterValue("txtOwner", cert.Owner);
                myDataReport.SetParameterValue("txtOwnerLocation", cert.OwnerLocation);
                myDataReport.SetParameterValue("txtAddress", cert.Address);
                myDataReport.SetParameterValue("txtName", cert.LandName);
                myDataReport.SetParameterValue("txtSquare", cert.Square);

                string sDocument = cert.Document;
                if (docBox.SelectedIndex > 0)
                {
                    sDocument += " " + cert.DocumentDetails;
                }
                myDataReport.SetParameterValue("txtDocument", sDocument);
                myDataReport.SetParameterValue("txtArea", cert.Area);
                myDataReport.SetParameterValue("txtKm2", cert.Km2);
                myDataReport.SetParameterValue("txtPrice", cert.Price);
                int number = 0;
                for (int i = 0; i < cert.CoefficientValues.Count; i++)
                {
                    number++;
                    myDataReport.SetParameterValue(string.Format("txtC{0}Name", number), cert.CoefficientValues[i].Coefficient.Name);
                    myDataReport.SetParameterValue(string.Format("txtC{0}Value", number), cert.CoefficientValues[i].Value);
                }

                while (number < 12)
                {
                    number++;
                    myDataReport.SetParameterValue(string.Format("txtC{0}Name", number), "Не задано");
                    myDataReport.SetParameterValue(string.Format("txtC{0}Value", number), 1);
                }
                myDataReport.SetParameterValue("txtKm3", cert.Km3);

                myDataReport.SetParameterValue("txtKfMain", cert.KfMain);
                myDataReport.SetParameterValue("txtEvalM2Main", cert.NormEvalM2Main);
                myDataReport.SetParameterValue("txtEvalMain", cert.TotalNormEvalMain);

                myDataReport.SetParameterValue("txtIndexCoef", cert.IndexCoefficient);
                myDataReport.SetParameterValue("txtExecutor", cert.Executor);
                myDataReport.SetParameterValue("txtChief", cert.Chief);
            }
            processLbl.Text = string.Empty;
            return myDataReport;
        }

        void SaveCmd()
        {
            ownerBox.Focus();
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
            ownerBox.Focus();
            this.ValidateChildren();
            if (ValidateValues())
            {
                SaveAs();
            }
        }

        void PrintCmd()
        {
            ownerBox.Focus();
            this.ValidateChildren();
            if (ValidateValues())
            {
                Print();
            }
        }

        void PdfCmd()
        {
            ownerBox.Focus();
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
                    cert.IndexCoefficient = m_Profile.IndexCoefficient;

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

            if (string.IsNullOrEmpty(ownerBox.Text.Trim()))
            {
                sError += "Вкажіть власника земельної ділянки!" + Environment.NewLine;
            }
 
            // Owner address set
            if (string.IsNullOrEmpty(ownerLocationBox.Text.Trim()))
            {
                sError += "Вкажіть місце розташування власника!" + Environment.NewLine;
            }

            // Address set
            if (string.IsNullOrEmpty(addressBox.Text.Trim()))
            {
                sError += "Вкажіть адресу земельної ділянки!" + Environment.NewLine;
            }

            // Name set
            if (string.IsNullOrEmpty(landNameBox.Text.Trim()))
            {
                sError += "Вкажіть назву земельної ділянки!" + Environment.NewLine;
            }
            
            // Square set
            if (squareBox.Value <= 0)
            {
                sError += "Задайте площу земельної ділянки" + Environment.NewLine;
            }

            // Attributes of a document set
            if (docBox.SelectedIndex > 0 && string.IsNullOrEmpty(docAttribBox.Text.Trim()))
            {
                sError += "Задайте атрибути правовстановлюючого документа" + Environment.NewLine;
            }

            // Executor set
            if (string.IsNullOrEmpty(executorBox.Text.Trim()))
            {
                sError += "Задайте виконавця!" + Environment.NewLine;
            }

            // Chief set
            if (string.IsNullOrEmpty(chiefBox.Text.Trim()))
            {
                sError += "Задайте керівника!" + Environment.NewLine;
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
            ownerBox.Focus();
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
                ownerBox.Focus();
                kfBox.Focus();
            }
        }

        private void squareBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ownerBox.Focus();
                squareBox.Focus();
            }
        }

        private void kfBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ownerBox.Focus();
                kfBox2.Focus();
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
    }
}
