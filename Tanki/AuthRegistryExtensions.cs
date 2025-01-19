using Microsoft.AspNetCore.Authentication.JwtBearer;
using Tanki.Infrastructure;

namespace Tanki
{
    public static class AuthRegistryExtensions
    {
        private const string _cookieKey = "cookie";//TODO: cookie name from options
        private static readonly TimeSpan _cookieExpiration = TimeSpan.FromHours(1);

        public static void RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = OnMessageRecived
                    };
                });
        }

        public static void RegisterAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("roomPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                });
            });
        }

        private static Task OnMessageRecived(MessageReceivedContext context)
        {
            context.Token = context.HttpContext.Request.Cookies[_cookieKey];
            return Task.CompletedTask;
        }
    }
}
