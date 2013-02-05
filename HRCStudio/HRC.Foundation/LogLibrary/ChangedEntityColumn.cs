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
            //TODO: transfer these literals to a constant or resource file.
            return string.Format("[OldValue: {0}] [CurrentValue: {1}]", OldValue, CurrentValue);
        }
    }
}
