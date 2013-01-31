using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRC.Foundation.ExceptionLibrary.Actions;
using HRC.Foundation.ExceptionLibrary.Entities;
using HRC.Foundation.ExceptionLibrary.Publisher;

namespace HRC.Foundation.ExceptionLibrary.Factories
{
    class DefaultExceptionFactory : ExceptionFactoryBase
    {
        public ExceptionBase Exception
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        internal PublisherBase PublisherBase
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    
        public override PublisherList GetPublisherList()
        {
            PublisherList list = new PublisherList();
            SqlPublisher sql = new SqlPublisher();
            list.Add(sql);
            return list;
        }

        public override ActionBase GetAction()
        {
            return new AllAction();
        }
    }
}
