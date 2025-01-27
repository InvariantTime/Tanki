using Tanki.Infrastructure.Intefaces;
using Tanki.Services.Interfaces;

namespace Tanki.Infrastructure.Server
{
    public interface IGameClient : IServerClient
    {
    }

    public class GameServer : ServerHub<IGameClient>
    {
        public GameServer(ISessionService sessions, IAccountService accounts)
            : base(sessions, accounts)
        {
        }


    }
}
