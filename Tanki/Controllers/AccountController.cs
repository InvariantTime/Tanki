using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Tanki.Domain.Models;
using Tanki.Infrastructure;
using Tanki.Infrastructure.Authentication;
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
        private readonly AuthOptions _options;

        public AccountController(IAccountService accounts, IOptions<AuthOptions> options)
        {
            _accounts = accounts;
            _options = options.Value;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _accounts.Register(request.Name, request.Password);

            if (result.IsSuccess == false)
                return BadRequest(result.Error);

            HttpContext.Response.Cookies.Append(_options.Cookie, result.Value!);

            return Ok();
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            var result = await _accounts.SignIn(request.Name, request.Password);

            if (result.IsSuccess == false)
                return BadRequest(result.Error);

            HttpContext.Response.Cookies.Append(_options.Cookie, result.Value!);

            return Ok();
        }

        [Authorize]
        [HttpGet("verify")]
        public Task<IActionResult> Verify(User user)
        {
            IActionResult ok = Ok(new VerifyUserResponce(user.Name, user.Score));
            return Task.FromResult(ok);
        }
    }
}