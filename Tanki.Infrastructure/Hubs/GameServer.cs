using Tanki.Infrastructure.Server;
using Tanki.Services.Interfaces;

namespace Tanki.Infrastructure.Hubs
{
    public interface IGameClient : IServerClient
    {
    }

    public class GameServer : ServerHub<IGameClient>
    {
        public GameServer(
            ISessionService sessions, 
            IAccountService accounts, 
            RoomChangedNotifier notifier) : base(sessions, accounts, notifier)
        {
        }
    }
}
