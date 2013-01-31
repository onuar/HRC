﻿using System;
using HRC.Foundation.ExceptionLibrary.Publisher;

namespace HRC.Foundation.ExceptionLibrary.Actions
{
    class AllAction : ActionBase
    {
        public override void Action(PublisherList publishers, Exception exp)
        {
            foreach (PublisherBase publisher in publishers)
            {
                try
                {
                    publisher.Publish(exp);
                }
                catch
                {

                }
            }
        }
    }
}
