using Microsoft.AspNetCore.SignalR;
using Tanki.Domain.Models;
using Tanki.Services.Interfaces;

namespace Tanki.Hubs
{
    public class GameStateHandler : IGameStateHandler
    {
        private readonly IHubContext<SessionHub, ISessionHubClient> _hub;

        public GameStateHandler(IHubContext<SessionHub, ISessionHubClient> hub)
        {
            _hub = hub;
        }

        public async Task OnSessionDisconnect(GameSession session)
        {
            await Console.Out.WriteLineAsync($"On disconnect session {session.Id}");
        }
    }
}