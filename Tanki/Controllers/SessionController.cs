using Microsoft.AspNetCore.Mvc;
using Tanki.Requests;
using Tanki.Services;
using Tanki.Services.Interfaces;

namespace Tanki.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessions;

        public SessionController(ISessionService session)
        {
            _sessions = session;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSession(RoomCreateRequest request)
        {
            var id = HttpContext.Session.Get(SessionOptions.SessionUserId);

            if (id == null)
                return Unauthorized();

            var result = await _sessions.CreateSession(new SessionCreationInfo()
            {
                MaxPlayerCount = request.PlayerCount,
                Name = request.Name,
                Password = request.Password,
                UserId = new Guid(id)
            });

            if (result.IsSuccess == false)
                return BadRequest(result.Error);

            return Ok(result.Value!);
        }

        [HttpPost("join")]
        public Task<IActionResult> JoinSession()
        {
            return Task.FromResult((IActionResult)Ok());
        }
    }
}
