using Microsoft.AspNetCore.SignalR;
using Tanki.Infrastructure.Hubs;

namespace Tanki.Infrastructure
{
    public class RoomChangedNotifier
    {
        private readonly IHubContext<RoomHub, IRoomHubClient> _context;

        public RoomChangedNotifier(IHubContext<RoomHub, IRoomHubClient> context)
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
