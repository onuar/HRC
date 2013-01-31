using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRC.Foundation.ExceptionLibrary.Actions;
using HRC.Foundation.ExceptionLibrary.Entities;
using HRC.Foundation.ExceptionLibrary.Publisher;

namespace HRC.Foundation.ExceptionLibrary.Factories
{
    class CleExceptionFactory : ExceptionFactoryBase
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
            TextPublisher text = new TextPublisher();
            EmailPublisher email = new EmailPublisher();
            list.Add(sql);
            list.Add(text);
            list.Add(email);

            return list;
        }

        public override ActionBase GetAction()
        {
            return new AllAction();
        }
    }
}
