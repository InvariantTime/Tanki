using Tanki.Domain;
using Tanki.Domain.Models;
using Tanki.Domain.Repositories;
using Tanki.Services.Interfaces;

namespace Tanki.Services
{
    public class UserService : IUserService
    {
        private const int _startScore = 50;

        private readonly IHashService _hasher;
        private readonly IUserRepository _repository;

        public UserService(IHashService hasher, IUserRepository repository)
        {
            _hasher = hasher;
            _repository = repository;
        }

        public async Task<Result<User>> CreateUser(string name, string password)
        {
            if (await _repository.ContainsWithName(name) == true)
                return Result.Failure<User>("User with such name already is exist");

            var passwordHash = _hasher.CreateHash(password);

            var user = new User
            {
                Name = name,
                PaswordHash = passwordHash,
                Score = _startScore
            };

            await _repository.AddUser(user);

            return Result.Success(user);
        }

        public async Task<Result<User>> GetUser(Guid id)
        {
            var user = await _repository.GetUser(id);

            if (user == null)
                return Result.Failure<User>("Invalid user");

            return Result.Success(user);
        }

        public async Task<Result<User>> GetUser(string name, string password)
        {
            var user = await _repository.GetUserByName(name);

            const string errorMessage = "invalid name or password";

            if (user == null)
                return Result.Failure<User>(errorMessage);

            bool equal = _hasher.Compare(user.PaswordHash, password);

            if (equal == false)
                return Result.Failure<User>(errorMessage);

            return Result.Success(user);
        }
    }
}