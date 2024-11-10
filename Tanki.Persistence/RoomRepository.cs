using Microsoft.EntityFrameworkCore;
using Tanki.Domain;
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

        public async Task<Result> Add(Room room)
        {
            var entry = _db.Rooms.Attach(room);
            await _db.SaveChangesAsync();

            return Result.Success();
        }

        public IQueryable<Room> GetAll()
        {
            return _db.Rooms
                .Include(x => x.Host)
                .AsNoTracking();
        }

        public async Task<List<Room>> GetAllByPage(int page, int pageSize)
        {
            return await _db.Rooms
                .AsNoTracking()
                .Include(x => x.Host)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Room?> GetById(Guid id)
        {
            var result = await _db.Rooms
                .Include (x => x.Host)
                .FirstOrDefaultAsync(x => x.Id == id);

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

        public async Task<int> GetCount()
        {
            return await _db.Rooms.CountAsync();
        }

        public async Task SetPlayerCount(Guid id, uint count)
        {
            var room = _db.Rooms.FirstOrDefault(x => x.Id == id);

            if (room == null)
                return;

            room.PlayerCount = count;
            await _db.SaveChangesAsync();
        }
    }
}