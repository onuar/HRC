using System;
using System.Configuration;

namespace HRC.Foundation.ConfigLibrary
{
    internal class ConfigurationManagerReader : ConfigReaderBase
    {
        protected override object GetConfigValue(string key)
        {
            try
            {
                string val = ConfigurationManager.AppSettings[key];
                //TODO: baris Exception
                if (string.IsNullOrEmpty(val))
                    throw new Exception("Config dosyasında bulunamayan key: " + key);
                return val;

            }
            catch (Exception exp)
            {
                //TODO: baris Exception
                throw exp;
            }
        }
    }
}
