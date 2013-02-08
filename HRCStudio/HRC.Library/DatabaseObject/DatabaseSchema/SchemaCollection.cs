using System;
using System.Collections.Generic;
using HRC.Library.DatabaseObject.DatabaseSchema.EntitySchemaOperations;
using HRC.Library.DatabaseObject.DatabaseSchema.PocoSchemaOperations;
using HRC.Library.DatabaseObject.DatabaseSchema.SchemaBuilders.Interfaces;
using HRC.Library.DatabaseObject.DatabaseSchema.SchemaObjects;
using HRC.Library.DatabaseObject.EntityLibrary.EntityBase;

namespace HRC.Library.DatabaseObject.DatabaseSchema
{
    public class SchemaCollection : Dictionary<String, Schema>
    {
        private SchemaCollection()
        { }

        private static SchemaCollection _instance;
        static object o = new object();
        public static SchemaCollection Instance
        {
            get
            {

                if (_instance == null)
                {
                    lock (o)
                    {
                        if (_instance == null)
                        {
                            _instance = new SchemaCollection();
                        }
                    }
                }
                return _instance;
            }

        }
        public Schema GetSchema(object databaseObject)
        {
            string schemaName = GetSchemaName(databaseObject);
            if (!this.ContainsKey(schemaName))
            {
                IDatabaseObjectSchemaBuilder databaseObjectSchemaBuilder = null;

                if (databaseObject is BaseEntity)
                {
                    //entity
                    databaseObjectSchemaBuilder = new EntitySchemaBuilder();
                }
                else
                {
                    //poco
                    databaseObjectSchemaBuilder = new PocoSchemaBuilder();
                }
                Schema schema = databaseObjectSchemaBuilder.Build(databaseObject);
                this.Add(schemaName, schema);
            }
            return this[schemaName];
        }

        private string GetSchemaName(object databaseObject)
        {
            string schemaName;
            if (databaseObject is BaseEntity)
            {
                //entity
                schemaName = (databaseObject as BaseEntity).EntityName;
            }
            else
            {
                //poco
                schemaName = databaseObject.ToString();
            }
            return schemaName;
        }
    }
}
