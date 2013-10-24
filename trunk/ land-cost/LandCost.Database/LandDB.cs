using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LandCost.Entities;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.TA;
using Db4objects.Db4o.Defragment;
using System.IO;

namespace LandCost.Database
{
    public class LandDB
    {
        Configuration m_oConfiguration;

        public LandDB()
        {
            m_oConfiguration = new Configuration();
        }

        public Configuration Config
        {
            get
            {
                return m_oConfiguration;
            }
        }

        public bool LoadConfiguration(string dbName)
        {
            bool bRet = true;

            try
            {
                IEmbeddedConfiguration config = Db4oEmbedded.NewConfiguration();
                config.Common.Add(new TransparentActivationSupport());

                using (IObjectContainer db = Db4oEmbedded.OpenFile(config, dbName))
                {
                    IList<Configuration> confs = db.Query<Configuration>(typeof(Configuration));

                    if (confs != null &&
                        confs.Count > 0)
                    {
                        m_oConfiguration = confs[0];
                        m_oConfiguration.Changed = false;
                        bRet = true;
                    }
                    else
                    {
                        bRet = false;
                    }
                }
            }
            catch (Exception ex)
            {
                bRet = false;
            }

            return bRet;
        }

        public bool SaveConfiguration(string dbName)
        {
            bool bRet = true;
            try
            {
                if (File.Exists(dbName))
                {
                    File.Delete(dbName);
                }
                using (IObjectContainer db = Db4oEmbedded.OpenFile(dbName))
                {
                    IObjectSet result = db.QueryByExample(typeof(Configuration));
                    Configuration found = result.Count > 0 ? (Configuration)result.Next() : null;
                    if (found != null)
                    {
                        db.Delete(found);
                    }

                    if (m_oConfiguration != null)
                    {
                        db.Store(m_oConfiguration);
                        m_oConfiguration.Changed = false;
                    }
                    else
                    {
                        bRet = false;
                    }
                }
            }
            catch (Exception ex)
            {
                bRet = false;
            }
            return bRet;
        }
    }
}
