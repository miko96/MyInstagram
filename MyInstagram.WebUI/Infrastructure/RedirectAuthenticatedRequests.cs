using System.Web.Mvc;
using System.Web.Routing;

namespace MyInstagram.WebUI.Infrastructure
{
    public class RedirectAuthenticatedRequestsAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
             //var userManager = filterContext.HttpContext.GetOwinContext().Authentication;
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new
                    {
                        controller = "User",
                        action = "Page"
                    }
                ));
            }

            
        }
    }
}