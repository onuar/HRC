using System.Collections.Generic;
using System.Xml;

namespace HRC.Library.EntityLibrary.EntitySchemaOperations
{
    public class EntityColumnsSchema : Dictionary<string, EntityColumnSchema>
    {
        public EntityColumnsSchema(XmlNode xmlNode)
        {
            foreach (XmlNode node in xmlNode.SelectNodes("Column"))
            {
                EntityColumnSchema column = new EntityColumnSchema(node);
                this.Add(column.Name, column);
            }
        }
    }
}