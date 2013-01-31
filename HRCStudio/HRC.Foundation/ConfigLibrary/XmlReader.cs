using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using HRC.Foundation.XmlLibrary;

namespace HRC.Foundation.ConfigLibrary
{
    internal class XmlReader : ConfigReaderBase
    {
        public const string HRCConfigFileName = "HRCConfig.hrc";

        protected override object GetConfigValue(string key)
        {
            try
            {
                string val = string.Empty;

                XmlDocument doc = XmlHelper.GetXmlDocument(HRCConfigFileName);
                foreach (XmlNode node in doc.SelectSingleNode("./configuration").SelectSingleNode("./appSettings").SelectNodes("add"))
                {
                    if (XmlLibrary.XmlHelper.GetAttibuteValue<string>("key", node).Equals(key))
                    { val = XmlLibrary.XmlHelper.GetAttibuteValue<string>("value", node); break; }
                }

                if (string.IsNullOrEmpty(val))
                    throw new Exception("Config dosyasında bulunamayan key:" + key);
                return val;

            }
            catch (Exception exp)
            {
                //TODO: baris Exception
                throw new Exception("ConfigLibrary.XmlReader hatası: " + exp.Message);
            }
        }
    }
}
