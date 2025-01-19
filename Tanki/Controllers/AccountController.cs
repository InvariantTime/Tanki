using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tanki.Infrastructure.Intefaces;
using Tanki.Requests;
using Tanki.Responces;
using Tanki.Services.Interfaces;

namespace Tanki.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accounts;
        private readonly IUserService _users;

        public AccountController(IAccountService accounts, IUserService users)
        {
            _accounts = accounts;
            _users = users;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _accounts.Register(request.Name, request.Password);

            if (result.IsSuccess == false)
                return BadRequest(result.Error);

            HttpContext.Response.Cookies.Append("cookie", result.Value!);

            return Ok();
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            var result = await _accounts.SignIn(request.Name, request.Password);

            if (result.IsSuccess == false)
                return BadRequest(result.Error);

            HttpContext.Response.Cookies.Append("cookie", result.Value!);//TODO: Cookie name from options

            return Ok();
        }

        [Authorize]
        [HttpGet("verify")]
        public async Task<IActionResult> Verify()
        {
            var id = HttpContext.Request.Cookies["cookie"];

            return Ok(new VerifyUserResponce(string.Empty, 0));
        }
    }
}
