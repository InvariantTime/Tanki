using Microsoft.AspNetCore.Mvc;
using Tanki.Requests;
using Tanki.Services;

namespace Tanki.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _service;

        public SessionController(ISessionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSession(RoomCreateRequest request)
        {
            var id = HttpContext.Session.Get(SessionOptions.SessionUserId);

            if (id == null)
                return Unauthorized();

            var info = new SessionCreationInfo
            {
                Name = request.Name,
                MaxPlayerCount = request.PlayerCount,
                Password = request.Password,
                UserId = new Guid(id)
            };

            var result =  await _service.CreateSession(info);

            if (result.IsSuccess == false)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
    }
}
