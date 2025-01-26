using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Tanki.Domain.Models;
using Tanki.Game;
using Tanki.Infrastructure.Authentication;
using Tanki.Infrastructure.Intefaces;
using Tanki.Responces;
using Tanki.Services.Interfaces;

namespace Tanki.Hubs
{
    public interface IGameHubClient
    {
        Task OnPlayersChanged(IEnumerable<UserScoreResponce> players);

       // Task Fatal(string message);
    }

    public class GameHub : Hub<IGameHubClient>
    {
        private const string _sessionId = "sessionId";

        private readonly AuthOptions _options;
        private readonly ISessionService _sessions;
        private readonly IAccountService _accounts;

        public GameHub(
            ISessionService sessions, 
            IAccountService accounts,
            IOptions<AuthOptions> options)
        {
            _sessions = sessions;
            _accounts = accounts;
            _options = options.Value;
        }

        public async override Task OnConnectedAsync()
        {
            var user = await GetUser();

            if (user == null)
            {
                Context.Abort();
                return;
            }

            var session = GetSession();

            if (session == null)
            {
                Context.Abort();
                return;
            }

            var result = session.Scene.AddPlayer(user);
            await Groups.AddToGroupAsync(Context.ConnectionId, session.Id.ToString());

            if (result == false)
                return;

            await OnPlayersChanged(session);
            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        private async Task<User?> GetUser()
        {
            var http = Context.GetHttpContext()!;
            var token = http.Request.Cookies[_options.Cookie];

            if (token == null)
                return null;

            var result = await _accounts.GetUser(token);

            return result.Value;
        }

        private GameSession? GetSession()
        {
            var http = Context.GetHttpContext()!;
            var query = http.Request.Query[_sessionId];

            if (query.IsNullOrEmpty() == true)
                return null;

            if (Guid.TryParse(query, out var id) == false)
                return null;

            return _sessions.GetById(id).Value;
        }

        private Task OnPlayersChanged(GameSession session)
        {
            var usersInfo = session.Scene.Users
                .Select(x => new UserScoreResponce(x.Value.Name, x.Value.Score));

            return Clients.Group(session.Id.ToString())
                .OnPlayersChanged(usersInfo);
        }
    }
}
