using System.Collections.Concurrent;
using System.Collections.ObjectModel;

namespace Tanki.Domain.Models
{
    public class SessionScene
    {
        private readonly ConcurrentDictionary<Guid, User> _users;
        private readonly User _owner;

        public uint MaxPlayers { get; }

        public IReadOnlyDictionary<Guid, User> Users { get; }

        public User Owner => _owner;

        public SessionScene(User owner, uint maxPlayers)
        {
            _users = new();
            _owner = owner;

            MaxPlayers = maxPlayers;
            Users = new ReadOnlyDictionary<Guid, User>(_users);
        }

        public AddingUserResults AddPlayer(User user)
        {
            if (_users.ContainsKey(user.Id) == true)
                return AddingUserResults.HasAlready;

            var result = _users.TryAdd(user.Id, user);

            return result == true ? AddingUserResults.Success 
                : AddingUserResults.Error;
        }

        public bool RemovePlayer(User user)
        {
            return _users.TryRemove(user.Id, out _);
        }
    }

    public enum AddingUserResults
    {
        Success,
        HasAlready,
        Error
    }
}
