using Microsoft.EntityFrameworkCore;
using Tanki.Domain.Models;
using Tanki.Domain.Repositories;

namespace Tanki.Persistence
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _db;

        public RoomRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Add(Room room)
        {
            await _db.Rooms.AddAsync(room);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Room> GetAll()
        {
            return _db.Rooms;
        }

        public async Task<Room?> GetById(Guid id)
        {
            var result = await _db.Rooms.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task Remove(Guid id)
        {
            var result = await _db.Rooms.FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
                return;

            _db.Rooms.Remove(result);
            await _db.SaveChangesAsync();
        }
    }
}