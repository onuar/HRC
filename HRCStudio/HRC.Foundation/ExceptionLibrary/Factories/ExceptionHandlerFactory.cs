using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRC.Foundation.ExceptionLibrary.Entities;

namespace HRC.Foundation.ExceptionLibrary.Factories
{
    class ExceptionHandlerFactory
    {
        internal ExceptionFactoryBase ExceptionFactoryBase
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    
        internal static ExceptionFactoryBase GetHandlerFactory(Exception exp)
        {
            if (exp is CoreLevelException)
            {
                return new CleExceptionFactory();
            }
            else if (exp is UserLevelException)
            {
                return new UleExceptionFactory();
            }
            else
                return new DefaultExceptionFactory();
        }

        private static ExceptionFactoryBase DefaultExceptionFactory()
        {
            throw new NotImplementedException();
        }
    }
}
