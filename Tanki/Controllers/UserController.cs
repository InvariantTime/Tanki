using Microsoft.AspNetCore.Mvc;
using Tanki.Requests;
using Tanki.Responces;
using Tanki.Services.Interfaces;

namespace Tanki.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _service.CreateUser(request.Name, request.Password);

            if (result.IsSuccess == false)
                return BadRequest(result.Error);

            var user = result.Value!;

            HttpContext.Session.Set(SessionOptions.SessionUserId, user.Id.ToByteArray());

            return Ok();
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            var result = await _service.GetUser(request.Name, request.Password);

            if (result.IsSuccess == false)
                return BadRequest(result.Error);

            HttpContext.Session.Set(SessionOptions.SessionUserId, result.Value!.Id.ToByteArray());

            return Ok();
        }

        [HttpGet("verify")]
        public async Task<IActionResult> Verify()
        {
            if (HttpContext.Session.Keys.Contains(SessionOptions.SessionUserId) == false)
                return Unauthorized();

            var id = HttpContext.Session.Get(SessionOptions.SessionUserId);

            if (id == null)
            {
                HttpContext.Session.Remove(SessionOptions.SessionUserId);
                return StatusCode(500);
            }

            var result = await _service.GetUser(new Guid(id));

            if (result.IsSuccess == false)
            {
                HttpContext.Session.Remove(SessionOptions.SessionUserId);
                return Unauthorized();
            }

            var user = result.Value!;

            return Ok(new VerifyUserResponce(user.Name, user.Score));
        }
    }
}
