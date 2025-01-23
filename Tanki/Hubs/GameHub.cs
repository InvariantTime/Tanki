using Microsoft.AspNetCore.SignalR;
using Tanki.Services.Interfaces;

namespace Tanki.Hubs
{
    public interface IGameHubClient
    {
    }

    public class GameHub : Hub<IGameHubClient>
    {
        private readonly ISessionService _sessions;
        private readonly IUserService _users;

        public GameHub(ISessionService sessions, IUserService users)
        {
            _sessions = sessions;
            _users = users;
        }
    }
}
