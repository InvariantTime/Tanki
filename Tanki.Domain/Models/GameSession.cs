using System.Collections.Concurrent;

namespace Tanki.Domain.Models
{
    public class GameSession
    {
        private readonly ConcurrentDictionary<Guid, User> _users;
        private readonly Room _room;
        private readonly Guid _id;

        public Room Room => _room;

        public Guid Id => _id;

        public bool HasHost => 
            _users.FirstOrDefault(x => x.Key == Room.HostId).Value != null;

        public ICollection<User> Users => _users.Values;

        public GameSession(Room room, Guid id)
        {
            _room = room;
            _id = id;
            _users = new();
        }

        public bool AddUser(User user)
        {
            return _users.TryAdd(user.Id, user);
        }

        public bool RemoveUser(Guid user)
        {
            return _users.TryRemove(user, out _);
        }

        public bool IsHost(Guid user)
        {
            return _room.HostId == user;
        }

        public static GameSession Create(Room room)
        {
            var id = Guid.NewGuid();
            return new GameSession(room, id);
        }
    }
}
