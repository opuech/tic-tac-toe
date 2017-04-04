
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Infrastructure;
using TicTacToe.Application.Events;

namespace TicTacToe.Infrastructure.SignalR.Hubs
{
    public class SignalRGamePublisher : IPublisher 
    {
        private IConnectionManager _signalRConnectionManager { get; set; }
        
        public SignalRGamePublisher(IConnectionManager signalRConnectionManager)
        {
            _signalRConnectionManager = signalRConnectionManager;
        }
        public void Publish<T>(string Id, T objectToSend)
        {
            _signalRConnectionManager
                .GetHubContext<GameHub>()
                .Clients
                .Group(Id)
                .publishPost(objectToSend);
        }
    }
}
