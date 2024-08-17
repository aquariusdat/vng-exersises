using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace VNGExercises.Application.Attributes
{
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context != null)
            {
                var xAuthHeader = context.HttpContext.Request.Headers.FirstOrDefault(t => t.Key == "xAuth");

                if (!xAuthHeader.Value.Any() || String.IsNullOrEmpty(xAuthHeader.Value))
                {
                    context.Result = new UnauthorizedObjectResult(string.Empty);
                    return;
                }
            }
        }
    }
}
