using System.Text;
using System.Collections.Generic;
using System;
using System.Data.Common;
using System.Data;
using HRC.Library.EntityLibrary.EntityBase;
using HRC.Library.EntityLibrary.EntitySchemaOperations;
using HRC.Library.DBAccessLayer;
using HRC.Library.DBAccessLayer.Parameters;

namespace HRC.Library.EntityLibrary.EntityOperations
{
    public class EntityManager
    {
        public static void Insert(BaseEntity entity)
        {
            string insertSQL = "insert into {0} ({1}) values({2});";

            StringBuilder sbColumnNames = new StringBuilder();
            StringBuilder sbParameters = new StringBuilder();

            EntityColumnSchema ic = null;

            using (DbManager db = new DbManager())
            {
                EntitySchema schema = EntitySchemaCollection.Instance.GetSchema(entity.EntityName);

                foreach (EntityColumnSchema column in schema.Columns.Values)
                {
                    if (column.IsIdentity)
                    {
                        insertSQL += "select scope_identity();";
                        ic = column;
                        continue;
                    }
                    sbColumnNames.Append(column.ColumnName);
                    sbColumnNames.Append(",");

                    sbParameters.Append("~");
                    sbParameters.Append(column.ColumnName);
                    sbParameters.Append(",");


                    HRCParameter p = new HRCParameter(
                        column.ColumnName
                        , entity.GetValue<object>(column.ColumnName)
                        , column.ParameterType);
                    //TODO: onur, test, entity.GetValue<object>(column.ColumnName) ile gelen null ise, DBNull.Value'i parameterValue olarak eklemek gerekebilir. Burası test edile.

                    db.Parameters.Add(p);
                }

                insertSQL = string.Format(insertSQL, schema.TableName, sbColumnNames.ToString().TrimEnd(','),
                    sbParameters.ToString().TrimEnd(','));

                int id = db.ExecuteScalar<int>(insertSQL);
                if (ic != null)
                    entity.SetValue<int>(ic.ColumnName, id);

            }
        }

        public static int Update(BaseEntity entity)
        {
            string updateSQL = "update {0} set {1} where {2};";

            StringBuilder sbWhere = new StringBuilder();
            StringBuilder sbParameters = new StringBuilder();

            HRCParameter p = null;

            using (DbManager db = new DbManager())
            {
                EntitySchema schema = EntitySchemaCollection.Instance.GetSchema(entity.EntityName);

                foreach (EntityColumnSchema column in schema.Columns.Values)
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

                    p = new HRCParameter(
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

                updateSQL = string.Format
                    (updateSQL
                    , schema.TableName
                    , sbParameters.ToString().TrimEnd(',')
                    , sbWhere.ToString());

                return db.ExecuteNonQuery(updateSQL);
            }
        }

        public static int Delete(BaseEntity entity)
        {
            string deleteSql = "Delete from {0} where {1};";

            StringBuilder sbWhere = new StringBuilder();

            using (DbManager db = new DbManager())
            {
                EntitySchema schema = EntitySchemaCollection.Instance.GetSchema(entity.EntityName);

                EntityColumnSchema pk = schema.GetPrimaryColumn();
                if (pk == null)
                    throw new Exception("Delete işlemi için primary column bulunamadı.");

                sbWhere.Append(pk.ColumnName);
                sbWhere.Append("=");
                sbWhere.Append("~");
                sbWhere.Append(pk.ColumnName);

                HRCParameter p = new HRCParameter(
                     pk.ColumnName
                     , entity.GetValue<object>(pk.ColumnName)
                     , pk.ParameterType);

                db.Parameters.Add(p);

                deleteSql = string.Format
                    (deleteSql
                    , schema.TableName
                    , sbWhere.ToString());

                return db.ExecuteNonQuery(deleteSql);
            }
        }

        public static int Delete<T>(params HRCParameter[] parameters) where T : new()
        {
            string deleteSql = "Delete from {0}";

            StringBuilder sbWhere = new StringBuilder();

            BaseEntity entity = Activator.CreateInstance<T>() as BaseEntity;

            int affectedRowCount = 0;

            using (DbManager db = new DbManager())
            {
                EntitySchema schema = EntitySchemaCollection.Instance.GetSchema(entity.EntityName);

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

                    int andLastIndex = sbWhere.ToString().LastIndexOf(" AND ");
                    deleteSql = deleteSql + sbWhere.ToString().Substring(0, andLastIndex);
                }

                affectedRowCount = db.ExecuteNonQuery(deleteSql);
            }

            return affectedRowCount;
        }

        public static List<T> LoadAllCustomQuery<T>(string query, params HRCParameter[] parameters) where T : new()
        {
            object MyEntityBase = Activator.CreateInstance(typeof(T));

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

                EntitySchema schema = EntitySchemaCollection.Instance.GetSchema(((BaseEntity)MyEntityBase).EntityName);
                StringBuilder SelectQuery = new StringBuilder();
                SelectQuery.Append(query);
                if (parameters.Length > 0)
                {
                    int andLastIndex = sbParameters.ToString().LastIndexOf(" AND ");
                    string whereOrAnd = (SelectQuery.ToString().ToLower().Contains("where") ? " And " : " Where ");
                    SelectQuery.AppendLine(string.Format(whereOrAnd + " {0}", sbParameters.ToString().Substring(0, andLastIndex)));
                }

                DbDataReader dr = dManager.ExecuteReader(SelectQuery.ToString());
                List<T> MyEntities = new List<T>();
                try
                {
                    BaseEntity eb = new BaseEntity(((BaseEntity)MyEntityBase).EntityName);
                    while (dr.Read())
                    {
                        MyEntityBase = new T();
                        foreach (EntityColumnSchema sc in schema.Columns.Values)
                        {
                            ((BaseEntity)MyEntityBase).SetValue(sc.Name, dr[sc.Name]);
                        }
                        object obj = Convert.ChangeType(MyEntityBase, typeof(T));
                        MyEntities.Add((T)obj);
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
                return MyEntities;
            }
        }

        public static List<T> LoadAll<T>(string where) where T : new()
        {
            object MyEntityBase = Activator.CreateInstance(typeof(T));
            string identityFieldName = string.Empty;
            using (DbManager dManager = new DbManager())
            {
                EntitySchema schema = EntitySchemaCollection.Instance.GetSchema(((BaseEntity)MyEntityBase).EntityName);
                StringBuilder SelectQuery = new StringBuilder();
                SelectQuery.AppendLine(string.Format("select * from {0} {1}", schema.TableName, where));
                DbDataReader dr = dManager.ExecuteReader(SelectQuery.ToString());
                List<T> MyEntities = new List<T>();
                try
                {
                    BaseEntity eb = new BaseEntity(((BaseEntity)MyEntityBase).EntityName);
                    while (dr.Read())
                    {
                        MyEntityBase = new T();
                        foreach (EntityColumnSchema sc in schema.Columns.Values)
                        {
                            ((BaseEntity)MyEntityBase).SetValue(sc.Name, dr[sc.Name]);
                        }
                        object obj = Convert.ChangeType(MyEntityBase, typeof(T));
                        MyEntities.Add((T)obj);
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
                return MyEntities;
            }
        }

        public static List<T> LoadAll<T>(params HRCParameter[] parameters) where T : new()
        {
            object MyEntityBase = Activator.CreateInstance(typeof(T));

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

                EntitySchema schema = EntitySchemaCollection.Instance.GetSchema(((BaseEntity)MyEntityBase).EntityName);
                StringBuilder SelectQuery = new StringBuilder();
                int andLastIndex = sbParameters.ToString().LastIndexOf(" AND ");
                SelectQuery.AppendLine(string.Format("select * from {0} Where {1}", schema.TableName, sbParameters.ToString().Substring(0, andLastIndex)));
                DbDataReader dr = dManager.ExecuteReader(SelectQuery.ToString());
                List<T> MyEntities = new List<T>();
                try
                {
                    BaseEntity eb = new BaseEntity(((BaseEntity)MyEntityBase).EntityName);
                    while (dr.Read())
                    {
                        MyEntityBase = new T();
                        foreach (EntityColumnSchema sc in schema.Columns.Values)
                        {
                            ((BaseEntity)MyEntityBase).SetValue(sc.Name, dr[sc.Name]);
                        }
                        object obj = Convert.ChangeType(MyEntityBase, typeof(T));
                        MyEntities.Add((T)obj);
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
                return MyEntities;
            }
        }

        public static List<T> LoadAll<T>() where T : new()
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

        public static T Load<T>(string where) where T : new()
        {
            object MyEntityBase = Activator.CreateInstance(typeof(T));
            string identityFieldName = string.Empty;
            using (DbManager dManager = new DbManager())
            {

                EntitySchema schema = EntitySchemaCollection.Instance.GetSchema(((BaseEntity)MyEntityBase).EntityName);
                StringBuilder SelectQuery = new StringBuilder();
                SelectQuery.AppendLine(string.Format("select * from {0} {1}", schema.TableName, where));
                DbDataReader dr = dManager.ExecuteReader(SelectQuery.ToString());
                try
                {

                    List<T> MyEntities = new List<T>();
                    BaseEntity eb = new BaseEntity(((BaseEntity)MyEntityBase).EntityName);
                    while (dr.Read())
                    {
                        MyEntityBase = new T();
                        foreach (EntityColumnSchema sc in schema.Columns.Values)
                        {

                            ((BaseEntity)MyEntityBase).SetValue(sc.Name, dr[sc.Name]);
                        }
                        object obj = Convert.ChangeType(MyEntityBase, typeof(T));
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

        public static DataTable ExecuteDataTable(string query, params HRCParameter[] parameters)
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