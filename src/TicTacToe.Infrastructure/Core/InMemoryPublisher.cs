using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Application.Events;

namespace TicTacToe.Infrastructure.Core
{
    //http://blog.ploeh.dk/2011/09/19/MessageDispatchingwithoutServiceLocation/
    public class InMemoryPublisher : IPublisher
    {
        //private readonly List<IDomainEventHandler> domainEventHandlers;

        public InMemoryPublisher(/*params IDomainEventHandler[] domainEventHandlers*/)
        {
            //if (domainEventHandlers == null)
            //{
            //    throw new ArgumentNullException("domainEventHandlers");
            //}
            //this.domainEventHandlers = domainEventHandlers.ToList();
        }

        //public void AddSubscriber(IDomainEventHandler domainEventHandler)
        //{
        //    domainEventHandlers.Add(domainEventHandler);
        //}
        public void Publish<T>(string id, T objectToSend)
        {
            //foreach (var DomainEventHandler in this.domainEventHandlers)
            //{
            //    if (DomainEventHandler is IDomainEventHandler<DomainEventType>)
            //    {
            //        (DomainEventHandler as IDomainEventHandler<DomainEventType>).Handle((DomainEventType)domainEvent);
            //    }
            //}
        }
    }
}
