using Microsoft.AspNetCore.SignalR;

namespace Tanki.Hubs
{
    public class RoomHubContext
    {
        private readonly IHubContext<RoomHub, IRoomHubClient> _context;

        public RoomHubContext(IHubContext<RoomHub, IRoomHubClient> context)
        {
            _context = context;
        }

        public async Task OnRoomsChanged()
        {
            await _context.Clients.All
                .OnRoomsChanged();
        }
    }
}
