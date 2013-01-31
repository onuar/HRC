using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRC.Foundation.ConfigLibrary;

namespace HRC.Library.DBAccessLayer
{
    class HRCConnectionStringBuilder
    {
        public static string Build()
        {
            string constr = ConfigManager.GetConfigValue<String>("conStr");
            //constr = Crypto.GetEnCrypted(constr);
            //TODO: onur, encrypted connection string
            return constr;
        }
    }
}
