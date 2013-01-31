using System;
using System.Collections.Generic;

namespace HRC.Foundation.ExceptionLibrary.Entities
{
    public class ExceptionBase : Exception
    {
        public int MLCode { get; set; }
        Dictionary<string, string> _info;
        public Dictionary<string, string> Info
        {
            get
            {
                if (_info == null)
                    _info = new Dictionary<string, string>();
                return _info;
            }
        }
    }
}
