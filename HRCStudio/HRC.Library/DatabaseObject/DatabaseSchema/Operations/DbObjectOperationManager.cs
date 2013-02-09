using System.Text;
using System.Collections.Generic;
using System;
using System.Data.Common;
using System.Data;
using HRC.Library.DatabaseObject.DatabaseSchema.SchemaObjects;
using HRC.Library.DatabaseObject.EntityLibrary.EntityBase;
using HRC.Library.DBAccessLayer;
using HRC.Library.DBAccessLayer.Parameters;

namespace HRC.Library.DatabaseObject.DatabaseSchema.Operations
{
    public class DbObjectOperationManager : IDbObjectOperationManager
    {
        private static readonly Lazy<IDbObjectOperationManager> _instance = new Lazy<IDbObjectOperationManager>(() => new DbObjectOperationManager(), true);
        public static IDbObjectOperationManager Instance
        {
            get { return _instance.Value; }
        }

        public void Insert(BaseEntity entity)
        {
            string insertSql = "insert into {0} ({1}) values({2});";

            var sbColumnNames = new StringBuilder();
            var sbParameters = new StringBuilder();

            ColumnSchema ic = null;

            using (var db = new DbManager())
            {
                Schema schema = SchemaCollection.Instance.GetSchema(entity);

                foreach (ColumnSchema column in schema.Columns.Values)
                {
                    if (column.IsIdentity)
                    {
                        insertSql += "select scope_identity();";
                        ic = column;
                        continue;
                    }
                    sbColumnNames.Append(column.ColumnName);
                    sbColumnNames.Append(",");

                    sbParameters.Append("~");
                    sbParameters.Append(column.ColumnName);
                    sbParameters.Append(",");


                    var p = new HRCParameter(
                        column.ColumnName
                        , entity.GetValue<object>(column.ColumnName)
                        , column.ParameterType);
                    //TODO: onur, test, entity.GetValue<object>(column.ColumnName) ile gelen null ise, DBNull.Value'i parameterValue olarak eklemek gerekebilir. Burası test edile.

                    db.Parameters.Add(p);
                }

                insertSql = string.Format(insertSql, schema.TableName, sbColumnNames.ToString().TrimEnd(','),
                    sbParameters.ToString().TrimEnd(','));

                var id = db.ExecuteScalar<int>(insertSql);
                if (ic != null)
                    entity.SetValue<int>(ic.ColumnName, id);

            }
        }

        public int Update(BaseEntity entity)
        {
            string updateSql = "update {0} set {1} where {2};";

            var sbWhere = new StringBuilder();
            var sbParameters = new StringBuilder();

            using (var db = new DbManager())
            {
                Schema schema = SchemaCollection.Instance.GetSchema(entity.EntityName);

                foreach (ColumnSchema column in schema.Columns.Values)
                {
                    if (entity.GetValue<object>(column.Name) == null)//TODO: test edilecek. object olarak getiriliyor her veri
                        continue;
                    if (column.IsIdentity && !column.IsPrimary)
                        continue;
                    if (column.IsPrimary)
                    {//where kriteri için

                        sbWhere.Append(column.ColumnName);
                        sbWhere.Append("=");
                        sbWhere.Append("~");
                        sbWhere.Append(column.ColumnName);
                    }
                    else
                    {
                        sbParameters.Append(column.ColumnName);
                        sbParameters.Append("=");

                        sbParameters.Append("~");
                        sbParameters.Append(column.ColumnName);
                        sbParameters.Append(",");
                    }

                    var p = new HRCParameter(
                        column.ColumnName
                        , entity.GetValue<object>(column.ColumnName)
                        , column.ParameterType);
                    //TODO: onur, test, entity.GetValue<object>(column.ColumnName) ile gelen null ise, DBNull.Value'i parameterValue olarak eklemek gerekebilir. Burası test edile.

                    db.Parameters.Add(p);
                }
                if (string.IsNullOrEmpty(sbWhere.ToString()))
                {
                    throw new Exception("Update sorgusu için gerekli olan primary alanı bulunamadı.");
                }

                updateSql = string.Format
                    (updateSql
                    , schema.TableName
                    , sbParameters.ToString().TrimEnd(',')
                    , sbWhere);

                return db.ExecuteNonQuery(updateSql);
            }
        }

        public int Delete(BaseEntity entity)
        {
            string deleteSql = "Delete from {0} where {1};";

            var sbWhere = new StringBuilder();

            using (var db = new DbManager())
            {
                Schema schema = SchemaCollection.Instance.GetSchema(entity.EntityName);

                ColumnSchema pk = schema.GetPrimaryColumn();
                if (pk == null)
                    throw new Exception("Delete işlemi için primary column bulunamadı.");

                sbWhere.Append(pk.ColumnName);
                sbWhere.Append("=");
                sbWhere.Append("~");
                sbWhere.Append(pk.ColumnName);

                var parameter = new HRCParameter(
                     pk.ColumnName
                     , entity.GetValue<object>(pk.ColumnName)
                     , pk.ParameterType);

                db.Parameters.Add(parameter);

                deleteSql = string.Format
                    (deleteSql
                    , schema.TableName
                    , sbWhere);

                return db.ExecuteNonQuery(deleteSql);
            }
        }

        public int Delete<T>(params HRCParameter[] parameters) where T : new()
        {
            string deleteSql = "Delete from {0}";

            var sbWhere = new StringBuilder();

            var entity = Activator.CreateInstance<T>() as BaseEntity;

            int affectedRowCount = 0;

            using (var db = new DbManager())
            {
                Schema schema = SchemaCollection.Instance.GetSchema(entity.EntityName);

                deleteSql = string.Format
                        (deleteSql
                        , schema.TableName);

                if (parameters.Length > 0)
                {//exist parameters
                    sbWhere.Append(" where ");
                    foreach (HRCParameter p in parameters)
                    {
                        sbWhere.Append(p.Name);
                        sbWhere.Append(p.GetParameterOperator());
                        sbWhere.Append("~");
                        sbWhere.Append(p.Name);
                        sbWhere.Append(" AND ");

                        db.Parameters.Add(p);
                    }

                    int andLastIndex = sbWhere.ToString().LastIndexOf(" AND ", System.StringComparison.Ordinal);
                    deleteSql = deleteSql + sbWhere.ToString().Substring(0, andLastIndex);
                }

                affectedRowCount = db.ExecuteNonQuery(deleteSql);
            }

            return affectedRowCount;
        }

        public List<T> LoadAllCustomQuery<T>(string query, params HRCParameter[] parameters) where T : new()
        {
            object myEntityBase = Activator.CreateInstance(typeof(T));

            var sbParameters = new StringBuilder();

            using (var dManager = new DbManager())
            {
                foreach (HRCParameter prm in parameters)
                {
                    sbParameters.Append(prm.Name);
                    sbParameters.Append(prm.GetParameterOperator());
                    sbParameters.Append("~");
                    sbParameters.Append(prm.Name);
                    sbParameters.Append(" AND ");

                    dManager.Parameters.Add(prm);
                }

                Schema schema = SchemaCollection.Instance.GetSchema(((BaseEntity)myEntityBase).EntityName);
                var selectQuery = new StringBuilder();
                selectQuery.Append(query);
                if (parameters.Length > 0)
                {
                    int andLastIndex = sbParameters.ToString().LastIndexOf(" AND ", System.StringComparison.Ordinal);
                    string whereOrAnd = (selectQuery.ToString().ToLower().Contains("where") ? " And " : " Where ");
                    selectQuery.AppendLine(string.Format(whereOrAnd + " {0}", sbParameters.ToString().Substring(0, andLastIndex)));
                }

                DbDataReader dr = dManager.ExecuteReader(selectQuery.ToString());
                var myEntities = new List<T>();
                try
                {
                    var baseEntity = new BaseEntity(((BaseEntity)myEntityBase).EntityName);
                    while (dr.Read())
                    {
                        myEntityBase = new T();
                        foreach (ColumnSchema sc in schema.Columns.Values)
                        {
                            ((BaseEntity)myEntityBase).SetValue(sc.Name, dr[sc.Name]);
                        }
                        object obj = Convert.ChangeType(myEntityBase, typeof(T));
                        myEntities.Add((T)obj);
                    }
                }
                catch (Exception exp)
                {
                    //TODO:exp
                    throw exp;
                }
                finally
                {
                    dr.Dispose();
                }
                return myEntities;
            }
        }

        public List<T> LoadAll<T>(string where) where T : new()
        {
            object myEntityBase = Activator.CreateInstance(typeof(T));
            string identityFieldName = string.Empty;
            using (var dManager = new DbManager())
            {
                Schema schema = SchemaCollection.Instance.GetSchema(((BaseEntity)myEntityBase).EntityName);
                var selectQuery = new StringBuilder();
                selectQuery.AppendLine(string.Format("select * from {0} {1}", schema.TableName, where));
                DbDataReader dr = dManager.ExecuteReader(selectQuery.ToString());
                var myEntities = new List<T>();
                try
                {
                    var baseEntity = new BaseEntity(((BaseEntity)myEntityBase).EntityName);
                    while (dr.Read())
                    {
                        myEntityBase = new T();
                        foreach (ColumnSchema sc in schema.Columns.Values)
                        {
                            ((BaseEntity)myEntityBase).SetValue(sc.Name, dr[sc.Name]);
                        }
                        object obj = Convert.ChangeType(myEntityBase, typeof(T));
                        myEntities.Add((T)obj);
                    }
                }
                catch (Exception exp)
                {
                    //TODO:exp
                    throw exp;
                }
                finally
                {
                    dr.Dispose();
                }
                return myEntities;
            }
        }

        public List<T> LoadAll<T>(params HRCParameter[] parameters) where T : new()
        {
            object myEntityBase = Activator.CreateInstance(typeof(T));

            var sbParameters = new StringBuilder();

            using (var dManager = new DbManager())
            {
                foreach (HRCParameter prm in parameters)
                {
                    sbParameters.Append(prm.Name);
                    sbParameters.Append(prm.GetParameterOperator());
                    sbParameters.Append("~");
                    sbParameters.Append(prm.Name);
                    sbParameters.Append(" AND ");

                    dManager.Parameters.Add(prm);
                }

                Schema schema = SchemaCollection.Instance.GetSchema(((BaseEntity)myEntityBase).EntityName);
                var selectQuery = new StringBuilder();
                int andLastIndex = sbParameters.ToString().LastIndexOf(" AND ", System.StringComparison.Ordinal);
                selectQuery.AppendLine(string.Format("select * from {0} Where {1}", schema.TableName, sbParameters.ToString().Substring(0, andLastIndex)));
                DbDataReader dr = dManager.ExecuteReader(selectQuery.ToString());
                var myEntities = new List<T>();
                try
                {
                    var baseEntity = new BaseEntity(((BaseEntity)myEntityBase).EntityName);
                    while (dr.Read())
                    {
                        myEntityBase = new T();
                        foreach (ColumnSchema sc in schema.Columns.Values)
                        {
                            ((BaseEntity)myEntityBase).SetValue(sc.Name, dr[sc.Name]);
                        }
                        object obj = Convert.ChangeType(myEntityBase, typeof(T));
                        myEntities.Add((T)obj);
                    }
                }
                catch (Exception exp)
                {
                    //TODO:exp
                    throw exp;
                }
                finally
                {
                    dr.Dispose();
                }
                return myEntities;
            }
        }

        public List<T> LoadAll<T>() where T : new()
        {
            try
            {
                return LoadAll<T>(string.Empty);
            }
            catch (Exception exp)
            {
                //TODO: exp
                throw exp;
            }
        }

        public T Load<T>(string where) where T : new()
        {
            object myEntityBase = Activator.CreateInstance(typeof(T));
            string identityFieldName = string.Empty;
            using (var dManager = new DbManager())
            {

                Schema schema = SchemaCollection.Instance.GetSchema(((BaseEntity)myEntityBase).EntityName);
                var SelectQuery = new StringBuilder();
                SelectQuery.AppendLine(string.Format("select * from {0} {1}", schema.TableName, where));
                DbDataReader dr = dManager.ExecuteReader(SelectQuery.ToString());
                try
                {

                    var myEntities = new List<T>();
                    var baseEntity = new BaseEntity(((BaseEntity)myEntityBase).EntityName);
                    while (dr.Read())
                    {
                        myEntityBase = new T();
                        foreach (ColumnSchema sc in schema.Columns.Values)
                        {

                            ((BaseEntity)myEntityBase).SetValue(sc.Name, dr[sc.Name]);
                        }
                        object obj = Convert.ChangeType(myEntityBase, typeof(T));
                        return ((T)obj);
                    }
                    return new T();
                }
                catch (Exception exp)
                {
                    //TODO: exp
                    throw exp;
                }
                finally
                {
                    dr.Dispose();
                }
            }

        }

        public DataTable ExecuteDataTable(string query, params HRCParameter[] parameters)
        {
            DataTable dt = new DataTable();

            StringBuilder sbParameters = new StringBuilder();

            using (DbManager dManager = new DbManager())
            {
                foreach (HRCParameter prm in parameters)
                {
                    sbParameters.Append(prm.Name);
                    sbParameters.Append(prm.GetParameterOperator());
                    sbParameters.Append("~");
                    sbParameters.Append(prm.Name);
                    sbParameters.Append(" AND ");

                    dManager.Parameters.Add(prm);
                }

                StringBuilder SelectQuery = new StringBuilder();
                SelectQuery.Append(query);
                if (parameters.Length > 0)
                {
                    int andLastIndex = sbParameters.ToString().LastIndexOf(" AND ");
                    SelectQuery.AppendLine(string.Format(" Where {1}", sbParameters.ToString().Substring(0, andLastIndex)));
                }

                DbDataReader dr = dManager.ExecuteReader(SelectQuery.ToString());
                dt.Load(dr);

                dManager.Dispose();
                dr.Dispose();

                return dt;
            }
        }
    }
}