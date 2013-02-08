using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRC.Foundation.AttributeLibrary;
using HRC.Library.DatabaseObject.Attributes;
using HRC.Library.DatabaseObject.DatabaseSchema.SchemaBuilders.Interfaces;
using HRC.Library.DatabaseObject.DatabaseSchema.SchemaObjects;

namespace HRC.Library.DatabaseObject.DatabaseSchema.SchemaBuilders.PocoSchema
{
    class SchemaBuilder : ISchemaBuilder
    {
        private readonly Type _objectType;

        public SchemaBuilder(Type objectType)
        {
            _objectType = objectType;
        }

        public string GetTableName()
        {
            return PocoAttributeHelper.GetTableName(_objectType);
        }

        public string GetName()
        {
            return PocoAttributeHelper.GetName(_objectType);
        }

        public ColumnCollectionSchema GetColumnCollectionSchema()
        {
            var columnCollectionSchemaBuilder = new ColumnCollectionSchemaBuilder(_objectType);
            return columnCollectionSchemaBuilder.GetColumnCollection();
        }
    }
}
