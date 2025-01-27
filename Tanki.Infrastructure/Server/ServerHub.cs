using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Tanki.Domain.Models;
using Tanki.Infrastructure.Intefaces;
using Tanki.Services.Interfaces;

namespace Tanki.Infrastructure.Server
{
    public interface IServerClient
    {
        Task OnPlayersChanged(IEnumerable<UserInfo> users);

        Task OnFatal(string message);
    }

    public abstract class ServerHub<T> : Hub<T> where T : class, IServerClient
    {
        private const string _sessionId = "sessionId";

        private readonly ISessionService _sessions;
        private readonly IAccountService _accounts;

        public ServerHub(ISessionService sessions, IAccountService accounts)
        {
            _sessions = sessions;
            _accounts = accounts;
        }

        public sealed override async Task OnConnectedAsync()
        {
            var user = await GetUser();
            var session = GetSession();

            var result = session.Scene.AddPlayer(user);

            if (result == AddingUserResults.Error)
            {
                await Clients.Caller.OnFatal("Unable to connect");
                return;
            }

            if (result == AddingUserResults.HasAlready)
                await Groups.AddToGroupAsync(Context.ConnectionId, session.Id.ToString());

            await SendPlayersInfos(session);

            await base.OnConnectedAsync();
        }

        public sealed override async Task OnDisconnectedAsync(Exception? exception)
        {
            var user = await GetUser();
            var session = GetSession();

            session.Scene.RemovePlayer(user);

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, session.Id.ToString());
            await SendPlayersInfos(session);

            if (session.Scene.Owner.Id == user.Id)
                await ShutdownSession();

            await base.OnDisconnectedAsync(exception);
        }

        protected GameSession GetSession()
        {
            var http = Context.GetHttpContext()!;
            var query = http.Request.Query[_sessionId];

            if (query.IsNullOrEmpty() == true)
            {
                Clients.Caller.OnFatal("unable to connect to session");
                throw new HubException();
            }

            if (Guid.TryParse(query, out var id) == false)
            {
                Clients.Caller.OnFatal("unable to connect to session");
                throw new HubException();
            }

            return _sessions.GetById(id).Value!;
        }

        protected async Task<User> GetUser()
        {
            var http = Context.GetHttpContext()!;

            var result = await _accounts.GetUser(http);

            if (result.IsSuccess == false)
            {
                await Clients.Caller.OnFatal(result.Error);
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

        private Task ShutdownSession()
        {
            return Task.CompletedTask;
        }
    }
}