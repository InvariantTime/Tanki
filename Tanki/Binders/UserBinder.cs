using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Tanki.Domain.Models;
using Tanki.Infrastructure.Authentication;
using Tanki.Infrastructure.Intefaces;
using Tanki.Services.Interfaces;

namespace Tanki.Binders
{
    public class UserBinder : IModelBinder
    {
        private readonly IAccountService _accounts;
        private readonly AuthOptions _options;

        public UserBinder(IAccountService accounts, IOptions<AuthOptions> options)
        {
            _accounts = accounts;
            _options = options.Value;
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var http = bindingContext.HttpContext;

            if (http.Request.Cookies.ContainsKey(_options.Cookie) == false)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return;
            }

            var cookie = http.Request.Cookies[_options.Cookie]!;

            var user = await _accounts.GetUser(cookie);

            if (user.IsSuccess == false)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                bindingContext.ModelState.AddModelError(string.Empty, user.Error);
                return;
            }

            bindingContext.Result = ModelBindingResult.Success(user.Value!);
        }
    }
}
