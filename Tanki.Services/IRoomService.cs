using Tanki.Domain.Models;

namespace Tanki.Services
{
    public interface IRoomService
    {
        Task CreateRoom(Room room);

        IQueryable<Room> GetAll();
    }
}