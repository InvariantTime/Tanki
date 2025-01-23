
namespace Tanki.Domain.Models
{
    public class GameSession
    {
        public Guid Id { get; }

        public string Name { get; }

        public string PasswordHash { get; }

        public bool HasPassword => PasswordHash != string.Empty;

        public SessionScene Scene { get; }

        private GameSession(Guid id, SessionScene scene, string name, string passwordHash)
        {
            Scene = scene;
            Id = id;
            Name = name;
            PasswordHash = passwordHash;
        }

        public static GameSession Create(string name, SessionScene scene, string passwordHash = "")
        {
            var id = Guid.NewGuid();
            var session = new GameSession(id, scene, name, passwordHash);

            return session;
        }
    }
}
