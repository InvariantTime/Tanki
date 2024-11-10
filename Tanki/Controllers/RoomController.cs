using Microsoft.AspNetCore.Mvc;
using Tanki.Responces;
using Tanki.Services.Interfaces;

namespace Tanki.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _service;

        public RoomController(IRoomService service)
        {
            _service = service;
        }

        [HttpGet]
        public IAsyncEnumerable<RoomInfo> GetRooms(int page = 1, int pageSize = 1)
        {
            return GetRoomsInternal(page, pageSize);
        }

        [HttpGet("count")]
        public async Task<int> GetCount()
        {
            return await _service.GetRoomCount();
        }

        private async IAsyncEnumerable<RoomInfo> GetRoomsInternal(int page, int pageSize)
        {
            var rooms = await _service.GetAllByPage(page, pageSize);

            foreach (var room in rooms)
            {
                var host = room.Host?.Name ?? string.Empty;

                yield return new RoomInfo
                {
                    Name = room.Name,
                    IsLocked = room.HasPassword,
                    MaxPlayerCount = room.MaxPlayerCount,
                    PlayerCount = room.PlayerCount,
                    HostName = host,
                    Id = room.Id
                };
            }
        }
    }
}