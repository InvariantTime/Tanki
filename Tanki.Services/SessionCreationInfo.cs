namespace Tanki.Services
{
    public readonly struct SessionCreationInfo
    {
        public string Name { get; init; }

        public uint MaxPlayerCount { get; init; }

        public Guid UserId { get; init; }

        public string Password { get; init; }
    }
}