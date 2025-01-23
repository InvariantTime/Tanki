using System.Collections.ObjectModel;

namespace Tanki.Domain.Models
{
    public class SessionScene
    {
        private readonly Dictionary<Guid, User> _users;
        private readonly User _owner;

        public uint MaxPlayers { get; }

        public IReadOnlyDictionary<Guid, User> Users { get; }

        public User Owner => _owner;

        public SessionScene(User owner, uint maxPlayers)
        {
            _users = new Dictionary<Guid, User>();
            _owner = owner;

            MaxPlayers = maxPlayers;
            Users = new ReadOnlyDictionary<Guid, User>(_users);
        }

        public Result AddPlayer(User user)
        {
            if (_users.ContainsKey(user.Id) == true)
                return Result.Failure($"{user.Name} is already joined");

            _users.Add(user.Id, user);

            return Result.Success();
        }
    }
}
