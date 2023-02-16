using Microsoft.AspNetCore.Mvc.Filters;
using Fedorakin.CashDesk.Web.Exceptions;

namespace Fedorakin.CashDesk.Web.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

        if (allowAnonymous)
        {
            return;
        }

        var user = context.HttpContext.Items["AdminName"];

        if (user == null)
        {
            throw new UnauthorizedException();
        }
    }
}
