﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using HRC.Library.ContextFoundation.Aspects;
using HRC.Library.ContextFoundation.Aspects.BusinessAspects;

namespace HRC.Library.ContextFoundation
{
    internal class ProxyGenerator<TOrginal, IInterface> : RealProxy
        where TOrginal : new()
    {
        TOrginal _org;

        private ProxyGenerator(TOrginal t)
            : base(typeof(IInterface))
        {
            _org = t;
        }

        public static IInterface GetProxy()
        {
            TOrginal d = new TOrginal();
            ProxyGenerator<TOrginal, IInterface> p = new ProxyGenerator<TOrginal, IInterface>(d);
            return (IInterface)p.GetTransparentProxy();
        }

        public override IMessage Invoke(System.Runtime.Remoting.Messaging.IMessage msg)
        {
            IMethodCallMessage method = msg as IMethodCallMessage;
            if (method == null)
                throw new Exception("Method not found!");

            object returnValue = null;

            IEnumerable<BusinessAspectBase> attributes = Foundation.AttributeLibrary.AttributeHelper.GetMethodAttributes<BusinessAspectBase>(method.MethodBase);
            AspectContext context = new AspectContext() { Method = method };

            try
            {
                IterateAttributesAndReturnTrueIfCancel<WorksBeforeAttribute>(context, attributes);
                returnValue = method.MethodBase.Invoke(_org, method.Args);
                IterateAttributesAndReturnTrueIfCancel<WorksAfterAttribute>(context, attributes);
            }
            catch (Exception exp)
            {
                IterateAttributesAndReturnTrueIfCancel<WorksOnExceptionAttribute>(context, attributes, exp);
            }

            ReturnMessage rm = new ReturnMessage(returnValue, null, 0, method.LogicalCallContext, method);
            return rm;
        }

        private void IterateAttributesAndReturnTrueIfCancel<TAspectOrder>(AspectContext context, IEnumerable<BusinessAspectBase> attributes, Exception exp = null)
        {
            List<MethodInfo> methods = GetAllMethodsInType<TAspectOrder>(attributes);
            if (methods.Count == 0)
                if (exp != null) throw exp;
            foreach (var m in methods)
            {
                m.Invoke(Activator.CreateInstance(m.ReflectedType), new object[] { context });
            }
        }

        private List<MethodInfo> GetAllMethodsInType<TAspectOrder>(IEnumerable<BusinessAspectBase> attributes)
        {
            List<MethodInfo> methodInfos = new List<MethodInfo>();
            foreach (var att in attributes)
            {
                foreach (var m in att.GetType().GetMethods().Where(m => m.GetCustomAttributes(typeof(TAspectOrder), true).Length > 0).ToList())
                {
                    methodInfos.Add(m);
                }
            }

            return methodInfos;
        }
    }
}
