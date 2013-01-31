using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OleDb;
//using System.Data.SqlServerCe;

namespace HRC.Library.DBAccessLayer.Factories
{
    class HRCDbFactory
    {
        internal static HRCDbProvider GetFactory(DbProviderFactory Factory)
        {
            if (Factory is SqlClientFactory)
                return new HRCSqlProvider();
            else if (Factory is OleDbFactory)
                return new HRCOleDb();
            //else if (Factory is SqlCeProviderFactory)
            //    return new HRCSqlCEProvider();
            //else if (Factory is OracleClientFactory)
            //    return new HRCOracleProvider();
            throw new Exception("Tanınmayan db tipi");
            //TODO: onur, exp
        }
    }
}