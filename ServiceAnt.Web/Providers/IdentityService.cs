using System.Web;
using ServiceAnt.Logic.Api.Service;

namespace ServiceAnt.Web.Providers
{
   public class IdentityService : IIdentityService
   {
      public string UserName
      {
         get
         {
            var user = HttpContext.Current.User;
            if (user == null || user.Identity == null || string.IsNullOrEmpty(user.Identity.Name))
               return string.Empty;

            var splittedIdentity = user.Identity.Name.Split('\\');
            return splittedIdentity.Length > 1 ? splittedIdentity[1] : splittedIdentity[0];
         }
      }
   }
}