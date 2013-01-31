using System.Data.Common;
using System;
using HRC.Library.DBAccessLayer.Parameters;

namespace HRC.Library.DBAccessLayer.Factories
{
    public abstract class HRCDbProvider
    {
        public abstract string Prefix { get; }
        protected abstract DbParameter CreateParameter();
        protected abstract void SetInt(DbParameter p);
        protected abstract void SetString(DbParameter p);
        protected abstract void SetDate(DbParameter p);
        protected abstract void SetDouble(DbParameter p);

        protected abstract void SetBool(DbParameter p);
        protected abstract void SetDecimal(DbParameter p);
        protected abstract void SetFloat(DbParameter p);
        protected abstract void SetGuid(DbParameter p);

        internal DbParameter GetParameter(HRCParameter p)
        {
            DbParameter prm = CreateParameter();
            prm.Value = p.Value;
            prm.ParameterName = p.Name;
            prm.Size = p.Size;

            switch (p.Type)
            {
                case HRCParameterType.INT:
                    SetInt(prm);
                    break;
                case HRCParameterType.STRING:
                    SetString(prm);
                    break;
                case HRCParameterType.DATETIME:
                    SetDate(prm);
                    break;
                case HRCParameterType.DOUBLE:
                    SetDouble(prm);
                    break; 
                case HRCParameterType.BOOLEAN:
                    SetBool(prm);
                    break;
                case HRCParameterType.DECIMAL:
                    SetDecimal(prm);
                    break;
                case HRCParameterType.FLOAT:
                    SetFloat(prm);
                    break;
                case HRCParameterType.GUID:
                    SetGuid(prm);
                    break;
                default:
                    throw new Exception("HRC.DBAccessLayer.HRCDbProvider: Tanımlanmamış HRCParameterType");
            }

            return prm;
        }
    }
}
