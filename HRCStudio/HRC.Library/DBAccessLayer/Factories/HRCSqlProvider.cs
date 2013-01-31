using System.Data.SqlClient;
using System.Data.Common;

namespace HRC.Library.DBAccessLayer.Factories
{
    class HRCSqlProvider : HRCDbProvider
    {
        public override string Prefix
        {
            get { return "@"; }
        }

        protected override DbParameter CreateParameter()
        {
            return new SqlParameter();
        }

        protected override void SetInt(System.Data.Common.DbParameter p)
        {
            ((SqlParameter)p).SqlDbType = System.Data.SqlDbType.Int;
        }

        protected override void SetString(System.Data.Common.DbParameter p)
        {
            ((SqlParameter)p).SqlDbType = System.Data.SqlDbType.VarChar;
        }

        protected override void SetDate(System.Data.Common.DbParameter p)
        {
            ((SqlParameter)p).SqlDbType = System.Data.SqlDbType.DateTime;
        }

        protected override void SetDouble(System.Data.Common.DbParameter p)
        {
            ((SqlParameter)p).SqlDbType = System.Data.SqlDbType.Decimal;
        }

        protected override void SetBool(DbParameter p)
        {
            ((SqlParameter)p).SqlDbType = System.Data.SqlDbType.Bit;
        }

        protected override void SetDecimal(DbParameter p)
        {
            ((SqlParameter)p).SqlDbType = System.Data.SqlDbType.Decimal;
        }

        protected override void SetFloat(DbParameter p)
        {
            ((SqlParameter)p).SqlDbType = System.Data.SqlDbType.Float;
        }

        protected override void SetGuid(DbParameter p)
        {
            ((SqlParameter)p).SqlDbType = System.Data.SqlDbType.UniqueIdentifier;
        }
    }
}
