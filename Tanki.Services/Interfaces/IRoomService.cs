using Tanki.Domain;
using Tanki.Domain.Models;

namespace Tanki.Services.Interfaces
{
    public interface IRoomService
    {
        Task<List<Room>> GetAllByPage(int page, int pageSize);

        Task<int> GetRoomCount();

        Task<Result> AddRoom(Room room);

        Task DeleteRoom(Guid id);

        Task SetPlayerCount(Guid id, uint count);
    }
}