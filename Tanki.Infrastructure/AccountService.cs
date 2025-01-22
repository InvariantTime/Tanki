using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tanki.Domain;
using Tanki.Domain.Models;
using Tanki.Infrastructure.Intefaces;
using Tanki.Services.Interfaces;

namespace Tanki.Infrastructure
{
    public class AccountService : IAccountService
    {
        private readonly AuthOptions _options;
        private readonly IUserService _users;
        private readonly JwtSecurityTokenHandler _jwtHandler;

        public AccountService(IUserService users, IOptions<AuthOptions> options)
        {
            _users = users;
            _options = options.Value;
            _jwtHandler = new();
        }

        public async Task<Result<string>> SignIn(string name, string password)
        {
            var user = await _users.GetUser(name, password);

            if (user.IsSuccess == false)
                return Result.Failure<string>(user.Error);

            var token = GenerateToken(user.Value!);

            return Result.Success(token);
        }

        public async Task<Result<string>> Register(string name, string password)
        {
            var user = await _users.CreateUser(name, password);

            if (user.IsSuccess == false)
                return Result.Failure<string>(user.Error);

            var token = GenerateToken(user.Value!);

            return Result.Success(token);
        }

        public async Task<Result<User>> GetUser(string key)
        {
            var token = _jwtHandler.ReadJwtToken(key);

            var claim = token.Claims.First(x => x.Type == _options.UserIdClaim);

            if (Guid.TryParse(claim.Value, out var id) == false)
                return Result.Failure<User>("Unable to get user");

            return await _users.GetUser(id);
        }

        private string GenerateToken(User user)
        {
            Claim[] claims = [new Claim(_options.UserIdClaim, user.Id.ToString())];

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddHours(_options.ExpitesHours));

            return _jwtHandler.WriteToken(token);
        }
    }
}
