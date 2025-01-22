using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Tanki.Domain.Models;

namespace Tanki.Binders
{
    public class CustomBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(User))
                return new BinderTypeModelBinder(typeof(UserBinder));

            return null;
        }
    }
}
