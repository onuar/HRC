using System.Xml;
using System;
using HRC.Foundation.XmlLibrary;
using HRC.Library.DBAccessLayer.Parameters;
using HRC.Library.DatabaseObject.DatabaseSchema.SchemaBuilders.Interfaces;

namespace HRC.Library.DatabaseObject.DatabaseSchema.SchemaObjects
{
    public class ColumnSchema
    {
        public ColumnSchema(IColumnSchemaBuilder columnSchemaBuilder)
        {
            try
            {
                Name = columnSchemaBuilder.GetName();
                ColumnName = columnSchemaBuilder.GetColumnName();
                try
                {
                    IsIdentity = columnSchemaBuilder.IsIdentity();
                }
                catch 
                {
                    IsIdentity = false;
                }
                try
                {
                    IsPrimary = columnSchemaBuilder.IsPrimary();
                }
                catch 
                {
                    IsPrimary = false;
                }

                IsNull = columnSchemaBuilder.IsNull();
                Size = columnSchemaBuilder.GetSize();
                ParameterType = columnSchemaBuilder.GetParameterType();
            }
            catch (Exception exp)
            {
                throw new Exception("Entity schema çekerken, bulunamayan column attribute'u var: " + exp.Message);
            }

        }

        public string Name { get; set; }
        public string ColumnName { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsNull { get; set; }
        public int Size { get; set; }
        public HRCParameterType ParameterType { get; set; }
    }
}
