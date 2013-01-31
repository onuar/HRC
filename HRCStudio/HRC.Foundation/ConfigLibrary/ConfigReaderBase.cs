using System.Collections.Generic;

namespace HRC.Foundation.ConfigLibrary
{
    internal abstract class ConfigReaderBase
    {
        protected abstract object GetConfigValue(string key);
        Dictionary<string, object> _cache = new Dictionary<string, object>();
        internal virtual object GetValue(string key)
        {
            if (!_cache.ContainsKey(key))
            {
                object val = GetConfigValue(key);
                _cache[key] = val;
            }
            return _cache[key];

        }

    }
}
