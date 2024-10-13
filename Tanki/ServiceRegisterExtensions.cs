using Tanki.Domain.Repositories;
using Tanki.Hubs;
using Tanki.Persistence;
using Tanki.Services;
using Tanki.Services.Hashers;

namespace Tanki
{
    static class ServiceRegisterExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IHashService, ShaHasher>();

            services.AddSingleton<RoomHubContext>();
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}