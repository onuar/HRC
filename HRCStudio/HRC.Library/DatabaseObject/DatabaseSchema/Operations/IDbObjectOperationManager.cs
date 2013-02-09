using System.Collections.Generic;
using System.Data;
using HRC.Library.DBAccessLayer.Parameters;
using HRC.Library.DatabaseObject.EntityLibrary.EntityBase;

namespace HRC.Library.DatabaseObject.DatabaseSchema.Operations
{
    public interface IDbObjectOperationManager
    {
        void Insert(BaseEntity entity);
        int Update(BaseEntity entity);
        int Delete(BaseEntity entity);
        int Delete<T>(params HRCParameter[] parameters) where T : new();
        List<T> LoadAllCustomQuery<T>(string query, params HRCParameter[] parameters) where T : new();
        List<T> LoadAll<T>(string where) where T : new();
        List<T> LoadAll<T>(params HRCParameter[] parameters) where T : new();
        List<T> LoadAll<T>() where T : new();
        T Load<T>(string where) where T : new();
        DataTable ExecuteDataTable(string query, params HRCParameter[] parameters);
    }
}