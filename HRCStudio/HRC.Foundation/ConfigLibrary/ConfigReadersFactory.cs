using System;
using System.Xml;
using HRC.Foundation.XmlLibrary;

namespace HRC.Foundation.ConfigLibrary
{
    internal static class ConfigReadersFactory
    {

        static ConfigReaderBase _reader;
        internal static ConfigReaderBase GetConfigReader()
        {
            if (_reader == null)
            {
                try
                {
                    try
                    {
                        //eski
                        //ConfigurationManagerReader reader = new ConfigurationManagerReader();
                        //string readerType = reader.GetValue("configType").ToString();

                        //yeni
                        XmlDocument doc = XmlHelper.GetXmlDocument(XmlReader.HRCConfigFileName);
                        string readerType = XmlHelper.GetAttibuteValue<string>("configType", doc.SelectSingleNode("./configuration"));

                        Type t = Type.GetType(readerType);
                        _reader = (ConfigReaderBase)Activator.CreateInstance(t);
                    }
                    catch
                    {
                        _reader = new ConfigurationManagerReader();
                    }
                }
                catch (Exception exp)
                {
                    //TODO: baris Exception
                    throw exp;
                }
            }
            return _reader;

        }
    }
}
