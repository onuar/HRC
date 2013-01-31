using System;
using System.Collections.Generic;

namespace HRC.Library.EntityLibrary.EntitySchemaOperations
{
    public class EntitySchemaCollection : Dictionary<String, EntitySchema>
    {
        private EntitySchemaCollection()
        {

        }

        private static EntitySchemaCollection _instance;
        static object o = new object();
        public static EntitySchemaCollection Instance
        {
            get
            {

                if (_instance == null)
                {
                    lock (o)
                    {
                        if (_instance == null)
                        {
                            _instance = new EntitySchemaCollection();
                        }
                    }
                }
                return _instance;
            }

        }
        public EntitySchema GetSchema(string schemaName)
        {
            if (!this.ContainsKey(schemaName))
            {
                EntitySchema schema = EntitySchemaBuilder.Build(schemaName);
                this.Add(schemaName, schema);
            }
            return this[schemaName];
        }

    }
}
