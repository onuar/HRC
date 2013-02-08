using System.Xml;
using HRC.Foundation.XmlLibrary;
using HRC.Library.DBAccessLayer.Parameters;
using HRC.Library.DatabaseObject.DatabaseSchema.SchemaBuilders.Interfaces;

namespace HRC.Library.DatabaseObject.DatabaseSchema.SchemaBuilders.EntitySchema
{
    public class ColumnSchemaBuilder : IColumnSchemaBuilder
    {
        private readonly XmlNode _columSchema;

        public ColumnSchemaBuilder(XmlNode columSchema)
        {
            _columSchema = columSchema;
        }

        public string GetName()
        {
            return XmlHelper.GetAttibuteValue<string>("Name", _columSchema);
        }

        public string GetColumnName()
        {
            return XmlHelper.GetAttibuteValue<string>("ColumnName", _columSchema);
        }

        public bool IsIdentity()
        {
            return XmlHelper.GetAttibuteValue<bool>("IsIdentity", _columSchema);
        }

        public bool IsPrimary()
        {
            return XmlHelper.GetAttibuteValue<bool>("IsPrimary", _columSchema);
        }

        public bool IsNull()
        {
            return XmlHelper.GetAttibuteValue<bool>("IsNull", _columSchema);
        }

        public int GetSize()
        {
            return XmlHelper.GetAttibuteValue<int>("Size", _columSchema);
        }

        public HRCParameterType GetParameterType()
        {
            return XmlHelper.GetAttibuteValue<HRCParameterType>("Type", _columSchema);
        }
    }
}
