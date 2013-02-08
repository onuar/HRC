using HRC.Library.DatabaseObject.DatabaseSchema.SchemaObjects;

namespace HRC.Library.DatabaseObject.DatabaseSchema.SchemaBuilders.Interfaces
{
    public interface ISchemaBuilder
    {
        string GetTableName();
        string GetName();
        ColumnCollectionSchema GetColumnCollectionSchema();
    }
}
