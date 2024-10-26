using Tanki.Domain.Models;
using Tanki.Domain.Repositories;

namespace Tanki.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _repository;
        private readonly IHashService _hasher;

        public RoomService(IRoomRepository repository, IHashService hasher)
        {
            _repository = repository;
            _hasher = hasher;
        }
        
        public async Task CreateRoom(RoomCreationInfo creation)
        {
            var password = creation.Password == string.Empty ? string.Empty 
                : _hasher.CreateHash(creation.Password);

            var room = new Room
            {
                MaxPlayerCount = creation.MaxPlayerCount,
                Name = creation.Name,
                PasswordHash = password,
                HostId = creation.UserId
            };

            await _repository.Add(room);
        }

        public async Task<List<Room>> GetAllByPage(int page, int pageSize)
        {
            var rooms = await _repository.GetAllByPage(page, pageSize);
            return rooms;
        }

        public async Task<int> GetRoomCount()
        {
            return await _repository.GetCount();
        }
    }
}