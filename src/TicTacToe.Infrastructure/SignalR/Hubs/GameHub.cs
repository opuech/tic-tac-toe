using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace TicTacToe.Infrastructure.SignalR.Hubs
{
    public class GameHub : Hub
    {
        public Task JoinGame(string Id)
        {
            return Groups.Add(Context.ConnectionId, Id);
        }
        
       
        public Task QuitGame(string Id)
        {
            return Groups.Remove(Context.ConnectionId, Id);
        }
    }
}