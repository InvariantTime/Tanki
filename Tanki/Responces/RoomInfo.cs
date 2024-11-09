namespace Tanki.Responces
{
    public record RoomInfo
    {
        public string Name { get; init; } = string.Empty;

        public bool IsLocked { get; init; }

        public uint PlayerCount { get; init; }

        public uint MaxPlayerCount { get; init; }
        
        public string HostName { get; init; } = string.Empty;

        public Guid Id { get; init; } = Guid.Empty;
    }
}