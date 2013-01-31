using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRC.Library.ContextFoundation
{
    public class ProxyHelper<TOrginal, TInterface> : Dictionary<string, TInterface>
         where TOrginal : new()
    {
        private ProxyHelper()
        {

        }

        static Lazy<ProxyHelper<TOrginal, TInterface>> _instance =
            new Lazy<ProxyHelper<TOrginal, TInterface>>(() => { return new ProxyHelper<TOrginal, TInterface>(); }, true);
        public static ProxyHelper<TOrginal, TInterface> Instance
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
                TInterface intf = ProxyGenerator<TOrginal, TInterface>.GetProxy();
                this.Add(key, intf);
            }
            return this[key];
        }
    }
}