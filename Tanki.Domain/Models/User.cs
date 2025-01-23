namespace Tanki.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string PaswordHash { get; set; } = string.Empty;

        public int Score { get; set; }
    }
}