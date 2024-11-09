using Microsoft.AspNetCore.SignalR;
using Tanki.Domain.Models;
using Tanki.Responces;
using Tanki.Services.Interfaces;

namespace Tanki.Hubs
{
    public interface ISessionHubClient
    {
        Task PlayersChanged(IEnumerable<UserScoreResponce> users);

        Task ConnectionError();

        Task SessionClosed();
    }

    public class SessionHub : Hub<ISessionHubClient>
    {
        private const string _querySession = "sessionId";

        private readonly ISessionService _sessions;

        public SessionHub(ISessionService sessions)
        {
            _sessions = sessions;
        }

        public override async Task OnConnectedAsync()
        {
            var sessionId = Context.GetHttpContext()!.Request.Query[_querySession];

            if (Guid.TryParse(sessionId, out var id) == false || id == Guid.Empty)
            {
                await Clients.Caller.ConnectionError();
                return;
            }

            var userId = Context.GetHttpContext()!.Session.Get(SessionOptions.SessionUserId);

            if (userId == null)
            {
                await Clients.Caller.ConnectionError();
                return;
            }

            var result = await _sessions.Join(id, new Guid(userId));

            if (result.IsSuccess == false)
                return;

            await Groups.AddToGroupAsync(Context.ConnectionId, sessionId!);
            await OnChangedPlayers(sessionId!, result.Value!);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var sessionId = Context.GetHttpContext()!.Request.Query[_querySession];

            if (Guid.TryParse(sessionId, out var id) == false || id == Guid.Empty)
                return;

            var userId = Context.GetHttpContext()!.Session.Get(SessionOptions.SessionUserId);

            if (userId == null)
                return;

            var result = await _sessions.Leave(id, new Guid(userId));

            if (result == SessionLeaveStates.Failure)
                return;

            if (result == SessionLeaveStates.IsHost)
            {
                await Clients.Group(sessionId!).SessionClosed();
                return;
            }

            var session = _sessions.Get(id);

            if (session != null)
                await OnChangedPlayers(sessionId!, session);
        }

        private async Task OnChangedPlayers(string id, GameSession session)
        {
            await Clients.Group(id).PlayersChanged(session.Users
                .Select(x => new UserScoreResponce(x.Name, x.Score)));
        }
    }
}
