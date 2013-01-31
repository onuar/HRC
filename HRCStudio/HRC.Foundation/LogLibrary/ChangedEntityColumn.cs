using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRC.Foundation.LogLibrary
{
    public class ChangedEntityColumn
    {
        public ChangedEntityColumn(object oldValue,object  currentValue)
        {
            OldValue = oldValue;
            CurrentValue = currentValue;
        }

        public object CurrentValue { get; set; }
        public object OldValue { get; set; }

        public override string ToString()
        {
            return string.Format("[OldValue: {0}] [CurrentValue: {1}]", OldValue, CurrentValue);
        }
    }
}
