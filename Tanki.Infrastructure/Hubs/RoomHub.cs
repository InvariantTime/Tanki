using Microsoft.AspNetCore.SignalR;

namespace Tanki.Infrastructure.Hubs
{
    public interface IRoomHubClient
    {
        Task OnRoomsChanged();
    }

    public class RoomHub : Hub<IRoomHubClient>
    {
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
