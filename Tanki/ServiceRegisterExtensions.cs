using Tanki.Domain.Repositories;
using Tanki.Persistence;
using Tanki.Services;

namespace Tanki
{
    static class ServiceRegisterExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IRoomService, RoomService>();
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRoomRepository, RoomRepository>();
        }
    }
}
