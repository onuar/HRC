using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRC.Foundation.ExceptionLibrary.Actions;
using HRC.Foundation.ExceptionLibrary.Factories;
using HRC.Foundation.ExceptionLibrary.Publisher;

namespace HRC.Foundation.ExceptionLibrary
{
    public class ExceptionManager
    {
        public static void Handle(Exception exp)
        {
            ExceptionFactoryBase facto = ExceptionHandlerFactory.GetHandlerFactory(exp);
            PublisherList publishers = facto.GetPublisherList();
            ActionBase action = facto.GetAction();
            action.Action(publishers, exp);
        }
    }
}
