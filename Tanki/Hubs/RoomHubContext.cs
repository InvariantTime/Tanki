using Microsoft.AspNetCore.SignalR;
using Tanki.Services.Interfaces;

namespace Tanki.Hubs
{
    public class RoomHubContext : IRoomChangedRouter
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
