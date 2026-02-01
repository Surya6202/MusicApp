using System.Web.Mvc;
using System.Web.Routing;

namespace MusicApp.Helpers
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "User", action = "LoginUser" })
                );
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}