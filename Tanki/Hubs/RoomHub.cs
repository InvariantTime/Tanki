using Microsoft.AspNetCore.SignalR;

namespace Tanki.Hubs
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
