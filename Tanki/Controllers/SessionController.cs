using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Tanki.Domain.Models;
using Tanki.Hubs;
using Tanki.Infrastructure.Authentication;
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
        private readonly AuthOptions _options;
        private readonly RoomHubContext _hub;

        public SessionController(
            ISessionService service,
            RoomHubContext hub, 
            IOptions<AuthOptions> options)
        {
            _service = service;
            _options = options.Value;
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
        public async Task<IActionResult> Create(
            [FromBody]RoomCreateRequest request, 
            [FromRoute]User user)
        {
            var data = new SessionCreationInfo
            {
                MaxPlayerCount = request.PlayerCount,
                Name = request.Name,
                Password = request.Password,
                UserId = user.Id
            };

            var result = await _service.Create(data);

            if (result.IsSuccess == false)
                return BadRequest(result.Error);

            await _hub.OnRoomsChanged();

            if (result.Value!.HasPassword == true)
            {
                HttpContext.Response.Cookies
                    .Append(_options.RoomAccessCookie, result.Value.PasswordHash);
            }

            return Ok(result.Value!.Id);
        }

        [HttpPost("access")]
        public IActionResult Access([FromBody]JoinRoomRequest request)
        {
            if (Guid.TryParse(request.Id, out var id) == false)
                return BadRequest("Room not found");

            var result = _service.Access(id, request.Password ?? string.Empty);

            if (result.IsSuccess == false)
                return BadRequest(result.Error);

            HttpContext.Response.Cookies.Append(_options.RoomAccessCookie, result.Value!);

            return Ok();
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