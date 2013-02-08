using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRC.Foundation.AttributeLibrary;
using HRC.Library.DatabaseObject.Attributes;

namespace HRC.Library.DatabaseObject.DatabaseSchema.SchemaBuilders.PocoSchema
{
    public class PocoAttributeHelper
    {
        static internal string GetTableName(Type objectType)
        {
            var attributes = AttributeHelper.GetCustomAttributes<TableNameAttribute>(objectType).ToList();
            if (attributes.Count == 0)
            {
                throw new ArgumentNullException("table name");
            }
            return attributes[0].TableName;
        }

        internal static string GetName(Type objectType)
        {
            var attributes = AttributeHelper.GetCustomAttributes<NameAttribute>(objectType).ToList();
            if (attributes.Count == 0)
            {
                return string.Empty;
            }
            return attributes[0].Name;
        }
    }
}
