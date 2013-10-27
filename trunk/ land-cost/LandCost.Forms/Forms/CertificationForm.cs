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

namespace LandCost.Forms
{
    public partial class CertificationForm : Form
    {
        Certification cert;
        string m_sFileName;
        bool m_bChanged;

        public CertificationForm()
        {
            InitializeComponent();
            dateBox.Value = DateTime.Now;
            m_sFileName = string.Empty;
            fileLabel.Text = "Нова довідка";
            cert = new Certification();
            SetBindings();
            cert.Changed += new EventHandler(cert_Changed);
            m_bChanged = true;
            saveMenu.Enabled = true;
        }

        void cert_Changed(object sender, EventArgs e)
        {
            m_bChanged = true;
            saveMenu.Enabled = true;
        }

        private void ClearBindings()
        {
            numberBox.DataBindings.Clear();
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
            kfNameBox.DataBindings.Clear();
            kfBox2.DataBindings.Clear();
            kfNameBox2.DataBindings.Clear();
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
            numberBox.DataBindings.Add("Text", cert, "Number");
            dateBox.DataBindings.Add("Value", cert, "Date");
            ownerBox.DataBindings.Add("Text", cert, "Owner");
            ownerLocationBox.DataBindings.Add("Text", cert, "OwnerLocation");
            addressBox.DataBindings.Add("Text", cert, "Address");
            landNameBox.DataBindings.Add("Text", cert, "LandName");
            squareBox.DataBindings.Add("Text", cert, "Square");
            docBox.DataBindings.Add("Text", cert, "Document");
            docAttribBox.DataBindings.Add("Text", cert, "DocumentDetails");
            areaBox.DataBindings.Add("Text", cert, "Area");
            priceBox.DataBindings.Add("Text", cert, "Price");
            km2Box.DataBindings.Add("Text", cert, "Km2");
            km3Box.DataBindings.Add("Text", cert, "Km3");
            oneMoreCheck.DataBindings.Add("Checked", cert, "SideActive");
            indexCoefBox.DataBindings.Add("Text", cert, "IndexCoefficient");
            buildSquareBox.DataBindings.Add("Text", cert, "Square");
            totalSquareBox.DataBindings.Add("Text", cert, "Square");
            kfBox.DataBindings.Add("Text", cert, "KfMain");
            kfNameBox.DataBindings.Add("Text", cert, "KfNameMain");
            kfBox2.DataBindings.Add("Text", cert, "KfSide");
            kfNameBox2.DataBindings.Add("Text", cert, "KfNameSide");
            evalM2Box.DataBindings.Add("Text", cert, "NormEvalM2Main");
            evalM2Box2.DataBindings.Add("Text", cert, "NormEvalM2Side");
            evalBox.DataBindings.Add("Text", cert, "TotalNormEvalMain");
            evalBox2.DataBindings.Add("Text", cert, "TotalNormEvalSide");
            evalTotalBox.DataBindings.Add("Text", cert, "TotalNormEvalMain");
            evalTotalBox2.DataBindings.Add("Text", cert, "TotalNormEvalSide");
            executorBox.DataBindings.Add("Text", cert, "Executor");
            chiefBox.DataBindings.Add("Text", cert, "Chief");
        }

        public void SetProfile(Profile profile)
        {
            ClearBindings();
            SetBindings();
            docBox.Items.Clear();
            chiefBox.Items.Clear();
            executorBox.Items.Clear();
            if (profile != null)
            {
                docBox.Items.Add("Відсутній");
                docBox.Items.AddRange(profile.Documents.ToArray());
                docBox.SelectedIndex = 0;

                chiefBox.Items.AddRange(profile.Chiefs.ToArray());
                executorBox.Items.AddRange(profile.Executors.ToArray());

                cert.IndexCoefficient = profile.IndexCoefficient;
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
            m_sFileName = filename;
            fileLabel.Text = m_sFileName;
            cert.Changed += new EventHandler(cert_Changed);
        }

        public void SetValues(LandRegion selectedRegion, FunctionalUsageCoefficients selectedCoefs, string address)
        {
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
            kfNameBox2.Enabled = enabled;
            if (enabled)
            {
                kfNameBox2.BackColor = Color.FromArgb(255, 255, 192);
            }
            else
            {
                kfNameBox2.BackColor = SystemColors.Control;
            }
            evalM2Box2.Enabled = enabled;
            evalBox2.Enabled = enabled;
            evalTotalBox2.Enabled = enabled;
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
                }
                catch
                {
                    bRet = false;
                    MessageBox.Show(this, "Не можу зберегти довідку", "Халепа!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            return bRet;
        }

        private void saveMenu_Click(object sender, EventArgs e)
        {
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

        private void saveAsMenu_Click(object sender, EventArgs e)
        {
            if (ValidateValues())
            {
                SaveAs();
            }
        }

        bool ValidateValues()
        {
            bool bRet = true;
            string sError = string.Empty;
            // Number set
            if (string.IsNullOrEmpty(numberBox.Text.Trim()))
            {
                sError += "Вкажіть номер довідки!" + Environment.NewLine;
            }

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

            // Kf name set
            if (string.IsNullOrEmpty(kfNameBox.Text.Trim()))
            {
                sError += "Деталізуйте функціональне призначення!" + Environment.NewLine;
            }

            // Kf name set
            if (oneMoreCheck.Checked && string.IsNullOrEmpty(kfNameBox2.Text.Trim()))
            {
                sError += "Деталізуйте додаткове функціональне призначення!" + Environment.NewLine;
            }

            // Executor set
            if (string.IsNullOrEmpty(executorBox.Text.Trim()))
            {
                sError += "Задайте виконавця!" + Environment.NewLine;
            }

            // Chief set
            if (string.IsNullOrEmpty(chiefBox.Text.Trim()))
            {
                sError += "Задайте начальника!" + Environment.NewLine;
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
    }
}
