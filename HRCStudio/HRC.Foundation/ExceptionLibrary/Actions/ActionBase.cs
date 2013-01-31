using System;
using HRC.Foundation.ExceptionLibrary.Publisher;

namespace HRC.Foundation.ExceptionLibrary.Actions
{
    abstract class ActionBase
    {
        public abstract void Action(PublisherList publishers, Exception exp);
    }
}