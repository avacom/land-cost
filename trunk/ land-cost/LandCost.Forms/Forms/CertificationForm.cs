using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LandCost.Entities;

namespace LandCost.Forms
{
    public partial class CertificationForm : Form
    {
        Certification cert;

        public CertificationForm()
        {
            InitializeComponent();
            dateBox.Value = DateTime.Now;
            cert = new Certification();

            SetBindings();
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
                docBox.Items.AddRange(profile.Documents.ToArray());
                chiefBox.Items.AddRange(profile.Chiefs.ToArray());
                executorBox.Items.AddRange(profile.Executors.ToArray());

                cert.IndexCoefficient = profile.IndexCoefficient;
            }
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
    }
}
