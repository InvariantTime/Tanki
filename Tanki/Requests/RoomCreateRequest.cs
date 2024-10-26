namespace Tanki.Requests
{
    public record RoomCreateRequest(
        string Name, 
        string Password, 
        uint PlayerCount);
}