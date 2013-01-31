using System.Xml;
using System;
using HRC.Foundation.XmlLibrary;
using HRC.Library.DBAccessLayer.Parameters;

namespace HRC.Library.EntityLibrary.EntitySchemaOperations
{
    public class EntityColumnSchema
    {
        public EntityColumnSchema(XmlNode node)
        {
            try
            {
                Name = XmlHelper.GetAttibuteValue<string>("Name", node);
                ColumnName = XmlHelper.GetAttibuteValue<string>("ColumnName", node);
                try
                {
                    IsIdentity = XmlHelper.GetAttibuteValue<bool>("IsIdentity", node);
                }
                catch 
                {
                    IsIdentity = false;
                }
                try
                {
                    IsPrimary = XmlHelper.GetAttibuteValue<bool>("IsPrimary", node);
                }
                catch 
                {
                    IsPrimary = false;
                }
                
                IsNull = XmlHelper.GetAttibuteValue<bool>("IsNull", node);
                Size = XmlHelper.GetAttibuteValue<int>("Size", node);
                ParameterType = XmlHelper.GetAttibuteValue<HRCParameterType>("Type", node);
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
