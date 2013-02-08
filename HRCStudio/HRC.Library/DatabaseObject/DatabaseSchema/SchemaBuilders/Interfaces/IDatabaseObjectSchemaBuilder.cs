using HRC.Library.DatabaseObject.DatabaseSchema.SchemaObjects;

namespace HRC.Library.DatabaseObject.DatabaseSchema.SchemaBuilders.Interfaces
{
    public interface IDatabaseObjectSchemaBuilder
    {
        Schema Build(object schema);
    }
}