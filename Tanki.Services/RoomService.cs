using Tanki.Domain.Models;
using Tanki.Domain.Repositories;
using Tanki.Services.Interfaces;

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