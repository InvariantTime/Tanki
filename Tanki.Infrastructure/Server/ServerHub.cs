using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Tanki.Domain.Models;
using Tanki.Services.Interfaces;

namespace Tanki.Infrastructure.Server
{
    public interface IServerClient
    {
        Task OnPlayersChanged(IEnumerable<UserInfo> users);

        Task Shutdown(string message);
    }

    public abstract class ServerHub<T> : Hub<T> where T : class, IServerClient
    {
        private const string _sessionId = "sessionId";

        private readonly ISessionService _sessions;
        private readonly IAccountService _accounts;
        private readonly RoomChangedNotifier _notifier;

        public ServerHub(
            ISessionService sessions,
            IAccountService accounts,
            RoomChangedNotifier notifier)
        {
            _sessions = sessions;
            _accounts = accounts;
            _notifier = notifier;
        }

        public sealed override async Task OnConnectedAsync()
        {
            var user = await GetUser();
            var session = GetSession();

            if (session == null)
            {
                await Clients.Caller.Shutdown("Unable to connect to session");
                return;
            }

            var result = session.Scene.AddPlayer(user);

            if (result == false)
            {
                await Clients.Caller.Shutdown("Unable to connect");
                return;
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, session.Id.ToString());
            await SendPlayersInfos(session);
            await _notifier.OnRoomsChanged();

            await base.OnConnectedAsync();
        }

        public sealed override async Task OnDisconnectedAsync(Exception? exception)
        {
            var user = await GetUser();
            var session = GetSession();

            if (session == null)
                return;

            session.Scene.RemovePlayer(user);

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, session.Id.ToString());
            await SendPlayersInfos(session);

            if (session.Scene.Owner.Id == user.Id)
                await ShutdownSession(session);

            await _notifier.OnRoomsChanged();
            await base.OnDisconnectedAsync(exception);
        }

        protected GameSession? GetSession()
        {
            var http = Context.GetHttpContext()!;
            var query = http.Request.Query[_sessionId];

            if (query.IsNullOrEmpty() == true)
            {
                Clients.Caller.Shutdown("unable to connect to session");
                return null;
            }

            if (Guid.TryParse(query, out var id) == false)
            {
                Clients.Caller.Shutdown("unable to connect to session");
                return null;
            }

            return _sessions.GetById(id).Value!;
        }

        protected async Task<User> GetUser()
        {
            var http = Context.GetHttpContext()!;

            var result = await _accounts.GetUser(http);

            if (result.IsSuccess == false)
            {
                await Clients.Caller.Shutdown(result.Error);
                throw new HubException(result.Error);
            }

            return result.Value!;
        }

        private Task SendPlayersInfos(GameSession session)
        {
            var users = session.Scene.Users
                .Select(x => new UserInfo(x.Value.Name, x.Value.Score));

            return Clients
                .Group(session.Id.ToString())
                .OnPlayersChanged(users);
        }

        private async Task ShutdownSession(GameSession session)
        {
            await Clients.Group(session.Id.ToString())
                .Shutdown("host closed session");
            
            _sessions.Remove(session.Id);
        }
    }
}