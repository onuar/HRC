using System;
using System.Xml;
using System.IO;
using HRC.Foundation.ConfigLibrary;
using HRC.Foundation.XmlLibrary;
using HRC.Library.DatabaseObject.DatabaseSchema.SchemaBuilders.EntitySchema;
using HRC.Library.DatabaseObject.DatabaseSchema.SchemaBuilders.Interfaces;
using HRC.Library.DatabaseObject.DatabaseSchema.SchemaObjects;
using HRC.Library.DatabaseObject.EntityLibrary.EntityBase;

namespace HRC.Library.DatabaseObject.DatabaseSchema.EntitySchemaOperations
{
    public class EntitySchemaBuilder : IDatabaseObjectSchemaBuilder
    {
        public Schema Build(object schema)
        {
            var entity = schema as BaseEntity;
            if (entity == null)
            {
                throw new ArgumentNullException("schema");
            }
            return Build(entity.EntityName);
        }

        private Schema Build(string schemaName)
        {
            try
            {
                string schemapath = ConfigManager.GetConfigValue<string>("schemaPath");
                string fullPath = Path.Combine(schemapath, schemaName + ".xml");
                XmlDocument doc = XmlHelper.GetXmlDocument(fullPath);
                var xmlNode = doc.SelectSingleNode("./Entity");
                return new Schema(new SchemaBuilder(xmlNode));
            }
            catch (Exception exp)
            {
                //TODO: onur, exception
                throw new Exception("schema olusturulamadi: " + exp.Message);
            }
        }
    }
}