using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRC.Foundation.ExceptionLibrary.Actions;
using HRC.Foundation.ExceptionLibrary.Publisher;

namespace HRC.Foundation.ExceptionLibrary.Factories
{
    abstract class ExceptionFactoryBase
    {
        public abstract PublisherList GetPublisherList();
        public abstract ActionBase GetAction();

    }
}
