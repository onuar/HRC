using System;
using System.Xml;
using System.IO;
using HRC.Foundation.ConfigLibrary;
using HRC.Foundation.XmlLibrary;

namespace HRC.Library.EntityLibrary.EntitySchemaOperations
{
    class EntitySchemaBuilder
    {
        public static EntitySchema Build(string schemaName)
        {
            try
            {
                string schemapath = ConfigManager.GetConfigValue<string>("schemaPath");
                string fullPath = Path.Combine(schemapath, schemaName + ".xml");
                XmlDocument doc = XmlHelper.GetXmlDocument(fullPath);
                return new EntitySchema(doc.SelectSingleNode("./Entity"));
            }
            catch (Exception exp)
            {
                //TODO: onur, exception
                throw new Exception("schema olusturulamadi: " + exp.Message);
            }
        }
    }
}