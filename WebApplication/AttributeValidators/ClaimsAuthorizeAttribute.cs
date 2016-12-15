using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Web.Mvc;

namespace WebApplication.Attribute
{
    public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        private string claimType;
        private string claimValue;

        public ClaimsAuthorizeAttribute(string type, string value)
        {
            this.claimType = type;
            this.claimValue = value;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var identity = (ClaimsIdentity)Thread.CurrentPrincipal.Identity;
            var claimList = identity.Claims.Where(c => c.Type == claimType && c.Value == claimValue).ToList();
            // var claim = identity.Claims.FirstOrDefault(c => c.Type == claimType && c.Value == claimValue);

            foreach (Claim c in claimList)
            {
                if (c != null)
                {
                    base.OnAuthorization(filterContext);
                }
                else
                {
                    HandleUnauthorizedRequest(filterContext);
                }
            }
        }


        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
        }
    }
}