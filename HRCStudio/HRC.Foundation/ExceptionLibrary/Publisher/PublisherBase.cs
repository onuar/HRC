using System;

namespace HRC.Foundation.ExceptionLibrary.Publisher
{
    abstract class PublisherBase
    {
        PublisherList _list;
        public PublisherList Publishers
        {
            get
            {
                if (_list == null)
                    _list = new PublisherList();
                return _list;
            }
        }

        public abstract void Publish(Exception exp);
    }
}
