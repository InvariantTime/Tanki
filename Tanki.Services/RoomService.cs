using Tanki.Domain.Models;
using Tanki.Domain.Repositories;

namespace Tanki.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _repository;

        public RoomService(IRoomRepository repository)
        {
            _repository = repository;
        }
        
        public async Task CreateRoom(Room room)
        {
            await _repository.Add(room);
        }

        public IQueryable<Room> GetAll()
        {
            return _repository.GetAll();
        }
    }
}