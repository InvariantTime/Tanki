using Tanki.Domain.Models;

namespace Tanki.Domain.Repositories
{
    public interface IRoomRepository
    {
        Task Remove(Guid id);

        Task Add(Room room);

        Task<Room?> GetById(Guid id);
        
        //TODO: Add filter
        IQueryable<Room> GetAll();
    }
}