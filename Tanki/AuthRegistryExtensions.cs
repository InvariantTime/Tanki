using Microsoft.AspNetCore.Authentication.Cookies;

namespace Tanki
{
    public static class AuthRegistryExtensions
    {
        private const string _cookieKey = "Auth:Cookie";
        private static readonly TimeSpan _cookieExpiration = TimeSpan.FromHours(1);

        public static void RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var cookieName = configuration[_cookieKey];

            if (cookieName == null)
                throw new Exception("Auth cookie secret is not defined");

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.Name = cookieName;
                options.IdleTimeout = _cookieExpiration;
                options.Cookie.IsEssential = true;
            });
        }

        public static void RegisterAuthorization(this IServiceCollection services)
        {
        }
    }
}
