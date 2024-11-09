using Tanki.Domain.Models;

namespace Tanki.Domain.Repositories
{
    public interface IRoomRepository
    {
        Task Remove(Guid id);

        Task<Result> Add(Room room);

        Task<Room?> GetById(Guid id);
      
        IQueryable<Room> GetAll();

        Task<int> GetCount();

        Task<List<Room>> GetAllByPage(int page, int pageSize);
    }
}