using Tanki.Domain;
using Tanki.Domain.Models;
using Tanki.Domain.Repositories;
using Tanki.Services.Interfaces;

namespace Tanki.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _repository;
        private readonly IRoomChangedRouter _router;
        private readonly IHashService _hasher;

        public RoomService(IRoomRepository repository, IHashService hasher, IRoomChangedRouter router)
        {
            _repository = repository;
            _hasher = hasher;
            _router = router;
        }

        public async Task<Result> AddRoom(Room room)
        {
            var result = await _repository.Add(room);

            if (result.IsSuccess == true)
                await _router.OnRoomsChanged();

            return result;
        }

        public async Task DeleteRoom(Guid id)
        {
            await _repository.Remove(id);
            await _router.OnRoomsChanged();
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

        public async Task SetPlayerCount(Guid id, uint count)
        {
            await _repository.SetPlayerCount(id, count);
            await _router.OnRoomsChanged();
        }
    }
}