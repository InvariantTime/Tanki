using Microsoft.AspNetCore.SignalR;
using Tanki.Game.Visualization;
using Tanki.Infrastructure.Hubs;
using System.Numerics;

namespace Tanki.Infrastructure
{
    public class GameServerContext
    {
        private readonly IHubContext<GameServer, IGameClient> _hub;

        public GameServerContext(IHubContext<GameServer, IGameClient> hub)
        {
            _hub = hub;
        }

        public Task Visualize(Guid session, GameVisual visual)
        {
            return _hub.Clients.Group(session.ToString())
                .VisualizeScene(visual);
        }
    }
}
