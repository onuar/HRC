using System;
using System.Collections.Generic;
using System.Data;
using HRC.Library.DBAccessLayer.Parameters;
using HRC.Library.DatabaseObject.EntityLibrary.EntityBase;

namespace HRC.Library.DatabaseObject.DatabaseSchema.Operations
{
    public class PetaPocoOperation : IDbObjectOperationManager
    {
        public void Insert(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public int Update(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public int Delete(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public int Delete<T>(params HRCParameter[] parameters) where T : new()
        {
            throw new NotImplementedException();
        }

        public List<T> LoadAllCustomQuery<T>(string query, params HRCParameter[] parameters) where T : new()
        {
            throw new NotImplementedException();
        }

        public List<T> LoadAll<T>(string @where) where T : new()
        {
            throw new NotImplementedException();
        }

        public List<T> LoadAll<T>(params HRCParameter[] parameters) where T : new()
        {
            throw new NotImplementedException();
        }

        public List<T> LoadAll<T>() where T : new()
        {
            throw new NotImplementedException();
        }

        public T Load<T>(string @where) where T : new()
        {
            throw new NotImplementedException();
        }

        public DataTable ExecuteDataTable(string query, params HRCParameter[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
