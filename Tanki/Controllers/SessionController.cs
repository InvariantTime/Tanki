using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tanki.Hubs;
using Tanki.Requests;
using Tanki.Responces;
using Tanki.Services;
using Tanki.Services.Interfaces;

namespace Tanki.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _service;
        private readonly RoomHubContext _hub;

        public SessionController(ISessionService service, RoomHubContext hub)
        {
            _service = service;
            _hub = hub;
        }

        [HttpGet("getRooms")]
        public IEnumerable<RoomInfo> GetRooms(
            [FromQuery(Name = "page")] int page = 1, 
            [FromQuery(Name = "pageSize")] int pageSize = 1)
        {
            return GetRoomsInternal(page, pageSize);
        }

        [HttpGet("count")]
        public Task<int> GetCount()
        {
            return Task.FromResult(_service.GetCount());
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]RoomCreateRequest request)
        {
            var userRaw = HttpContext.Session.Get(SessionOptions.SessionUserId);

            if (userRaw == null)
                return Unauthorized();

            var data = new SessionCreationInfo
            {
                MaxPlayerCount = request.PlayerCount,
                Name = request.Name,
                Password = request.Password,
                UserId = new Guid(userRaw)
            };

            var success = await _service.Create(data);

            if (success.IsSuccess == false)
                return BadRequest(success.Error);

            await _hub.OnRoomsChanged();

            return Ok(success.Value!.Id);
        }

        [HttpPost("join")]
        public Task Join([FromBody]JoinRoomRequest request)
        {
            return Task.CompletedTask;
        }

        private IEnumerable<RoomInfo> GetRoomsInternal(int page, int pageSize)
        {
            return _service.GetByPage(page, pageSize)
                .Select(x => new RoomInfo
                {
                    Name = x.Name,
                    Id = x.Id,
                    IsLocked = x.HasPassword,
                    MaxPlayerCount = x.Scene.MaxPlayers,
                    PlayerCount = (uint)x.Scene.Users.Count,
                    HostName = x.Scene.Owner.Name,
                });
        }
    }
}