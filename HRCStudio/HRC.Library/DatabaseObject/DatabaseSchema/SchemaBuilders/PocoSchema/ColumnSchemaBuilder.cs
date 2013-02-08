using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRC.Library.DBAccessLayer.Parameters;
using HRC.Library.DatabaseObject.DatabaseSchema.SchemaBuilders.Interfaces;

namespace HRC.Library.DatabaseObject.DatabaseSchema.SchemaBuilders.PocoSchema
{
    class ColumnSchemaBuilder:IColumnSchemaBuilder
    {
        public string GetName()
        {
            throw new NotImplementedException();
        }

        public string GetColumnName()
        {
            throw new NotImplementedException();
        }

        public bool IsIdentity()
        {
            throw new NotImplementedException();
        }

        public bool IsPrimary()
        {
            throw new NotImplementedException();
        }

        public bool IsNull()
        {
            throw new NotImplementedException();
        }

        public int GetSize()
        {
            throw new NotImplementedException();
        }

        public HRCParameterType GetParameterType()
        {
            throw new NotImplementedException();
        }
    }
}
