using System;
using HRC.Library.DatabaseObject.DatabaseSchema.SchemaBuilders.Interfaces;
using HRC.Library.DatabaseObject.DatabaseSchema.SchemaBuilders.PocoSchema;
using HRC.Library.DatabaseObject.DatabaseSchema.SchemaObjects;

namespace HRC.Library.DatabaseObject.DatabaseSchema.PocoSchemaOperations
{
    public class PocoSchemaBuilder : IDatabaseObjectSchemaBuilder
    {
        public Schema Build(object schema)
        {
            return new Schema(new SchemaBuilder(schema.GetType()));
        }
    }
}
