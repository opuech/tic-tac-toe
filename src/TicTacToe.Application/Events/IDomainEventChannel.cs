namespace TicTacToe.Application.Events
{
    public interface IDomainEventChannel
    {
        //http://blog.ploeh.dk/2011/09/19/MessageDispatchingwithoutServiceLocation/
        void Submit<DomainEventType>( DomainEventType domainEvent);

        void AddSubscriber(IDomainEventHandler domainEventHandler);

    }
}
