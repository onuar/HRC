using System;
using HRC.Library.DatabaseObject.DatabaseSchema.SchemaBuilders.Interfaces;

namespace HRC.Library.DatabaseObject.DatabaseSchema.SchemaObjects
{
    public class Schema
    {
        public Schema(ISchemaBuilder schemaObjectBuilder)
        {
            TableName = schemaObjectBuilder.GetTableName();
            Name = schemaObjectBuilder.GetName();
            Columns = schemaObjectBuilder.GetColumnCollectionSchema();
        }
        public string TableName { get; set; }
        public string Name { get; set; }
        public ColumnCollectionSchema Columns { get; set; }

        public ColumnSchema GetIdentityColumn()
        {
            foreach (ColumnSchema item in Columns.Values)
            {
                if (item.IsIdentity)
                    return item;
            }
            //TODO: onur, exception
            throw new Exception("yok identity sana");
        }

        public ColumnSchema GetPrimaryColumn()
        {
            foreach (ColumnSchema item in Columns.Values)
            {
                if (item.IsPrimary)
                    return item;
            }
            return null;
            //TODO: onur, karar verin. primary yoksa, sistem nasıl davranır falan.
        }
    }
}