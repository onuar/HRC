using System.Xml;
using HRC.Foundation.XmlLibrary;
using HRC.Library.DatabaseObject.DatabaseSchema.SchemaBuilders.Interfaces;
using HRC.Library.DatabaseObject.DatabaseSchema.SchemaObjects;

namespace HRC.Library.DatabaseObject.DatabaseSchema.SchemaBuilders.EntitySchema
{
    public class SchemaBuilder : ISchemaBuilder
    {
        private readonly XmlNode _schemaNode;
        public SchemaBuilder(XmlNode schemaNode)
        {
            _schemaNode = schemaNode;
        }

        public string GetTableName()
        {
            return XmlHelper.GetAttibuteValue<string>("Name", _schemaNode);
        }

        public string GetName()
        {
            return XmlHelper.GetAttibuteValue<string>("Name", _schemaNode);
        }

        public ColumnCollectionSchema GetColumnCollectionSchema()
        {
            var columnSchema = _schemaNode.SelectSingleNode("EntityColumns");
            var columnCollectionSchemaBuilder = new ColumnCollectionSchemaBuilder(columnSchema);
            return columnCollectionSchemaBuilder.GetColumnCollection();
        }
    }
}
