namespace TicTacToe.Application.Events
{
    public interface IDomainEventChannelFactory
    {
        IDomainEventChannel Create(params IDomainEventHandler[] domainEventHandlers);
    }
}
