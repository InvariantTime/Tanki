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

        public AccountService(IUserService users)
        {
            _users = users;
            _options = new AuthOptions();
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

            var handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }
    }
}
