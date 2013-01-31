using System;
using System.Xml;
using HRC.Foundation.ConvertionLibrary;

namespace HRC.Foundation.XmlLibrary
{
    public class XmlHelper
    {
        public static T GetAttibuteValue<T>(string attrName, XmlNode node)
        {
            try
            {
                string val = node.Attributes[attrName].Value;
                return ConvertionHelper.ConvertValue<T>(val);
            }
            catch (Exception exp)
            {
                //TODO: baris Exception
                throw new Exception("attribute bulunamadı: " + attrName + "\n" + exp.Message);
            }
        }

        public static XmlDocument GetXmlDocument(string path)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                return doc;
            }
            catch (Exception exp)
            {
                //TODO: baris Exception
                throw new Exception("xml yuklenemedi: " + path);
            }
        }
    }
}
