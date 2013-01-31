using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRC.Foundation.LogLibrary
{
    public class LogContext
    {
        public string TableName { get; set; }
        public string EntityName { get; set; }

        Dictionary<string, ChangedEntityColumn> _values;
        public Dictionary<string, ChangedEntityColumn> Values
        {
            get
            {
                if (_values == null)
                    _values = new Dictionary<string, ChangedEntityColumn>();
                return _values;
            }
            set
            {
                _values = value;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, ChangedEntityColumn> kvp in _values)
            {
                sb.AppendLine(string.Format("EN: {0}, TBL: {1}, COL: {2}, VAL: {3}", this.EntityName, this.TableName, kvp.Key, kvp.Value.ToString()));
            }

            return sb.ToString();
        }
    }
}
