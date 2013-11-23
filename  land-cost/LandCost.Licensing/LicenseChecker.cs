using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LandCost.Forms;
using System.Windows.Forms;
using System.Globalization;

namespace LandCost.Licensing
{
    public class LicenseChecker
    {
        DateTimeFormatInfo dateTimeFormat;
        LicenseForm licForm;
        string m_sFileName;
        string m_sID;

        string m_sUser;
        DateTime m_dtExpire;

        const string publicKey = "<DSAKeyValue><P>2NywmsNJLxQeNtr9xi0rSdYF1yk0BUHke+leFc8bE+wg5NdD+hS00sbiP1jhyztCdPpn/GT9SpmV5ws4xbsEaqL4UCDHtlryBDwg2hQMIBiwIKoieNn+tCF6u+dZNqgsZBnwbm9qdVRWfVNKV1jwoaEbW0wU8xwlVpBG0Y8aJq0=</P><Q>ud7Imq0D01vGlDo+GdxARCJGjHU=</Q><G>1jzKI5bqnFPc56wAyFTh4QeK3z6AAuT2hR+rmpoFDf0+kjmMJMLMk0Ot7DCeU/BQ8O9K6tynLbJqmLemoQD/9+A2+/GWoKLPZAOPSgzDVR2qPNGKr3+ruEQstnPe0cjq2tJ81Awhz8bHzyfGib/ceGAz+0ZWhPl+QGpVeBoCND4=</G><Y>zA6C7DsQ/IDtVnKy29+nUUGM6gJ9p27q4ftoX7+2SGop4XQTsmfuVHCgH9leAZPnImZJHmVA8LBvU9FTXn+0AoeQmjLLfIEY5ds5cb6ZKGheD9ZSkbcEQ/aXFK4h1iohIWZy6xKIQGucAC0Suw9HDNGk1KkLaz0j6yEpCahSXus=</Y><J>AAAAASqvaGSkxFIkrFHC/LhT3CTv4psuEoP4Mev9XxLRoQ2+s8/XHuOFkg8NqLmax+NN6lVg0YHxR9eXT6MKrz3+VhyPdAlaNRPb6aINQ+hATmDiUxuACBrZAdHLPMTVhpQg4SZUqyJu8r2CRCnmfA==</J><Seed>eteBx7LyC393Oi9Uw9+4YLdmKJk=</Seed><PgenCounter>AQc=</PgenCounter></DSAKeyValue>";

        public LicenseChecker(string filename)
        {
            m_sFileName = filename;
            dateTimeFormat = (new CultureInfo("uk-UA")).DateTimeFormat;
            m_sID = LicenseAuthorization.GetHardwareID();
        }

        public bool CheckLicense()
        {
            bool bRet = true;
            licForm = new LicenseForm();
            licForm.Serial = m_sID;

            if (!File.Exists(m_sFileName))
            {
                licForm.MainMessage = "Дана копія продукту LandCost ще не була зареєстрована.";
                licForm.ShowDialog();
                bRet = false;
            }

            if (bRet)
            {
                License l = License.Load(m_sFileName);
                if (l != null)
                {
                    try
                    {
                        LicenseAuthorization.ValidateLicense(l, publicKey);

                        LandCost.Licensing.LicenseAuthorization.LicenseTerms t = LicenseAuthorization.GetValidTerms(l, publicKey);
                        m_sUser = t.UserName;
                        m_dtExpire = t.EndDate;
                    }
                    catch (Exception ex)
                    {

                        licForm.MainMessage = ex.Message;
                        licForm.ShowDialog();
                        bRet = false;
                    }
                }
                else
                {
                    licForm.MainMessage = "Некоректний файл ліцензії!";
                    licForm.ShowDialog();
                    bRet = false;
                }
            }
            return bRet;
        }

        public string User
        {
            get
            {
                return m_sUser;
            }
        }

        public DateTime ExpireDate
        {
            get
            {
                return m_dtExpire;
            }
        }

        public string LicenseString
        {
            get
            {
                return string.Format("Дана копія програми зареєстрована на користувача {0}. Термін дії ліцензії закінчується {1}", User, ExpireDate.ToString("F", dateTimeFormat));
            }
        }
    }
}
