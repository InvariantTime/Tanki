using Tanki.Domain.Repositories;
using Tanki.Hubs;
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
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISessionService, SessionService>();
            //services.AddScoped<IGame, Game>();

            services.AddScoped<IHashService, ShaHasher>();

            services.AddSingleton<IRoomChangedRouter, RoomHubContext>();
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}