using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace HRC.Library.ContextFoundation.Aspects
{
    public class AspectContext
    {
        public IMethodCallMessage Method { get; set; }
    }
}