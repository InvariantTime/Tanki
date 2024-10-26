using Tanki.Domain.Models;

namespace Tanki.Services
{
    public interface IRoomService
    {
        Task CreateRoom(RoomCreationInfo info);

        Task<List<Room>> GetAllByPage(int page, int pageSize);

        Task<int> GetRoomCount();
    }

    public readonly record struct RoomCreationInfo
    {
        public string Name { get; init; }

        public string Password { get; init; }

        public uint MaxPlayerCount { get; init; }

        public Guid UserId { get; init; }
    }
}