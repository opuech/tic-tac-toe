namespace TicTacToe.Application
{
    public interface IApplicationEventChannel
    {
        //http://blog.ploeh.dk/2011/09/19/MessageDispatchingwithoutServiceLocation/
        void Submit<DomainEventType>( DomainEventType domainEvent);
        
    }
}
