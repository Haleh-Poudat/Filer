using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using eBooks.ServiceLayer.Contracts.Identity;

namespace eBooks.ServiceLayer.Services.Identity
{
    public class UserAccessAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        #region Fields

        private IRoleManagerService _roleManagerService;
        private readonly string _permissionName;

        #endregion

        #region Constructor
        public UserAccessAttribute(string permissionName)
        {
            _permissionName = permissionName;
        }

        #endregion

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _roleManagerService = context.HttpContext.RequestServices.GetRequiredService<IRoleManagerService>();

            if (context.HttpContext.User.Identity is { IsAuthenticated: true })
            {
                var userClaims = context.HttpContext.User.Claims.ToList();
                var result = userClaims.Any(c => c.Value == _permissionName);
                if (!result)
                    context.Result = new RedirectResult("/LogIn");
            }
            else
            {
                context.Result = new RedirectResult("/LogIn");
            }
        }
    }
}
