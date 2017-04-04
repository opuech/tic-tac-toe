namespace TicTacToe.Application.Events
{
    public interface IDomainEventHandler
    {
        void Handle(object domainEvent);
    }
    public interface IDomainEventHandler<DomainEventType> : IDomainEventHandler
    {
        void Handle(DomainEventType DomainEvent);
    }
}
