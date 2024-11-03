using System.Collections.ObjectModel;

namespace Tanki.Domain.Models
{
    public class GameSession
    {
        private readonly List<User> _users;
        private readonly Room _room;

        public Guid Id { get; }

        public Room Room => _room;

        public IReadOnlyCollection<User> Users { get; }

        public GameSession(Room room)
        {
            _room = room;
            _users = new();

            Users = new ReadOnlyCollection<User>(_users);
            Id = Guid.NewGuid();
        }

        public bool AddUser(User user)
        {
            if (_users.FirstOrDefault(x => x.Id == user.Id) != null)
                return false;

            if (_users.Count >= _room.MaxPlayerCount)
                return false;

            _users.Add(user);
            return true;
        }

        public void RemoveUser(User user)
        {
            
        }
    }
}
