using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRC.Library.EntityLibrary.EntityBase
{
    public class EntityColumn
    {
        public object CurrentValue { get; set; }
        public object OldValue { get; set; }
        public bool IsChanged { get; set; }
    }
}