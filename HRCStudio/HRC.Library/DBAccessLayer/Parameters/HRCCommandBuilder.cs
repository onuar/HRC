using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using HRC.Library.DBAccessLayer.Factories;

namespace HRC.Library.DBAccessLayer.Parameters
{
    class HRCCommandBuilder
    {
        internal static void Build(DbCommand _command, string sql,
               List<HRCParameter> Parameters, DbProviderFactory Factory)
        {
            HRCDbProvider provider = HRCDbFactory.GetFactory(Factory);
            _command.CommandText = sql.Replace("~", provider.Prefix);
            foreach (HRCParameter p in Parameters)
            {
                DbParameter prm = provider.GetParameter(p);
                _command.Parameters.Add(prm);
            }
        }
    }
}
