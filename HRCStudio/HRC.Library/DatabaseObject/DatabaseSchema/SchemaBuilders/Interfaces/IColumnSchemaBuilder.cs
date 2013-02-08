using HRC.Library.DBAccessLayer.Parameters;

namespace HRC.Library.DatabaseObject.DatabaseSchema.SchemaBuilders.Interfaces
{
    public interface IColumnSchemaBuilder
    {
        string GetName();
        string GetColumnName();
        bool IsIdentity();
        bool IsPrimary();
        bool IsNull();
        int GetSize();
        HRCParameterType GetParameterType();
    }
}
