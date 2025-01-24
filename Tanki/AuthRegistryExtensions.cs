using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tanki.Infrastructure.Authentication;

namespace Tanki
{
    public static class AuthRegistryExtensions
    {
        private static readonly TimeSpan _cookieExpiration = TimeSpan.FromHours(1);

        public static void RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection(nameof(AuthOptions)).Get<AuthOptions>()!;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey))
                    };

                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = (context) =>
                        {
                            context.Token = context.HttpContext.Request.Cookies[settings.Cookie];
                            return Task.CompletedTask;
                        }
                    };
                });


            services.Configure<AuthOptions>(configuration.GetSection(nameof(AuthOptions)));
        }

        public static void RegisterAuthorization(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, RoomAccessHandler>();
            services.AddAuthorization();
        }
    }
}
