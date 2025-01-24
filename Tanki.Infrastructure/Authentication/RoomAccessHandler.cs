using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Tanki.Services.Interfaces;

namespace Tanki.Infrastructure.Authentication
{
    public class RoomAccessHandler : AuthorizationHandler<RoomAccessRequirement>
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly AuthOptions _options;

        public RoomAccessHandler(
            IHttpContextAccessor accessor, 
            IServiceScopeFactory factory,
            IOptions<AuthOptions> options)
        {
            _accessor = accessor;
            _scopeFactory = factory;
            _options = options.Value;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            RoomAccessRequirement requirement)
        {
            var request = _accessor.HttpContext!.Request;
            var query = request.Query[requirement.RoomIdQuery];

            using var scope = _scopeFactory.CreateScope();

            if (query.IsNullOrEmpty() == true || Guid.TryParse(query, out var sessionId) == false)
                return Task.CompletedTask;

            var sessions = scope.ServiceProvider.GetRequiredService<ISessionService>();
            var session = sessions.GetById(sessionId);

            if (session.IsSuccess == false)
                return Task.CompletedTask;

            if (session.Value!.HasPassword == false)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var passwordHash = request.Cookies[_options.RoomAccessCookie];

            if (session.Value!.PasswordHash == passwordHash)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}