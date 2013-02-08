using System;

namespace HRC.Library.DatabaseObject.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableNameAttribute : Attribute
    {
        public string TableName { get; set; }
        public TableNameAttribute(string tableName)
        {
            TableName = tableName;
        }
    }
}
