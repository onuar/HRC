using System;
using System.Data.OleDb;
using System.Data.Common;

namespace HRC.Library.DBAccessLayer.Factories
{
    public class HRCOleDb : HRCDbProvider
    {
        public override string Prefix
        {
            get { return "@"; }
        }

        protected override DbParameter CreateParameter()
        {
            return new OleDbParameter();
        }

        protected override void SetInt(System.Data.Common.DbParameter p)
        {
            ((OleDbParameter)p).OleDbType = OleDbType.Integer;
        }

        protected override void SetString(System.Data.Common.DbParameter p)
        {
            ((OleDbParameter)p).OleDbType = OleDbType.VarChar;
        }

        protected override void SetDate(System.Data.Common.DbParameter p)
        {
            ((OleDbParameter)p).OleDbType = OleDbType.Date;
        }

        protected override void SetDouble(System.Data.Common.DbParameter p)
        {
            ((OleDbParameter)p).OleDbType = OleDbType.Double;
        }

        protected override void SetBool(System.Data.Common.DbParameter p)
        {
            ((OleDbParameter)p).OleDbType = OleDbType.Boolean;
        }

        protected override void SetDecimal(System.Data.Common.DbParameter p)
        {
            ((OleDbParameter)p).OleDbType = OleDbType.Decimal;
        }

        protected override void SetFloat(System.Data.Common.DbParameter p)
        {
            ((OleDbParameter)p).OleDbType = OleDbType.Double;
        }

        protected override void SetGuid(System.Data.Common.DbParameter p)
        {
            ((OleDbParameter)p).OleDbType = OleDbType.Guid;
        }
    }
}
