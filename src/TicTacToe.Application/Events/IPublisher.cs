
namespace TicTacToe.Application.Events
{
    public interface IPublisher
    {
        void Publish<T>(string id, T objectToSend);
    }
}
