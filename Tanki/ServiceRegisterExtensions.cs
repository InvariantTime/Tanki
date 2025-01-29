using Tanki.Domain.Repositories;
using Tanki.Infrastructure;
using Tanki.Persistence;
using Tanki.Services;
using Tanki.Services.Hashers;
using Tanki.Services.Interfaces;

namespace Tanki
{
    static class ServiceRegisterExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISessionService, SessionService>();
            //services.AddScoped<IGame, Game>();

            services.AddScoped<IHashService, ShaHasher>();
            services.AddScoped<RoomChangedNotifier>();
            services.AddScoped<IAccountService, AccountService>();
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}