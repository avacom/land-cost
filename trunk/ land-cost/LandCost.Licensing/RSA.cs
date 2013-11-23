/*******************************************************************************
 *******************************************************************************
            Author: Simon Bridge, May 2011 mailto:srbridge@gmail.com
 
 
        This code is provided under the Code Project Open Licence (CPOL)
          See http://www.codeproject.com/info/cpol10.aspx for details
  
 *******************************************************************************
 ******************************************************************************/

using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Cryptography;
using System.Management;
using System.Windows.Forms;

namespace LandCost.Licensing
{
    /// <summary>
    /// the license agreement for the software.
    /// embedded are the license terms (ie start and end dates) and a digital signature used to verify the 
    /// the license terms. this way, the consumer may be able to see what the license terms are, but if they attempt to change them
    /// (in order to extend thier license) then they will not be able to generate a matching signature.
    /// </summary>
    public class License
    {
        #region Properties

        /// <summary>
        /// the license terms. obscured.
        /// </summary>
        public string LicenseTerms { get; set; }

        /// <summary>
        /// the signature.
        /// </summary>
        public string Signature { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// saves the license to an xml file.
        /// </summary>
        /// <param name="fileName"></param>
        public void Save(String fileName)
        {
            Serializer.Save<License>(this, fileName);
        }

        /// <summary>
        /// saves the license to a stream as xml.
        /// </summary>
        /// <param name="stream"></param>
        public void Save(Stream stream)
        {
            Serializer.Save<License>(this, stream);
        }

        /// <summary>
        /// create a license object from a license file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static License Load(String fileName)
        {
            // read the filename:
            License ret = null;
            try
            {
                ret = Serializer.Load<License>(new FileInfo(fileName));
            }
            catch
            {
                ret = null;
            }
            return ret;
        }

        /// <summary>
        /// load a license from stream xml data.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static License Load(Stream data)
        {
            // read the data stream:
            return Serializer.Load<License>(data);
        }

        #endregion
    }

    /// <summary>
    /// handles license authorization.
    /// </summary>
    public class LicenseAuthorization
    {
        /// <summary>
        /// terms of the license agreement: it's not encrypted (but is obscured)
        /// </summary>
        [Serializable]
        internal class LicenseTerms
        {
            /// <summary>
            /// start date of the license agreement.
            /// </summary>
            public DateTime StartDate { get; set; }

            /// <summary>
            /// registered user name for the license agreement.
            /// </summary>
            public String UserName { get; set; }

            /// <summary>
            /// The hardware ID
            /// </summary>
            public String HardwareID { get; set; }
 
            /// <summary>
            /// the assembly name of the product that is licensed.
            /// </summary>
            public String ProductName { get; set; }

            /// <summary>
            /// the last date on which the software can be used on this license.
            /// </summary>
            public DateTime EndDate { get; set; }

            /// <summary>
            /// returns the license terms as an obscure (not human readable) string.
            /// </summary>
            /// <returns></returns>
            public String GetLicenseString()
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // create a binary formatter:
                    BinaryFormatter bnfmt = new BinaryFormatter();

                    // serialize the data to the memory-steam;
                    bnfmt.Serialize(ms, this);

                    // return a base64 string representation of the binary data:
                    return Convert.ToBase64String(ms.GetBuffer());

                }
            }

            /// <summary>
            /// returns a binary representation of the license terms.
            /// </summary>
            /// <returns></returns>
            public byte[] GetLicenseData()
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // create a binary formatter:
                    BinaryFormatter bnfmt = new BinaryFormatter();

                    // serialize the data to the memory-steam;
                    bnfmt.Serialize(ms, this);

                    // return a base64 string representation of the binary data:
                    return ms.GetBuffer();

                }
            }

            /// <summary>
            /// create a new license-terms object from a string-representation of the binary
            /// serialization of the licence-terms.
            /// </summary>
            /// <param name="licenseTerms"></param>
            /// <returns></returns>
            internal static LicenseTerms FromString(String licenseTerms)
            {

                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(licenseTerms)))
                {
                    // create a binary formatter:
                    BinaryFormatter bnfmt = new BinaryFormatter();

                    // serialize the data to the memory-steam;
                    object value = bnfmt.Deserialize(ms);

                    if (value is LicenseTerms)
                        return (LicenseTerms)value;
                    else
                        throw new ApplicationException("Invalid Type!");

                }
            }

        }

        /// <summary>
        /// builds a user-license pack. This includes the public-key that must be embedded in the application,
        /// and the private key (which must be kept secure) and a license-file for each user, specific to the 
        /// currently executing assembly, with the specified end date. Start date for the user-license file is
        /// current date.
        /// </summary>
        /// <param name="outputFolder"></param>
        /// <param name="userNames"></param>
        /// <param name="endDates"></param>
        public static void GenerateLicensePack(String outputFolder, String[] userNames, String[] procIDs, DateTime[] endDates)
        {
            // if the key files don't exist..create them:
            if (!File.Exists(outputFolder + "\\privateKey.xml"))
                GenerateLicenseResources(outputFolder);

            // generate each user-license for the current assembly:
            int i = 0;
            foreach (String userName in userNames)
            {
                // generate each license file:
                GenerateUserLicenseFile(outputFolder, userName, procIDs[i], endDates[i]);
                i++;
            }
        }

        /// <summary>
        /// generate the public and private key files in the specified folder.
        /// </summary>
        /// <param name="outputFolder"></param>
        public static void GenerateLicenseResources(String outputFolder)
        {
            // create the directory if it doesn't exist:
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // generate the required key files:
            String publicKeyFile = outputFolder + "\\publicKey.xml";
            String privateKeyFile = outputFolder + "\\privateKey.xml";

            // create a new private key:
            String privateKey = GeneratePrivateKey();
            
            // extract the public part of the key:
            String publicKey = GetPublicKey(privateKey);

            // save them:
            File.WriteAllText(publicKeyFile, publicKey);
            File.WriteAllText(privateKeyFile, privateKey);

        }

        /// <summary>
        /// generate a user-license file.
        /// </summary>
        /// <param name="licenseResourceFolder"></param>
        /// <param name="userName"></param>
        /// <param name="endDate"></param>
        public static void GenerateUserLicenseFile(String licenseResourceFolder, String userName, String procID, DateTime endDate)
        {
            
            // find and load the private key:
            String privateKeyFile = licenseResourceFolder + "\\privateKey.xml";

            // check the key file exists:
            if (File.Exists(privateKeyFile))
            {
                // load the private key:
                String privateKey = File.ReadAllText(privateKeyFile);

                // generate the license file;
                AssemblyName assName = Assembly.GetEntryAssembly().GetName();
                string sName = string.Format("{0} {1}", assName.Name, assName.Version.Major);
                License license = CreateLicense(
                    DateTime.Now, endDate,
                    sName,
                    userName,
                    procID,
                    privateKey
                    );

                // save the license file:
                license.Save(licenseResourceFolder + "\\" + userName + ".lic");

            }
            else
            {
                // can't find the key-file:
            }
        }

        /// <summary>
        /// runs a test of the licensing system from:
        /// C:\temp\user2.lic and C:\temp\user2_publicKey.xml
        /// </summary>
        public static void TestTestLicense()
        {
            License l = License.Load(@"c:\temp\user2.lic");
            String pkey = File.ReadAllText(@"c:\temp\user2_publicKey.xml");

            if (l != null)
            {
                try
                {
                    ValidateLicense(l, pkey);
                    Console.WriteLine("License is Valid");
                }
                catch (SecurityException se)
                {
                    Console.WriteLine("License INVALID: " + se.Message);
                }
            }
        }

        /// <summary>
        /// generate the required files for TestTestLicense()
        /// C:\temp\user2.lic and C:\temp\user2_publicKey.xml
        /// </summary>
        public static void SaveTestLicense()
        {
            String privateKey = GeneratePrivateKey();
            AssemblyName assName = Assembly.GetEntryAssembly().GetName();
            string sName = string.Format("{0} {1}", assName.Name, assName.Version.Major);
            License l = CreateLicense(DateTime.MinValue, DateTime.MaxValue, sName, "Simon", GetHardwareID(), privateKey );
            l.Save(@"C:\temp\user2.lic");
            File.WriteAllText(@"C:\temp\user2_publicKey.xml", GetPublicKey(privateKey));
            File.WriteAllText(@"C:\temp\system_privateKey.xml", privateKey);
        }

        /// <summary>
        /// generate a new, private key. this will be the master key for generating license files.
        /// </summary>
        /// <returns></returns>
        public static String GeneratePrivateKey()
        {
            DSACryptoServiceProvider dsa = new DSACryptoServiceProvider();
            return dsa.ToXmlString(true);
        }

        /// <summary>
        /// get the public key from a private key. this key must be distributed with the application.
        /// </summary>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static String GetPublicKey(String privateKey)
        {
            DSACryptoServiceProvider dsa = new DSACryptoServiceProvider();
            dsa.FromXmlString(privateKey);
            return dsa.ToXmlString(false);
        }

        public static string GetHardwareID()
        {

            string sHardID = "";
            string sQuery = "SELECT ProcessorId FROM Win32_Processor";

            ManagementObjectSearcher oManagementObjectSearcher = new ManagementObjectSearcher(sQuery);
            ManagementObjectCollection oCollection = oManagementObjectSearcher.Get();

            foreach (ManagementObject oManagementObject in oCollection)
            {
                sHardID += (string)oManagementObject["ProcessorId"];
            }

            sQuery = "SELECT SerialNumber FROM Win32_DiskDrive";
            oManagementObjectSearcher = new ManagementObjectSearcher(sQuery);
            oCollection = oManagementObjectSearcher.Get();

            foreach (ManagementObject oManagementObject in oCollection)
            {
                sHardID += (string)oManagementObject["SerialNumber"];
            }
            return (sHardID);
        }
        /// <summary>
        /// use a private key to generate a secure license file. the private key must match the public key accessible to
        /// the system validating the license.
        /// </summary>
        /// <param name="start">applicable start date for the license file.</param>
        /// <param name="end">applicable end date for the license file</param>
        /// <param name="productName">applicable product name</param>
        /// <param name="userName">user-name</param>
        /// <param name="privateKey">the private key (in XML form)</param>
        /// <returns>secure, public license, validated with the public part of the key</returns>
        public static License CreateLicense(DateTime start, DateTime end, String productName, String userName, String procID, String privateKey)
        {
            // create the licence terms:
            LicenseTerms terms = new LicenseTerms()
            {
                StartDate = start,
                EndDate = end,
                ProductName = productName,
                UserName = userName,
                HardwareID = procID
            };

            // create the crypto-service provider:
            DSACryptoServiceProvider dsa = new DSACryptoServiceProvider();

            // setup the dsa from the private key:
            dsa.FromXmlString(privateKey);

            // get the byte-array of the licence terms:
            byte[] license = terms.GetLicenseData();

            // get the signature:
            byte[] signature = dsa.SignData(license);

            // now create the license object:
            return new License()
            {
                LicenseTerms = Convert.ToBase64String(license),
                Signature = Convert.ToBase64String(signature)
            };
        }

        /// <summary>
        /// validates the license and if the app should run; if the license is valid the 
        /// method will complete, if not it will throw a security exception.
        /// </summary>
        /// <param name="license">
        /// the license object.
        /// </param>
        /// <exception cref="SecurityException">thrown if the license is invalid or expired</exception>
        /// <returns></returns>
        public static void ValidateLicense(License license, String publicKey)
        {
            // get the valid terms for the license: (this checks the digital signature on the license file)
            LicenseTerms terms = GetValidTerms(license, publicKey);

            // ensure a valid license-terms object was returned:
            if (terms != null)
            {
                // validate the date-range of the license terms:
                DateTime now = DateTime.Now;
                if (now.CompareTo(terms.EndDate) <= 0)
                {
                    if (now.CompareTo(terms.StartDate) >= 0)
                    {
                        // date range is valid... check the product name against the current assembly
                        AssemblyName assName = Assembly.GetEntryAssembly().GetName();
                        string sName = string.Format("{0} {1}", assName.Name, assName.Version.Major);
                        if (sName == terms.ProductName)
                        {
                            if (GetHardwareID() == terms.HardwareID)
                            {
                                int nLeft = terms.EndDate.Subtract(now).Days;
                                if (nLeft <= 10)
                                {
                                    MessageBox.Show(string.Format("Термін дії ліцензії закінчується через {0} день (-ів)", nLeft), "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }

                                return;
                            }
                            else
                            {
                                // hardware ID does not match
                                throw new SecurityException("Неправильний серійний номер!");
                            }

                        }
                        else
                        {
                            // product name doesn't match.
                            throw new SecurityException("Неправильна назва продукту: " + terms.ProductName);
                        }
                    }
                    else
                    {
                        // license terms not valid yet.
                        throw new SecurityException("Дана ліцензія набуде чинності " + terms.StartDate.ToShortDateString());
                    }
                }
                else
                {
                    // license terms have expired.
                    throw new SecurityException("Дана ліцензія втратила чинність " + terms.EndDate.ToShortDateString());
                }
            }
            else
            {
                // the license file was not valid.
                throw new SecurityException("Неправильний файл ліцензії!");
            }
        }

        /// <summary>
        /// validate license file and return the license terms.
        /// </summary>
        /// <param name="license"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        internal static LicenseTerms GetValidTerms(License license, String publicKey)
        {
            // create the crypto-service provider:
            DSACryptoServiceProvider dsa = new DSACryptoServiceProvider();

            // setup the provider from the public key:
            dsa.FromXmlString(publicKey);

            // get the license terms data:
            byte[] terms = Convert.FromBase64String(license.LicenseTerms);

            // get the signature data:
            byte[] signature = Convert.FromBase64String(license.Signature);

            // verify that the license-terms match the signature data
            if (dsa.VerifyData(terms, signature))
                return LicenseTerms.FromString(license.LicenseTerms);
            else
                throw new SecurityException("Signature Not Verified!");
        }
    }
}
