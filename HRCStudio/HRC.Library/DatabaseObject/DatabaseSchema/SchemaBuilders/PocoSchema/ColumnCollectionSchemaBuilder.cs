using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRC.Library.DatabaseObject.DatabaseSchema.SchemaBuilders.Interfaces;
using HRC.Library.DatabaseObject.DatabaseSchema.SchemaObjects;

namespace HRC.Library.DatabaseObject.DatabaseSchema.SchemaBuilders.PocoSchema
{
    class ColumnCollectionSchemaBuilder : IColumnCollectionSchemaBuilder
    {
        private readonly Type _objectType;

        public ColumnCollectionSchemaBuilder(Type objectType)
        {
            _objectType = objectType;
        }

        public ColumnCollectionSchema GetColumnCollection()
        {
            return null;
        }
    }
}
