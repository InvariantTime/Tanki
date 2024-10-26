namespace Tanki.Domain.Models
{
    public class Room
    {
        public Guid Id { get; set; }

        public Guid HostId { get; set; }

        public User? Host { get; set; }

        public string Name { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public bool HasPassword => PasswordHash != string.Empty;

        public uint PlayerCount { get; set; }

        public uint MaxPlayerCount { get; set; }
    }
}