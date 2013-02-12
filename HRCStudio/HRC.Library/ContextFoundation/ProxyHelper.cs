using System;
using System.Collections.Generic;

namespace HRC.Library.ContextFoundation
{
    public class ProxyHelper<TInterface, TConcrete> : Dictionary<string, TInterface>
         where TConcrete : new()
    {
        private ProxyHelper()
        {

        }

        private static readonly Lazy<ProxyHelper<TInterface, TConcrete>> _instance =
            new Lazy<ProxyHelper<TInterface, TConcrete>>(() => new ProxyHelper<TInterface, TConcrete>(), true);
        public static ProxyHelper<TInterface, TConcrete> Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public TInterface AddOrGet()
        {
            string key = typeof(TInterface).ToString();
            if (!this.ContainsKey(key))
            {
                TInterface intf = ProxyGenerator<TInterface, TConcrete>.GetProxy();
                this.Add(key, intf);
            }
            return this[key];
        }
    }
}