using System;
using System.Xml;
using HRC.Foundation.XmlLibrary;

namespace HRC.Library.EntityLibrary.EntitySchemaOperations
{
    public class EntitySchema
    {
        public EntitySchema(XmlNode xmlNode)
        {
            TableName = XmlHelper.GetAttibuteValue<string>("TableName", xmlNode);
            Name = XmlHelper.GetAttibuteValue<string>("Name", xmlNode);
            Columns = new EntityColumnsSchema(xmlNode.SelectSingleNode("EntityColumns"));
        }
        public string TableName { get; set; }
        public string Name { get; set; }
        public EntityColumnsSchema Columns { get; set; }

        public EntityColumnSchema GetIdentityColumn()
        {
            foreach (EntityColumnSchema item in Columns.Values)
            {
                if (item.IsIdentity)
                    return item;
            }
            //TODO: onur, exception
            throw new Exception("yok identity sana");
        }

        public EntityColumnSchema GetPrimaryColumn()
        {
            foreach (EntityColumnSchema item in Columns.Values)
            {
                if (item.IsPrimary)
                    return item;
            }
            return null;
            //TODO: onur, karar verin. primary yoksa, sistem nasıl davranır falan.
        }
    }
}
