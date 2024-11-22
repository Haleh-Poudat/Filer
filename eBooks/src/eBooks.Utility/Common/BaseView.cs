using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Razor;

namespace eBooks.Utility
{
    public abstract class BaseView<TModel> : RazorPage<TModel>
    {
        public bool IsAuthenticated()
        {
            return User.Identity is { IsAuthenticated: true };
        }

        public bool IsAdmin()
        {
            var userClaims = User.Claims.ToList();
            bool isAdmin = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");

            return isAdmin;
        }

        public bool IsConstant()
        {
            var userClaims = User.Claims.ToList();
            bool isConstant = userClaims.Any(c => c.Type == ClaimTypes.System && c.Value.Equals("true", StringComparison.OrdinalIgnoreCase));

            return isConstant;
        }

        public string FullName1()
        {
            var claims = User.Claims.ToList();
            var name = claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
            var family = claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname).Value;
            return name + " " + family;
        }

        public string FullName()
        {
            var claims = User.Claims.ToList();
            var givenNameClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName);
            var surnameClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname);

            var name = givenNameClaim != null ? givenNameClaim.Value : " ";
            var family = surnameClaim != null ? surnameClaim.Value : " ";

            return name + " " + family;
        }
    }
}