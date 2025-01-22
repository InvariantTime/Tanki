namespace Tanki.Requests
{
    public record JoinRoomRequest(string Id, string? Password = null);
}