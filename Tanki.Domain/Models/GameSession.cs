namespace Tanki.Domain.Models
{
    public class GameSession
    {
        public List<User> Users { get; set; } = new();

        public GameSession()
        {

        }
    }
}