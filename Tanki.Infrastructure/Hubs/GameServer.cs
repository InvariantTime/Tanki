using Tanki.Game.Visualization;
using Tanki.Infrastructure.Server;
using Tanki.Services.Interfaces;

namespace Tanki.Infrastructure.Hubs
{
    public interface IGameClient : IServerClient
    {
        Task VisualizeScene(GameVisual visual);
    }

    public class GameServer : ServerHub<IGameClient>
    {
        public GameServer(
            ISessionService sessions, 
            IAccountService accounts, 
            RoomChangedNotifier notifier) : base(sessions, accounts, notifier)
        {
        }

        public async Task<CodeResult> SendCode(string code)
        {
            var scene = GetSession();
            var user = await GetUser();

            if (scene == null)
                return CodeResult.Failure("internal server error");

            var result = scene.Scene.AddTank(user, code);

            if (result.IsSuccess == false)
                return CodeResult.Failure(result.Error);

            return CodeResult.Success();
        }
    }
}
