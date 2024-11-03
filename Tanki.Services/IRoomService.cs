using Tanki.Domain.Models;

namespace Tanki.Services
{
    public interface IRoomService
    {
        Task<List<Room>> GetAllByPage(int page, int pageSize);

        Task<int> GetRoomCount();
    }
}