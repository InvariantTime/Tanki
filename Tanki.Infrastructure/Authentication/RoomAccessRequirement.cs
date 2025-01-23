using Microsoft.AspNetCore.Authorization;

namespace Tanki.Infrastructure.Authentication
{
    public class RoomAccessRequirement : IAuthorizationRequirement
    {
        public string RoomIdQuery { get; }

        public RoomAccessRequirement(string roomIdQuery)
        {
            RoomIdQuery = roomIdQuery;
        }
    }
}
