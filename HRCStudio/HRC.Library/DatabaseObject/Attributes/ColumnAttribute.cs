using System;
using HRC.Library.DBAccessLayer.Parameters;

namespace HRC.Library.DatabaseObject.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : Attribute
    {
        public string Name { get; set; }
        public string ColumnName { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsNull { get; set; }
        public int Size { get; set; }
        public HRCParameterType ParameterType { get; set; }
    }
}