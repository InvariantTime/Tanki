namespace Tanki.Infrastructure.Authentication
{
    public class AuthOptions
    {
        public string UserIdClaim { get; init; } = string.Empty;

        public string SecretKey { get; init; } = string.Empty;

        public string Cookie { get; init; } = string.Empty;

        public string RoomAccessCookie { get; init; } = string.Empty;

        public double ExpitesHours { get; init; } = 1;
    }
}