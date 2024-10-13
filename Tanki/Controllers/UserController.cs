using Microsoft.AspNetCore.Mvc;
using Tanki.Requests;
using Tanki.Responces;
using Tanki.Services;

namespace Tanki.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private const string _sessionIdKey = "UserId";

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

            HttpContext.Session.SetString(_sessionIdKey, user.Id.ToString());

            return Ok();
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            var result = await _service.GetUser(request.Name, request.Password);

            if (result.IsSuccess == false)
                return BadRequest(result.Error);

            HttpContext.Session.Set(_sessionIdKey, result.Value!.Id.ToByteArray());

            return Ok();
        }

        [HttpGet("verify")]
        public async Task<IActionResult> Verify()
        {
            if (HttpContext.Session.Keys.Contains(_sessionIdKey) == false)
                return Unauthorized();

            var id = HttpContext.Session.Get(_sessionIdKey);

            if (id == null)
            {
                HttpContext.Session.Remove(_sessionIdKey);
                return StatusCode(500);
            }

            var result = await _service.GetUser(new Guid(id));

            if (result.IsSuccess == false)
            {
                HttpContext.Session.Remove(_sessionIdKey);
                return Unauthorized();
            }

            var user = result.Value!;

            return Ok(new VerifyUserResponce(user.Name, user.Score));
        }
    }
}
