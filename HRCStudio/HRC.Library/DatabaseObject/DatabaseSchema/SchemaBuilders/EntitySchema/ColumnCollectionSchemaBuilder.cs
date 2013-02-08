using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using HRC.Library.DatabaseObject.DatabaseSchema.SchemaBuilders.Interfaces;
using HRC.Library.DatabaseObject.DatabaseSchema.SchemaObjects;

namespace HRC.Library.DatabaseObject.DatabaseSchema.SchemaBuilders.EntitySchema
{
    public class ColumnCollectionSchemaBuilder : IColumnCollectionSchemaBuilder
    {
        private readonly XmlNode _columnSchema;
        public ColumnCollectionSchemaBuilder(XmlNode columnSchema)
        {
            _columnSchema = columnSchema;
        }

        public ColumnCollectionSchema GetColumnCollection()
        {
            var columnCollection = new ColumnCollectionSchema();
            foreach (XmlNode node in _columnSchema.SelectNodes("Column"))
            {
                var column = new ColumnSchema(new ColumnSchemaBuilder(node));
                columnCollection.Add(column.Name, column);
            }
            return columnCollection;
        }
    }
}
