using System.Web.Mvc;
using System.Web.Routing;

namespace ServiceAnt.Web.Utils
{
   public class AuthorizeUser : AuthorizeAttribute
   {
      protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
      {
         if (filterContext.HttpContext.Request.IsAuthenticated)
         {
            filterContext.Result = new RedirectToRouteResult(new
               RouteValueDictionary(new { controller = "Account", action = "AccessDenied" }));
         }
         else
         {
            base.HandleUnauthorizedRequest(filterContext);
         }
      }
   }
}