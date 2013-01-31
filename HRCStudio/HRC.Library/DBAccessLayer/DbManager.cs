using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using HRC.Library.DBAccessLayer.Parameters;
using HRC.Foundation.ConfigLibrary;
using HRC.Foundation.ConvertionLibrary;

namespace HRC.Library.DBAccessLayer
{
    public class DbManager : IDisposable
    {
        #region Connection and Execute
        DbProviderFactory _factory;
        DbConnection _connection;
        DbCommand _command;

        public bool KeepConnection { get; set; }

        void OpenConnection()
        {
            if (_connection == null)
            {
                _connection = Factory.CreateConnection();
                _connection.ConnectionString = HRCConnectionStringBuilder.Build();
                _connection.Open();

            }
        }

        List<HRCParameter> _parameters;
        public List<HRCParameter> Parameters
        {
            get
            {
                if (_parameters == null)
                    _parameters = new List<HRCParameter>();
                return _parameters;
            }
        }

        void ClearParameters()
        {
            Parameters.Clear();
        }

        void CloseConnection()
        {
            if (!KeepConnection)
                _connection.Close();
        }

        void PrepareCommand(string sql)
        {

            _command = Factory.CreateCommand();
            _command.Transaction = _transaction;
            HRCCommandBuilder.Build(_command, sql, Parameters, Factory);
            _command.Connection = _connection;

        }

        void PreConditions(string sql)
        {
            OpenConnection();
            PrepareCommand(sql);
        }

        void PostConditions()
        {
            ClearParameters();
            CloseConnection();
        }

        DbProviderFactory Factory
        {
            get
            {
                if (_factory == null)
                {
                    string invariantName = ConfigManager.GetConfigValue<string>("providerName");
                    _factory = DbProviderFactories.GetFactory(invariantName);
                }
                return _factory;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                _command.Dispose();
                _connection.Dispose();
            }
            //
            GC.SuppressFinalize(this);
        }

        public int ExecuteNonQuery(string sql)
        {
            try
            {
                PreConditions(sql);
                return _command.ExecuteNonQuery();
            }
            catch (Exception exp)
            {
                throw exp;
                //TODO: onur, exp
                //ExceptionManager.Handle(exp);
            }
            finally
            {
                PostConditions();
            }
        }

        public T ExecuteScalar<T>(string sql)
        {
            try
            {
                PreConditions(sql);
                object retVal = _command.ExecuteScalar();
                if (retVal != null)
                    return ConvertionHelper.ConvertValue<T>(retVal);
            }
            catch (Exception exp)
            {
                throw exp;
                //TODO: onur, exception
                //ExceptionManager.Handle(exp);
            }
            finally
            {
                PostConditions();
            }
            return default(T);
        }

        public DbDataReader ExecuteReader(string sql)
        {
            try
            {
                PreConditions(sql);
                return _command.ExecuteReader();
            }
            catch (Exception exp)
            {
                throw exp;
                //TODO: onur, exception
                //ExceptionManager.Handle(exp);
            }
        }

        public DataTable ExecuteDataTable(string sql)
        {
            try
            {
                PreConditions(sql);
                DbDataAdapter adapter = Factory.CreateDataAdapter();
                DataTable table = new DataTable();
                DataSet ds = new DataSet();
                adapter.SelectCommand = _command;
                adapter.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception exp)
            {
                throw exp;
                //TODO: onur, exception
                //ExceptionManager.Handle(exp);
            }
            finally
            {
                PostConditions();
            }

        }
        #endregion

        DbTransaction _transaction;
        bool _isCancelled;
        int _transDepth = 0;
        public HRCTransacitonScope GetTransaction()
        {
            return new HRCTransacitonScope(this);
        }

        internal void BeginTransaction()
        {
            if (_transDepth == 0)
            {
                _transaction = _connection.BeginTransaction();
            }
            ++_transDepth;
        }

        void CleanupTransaction()
        {
            if (_transDepth == 0)
            {
                if (_isCancelled)
                    _transaction.Rollback();
                else
                    _transaction.Commit();

                _transaction.Dispose();
            }

        }
        internal void CompleteTransaction()
        {

            --_transDepth;
            if (_transDepth == 0)
            {
                CleanupTransaction();
                _isCancelled = false;
            }
        }

        internal void AbortTransaction()
        {
            _isCancelled = true;
            CompleteTransaction();
        }
    }
}
