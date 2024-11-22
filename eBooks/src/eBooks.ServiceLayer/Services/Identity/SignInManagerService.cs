using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using eBooks.Datalayer.Context;
using eBooks.Domain.Entities.Identity;
using eBooks.ServiceLayer.Contracts.Identity;
using eBooks.ViewModel.Accounts;
using Microsoft.AspNetCore.Authentication.Cookies;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Reflection.Metadata;

namespace eBooks.ServiceLayer.Services.Identity
{
    public class SignInManagerService : SignInManager<User>, ISignInManagerService
    {
        #region Fields

        private readonly IUnitOfWork _uow;
        private readonly DbSet<User> _users;
        private readonly DbSet<Role> _roles;
        private readonly DbSet<UserRole> _userRoles;
        private readonly IUserManagerService _userManagerService;

        #endregion Fields

        #region Constructor

        public SignInManagerService(
            IUserManagerService userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<User> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManagerService> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<User> userConfirmation, IUnitOfWork uow, IUserManagerService userManagerService)

            : base((UserManager<User>)userManager, contextAccessor,
                claimsFactory, optionsAccessor, logger, schemes, userConfirmation)
        {
            _uow = uow;
            _userManagerService = userManagerService;
            _users = _uow.Set<User>();
            _roles = _uow.Set<Role>();
            _userRoles = _uow.Set<UserRole>();
        }

        #endregion Constructor

        #region Custom Methods

        public async Task<IdentityResult> CustomSignInAsync(LogInViewModel viewModel)
        {
            var user = await _users.SingleOrDefaultAsync(p => p.UserName == viewModel.UserName);
            if (user == null) return IdentityResult.Failed();
            if (user.IsDelete)
                return IdentityResult.Failed();

            var isValid = await _userManagerService.CheckPasswordAsync(user, viewModel.Password);
            if (!isValid)
                return IdentityResult.Failed();
            var roleIds = _userRoles.AsQueryable()
                .Where(p => p.UserId == user.Id)
            .Select(p => p.RoleId);
            var roles = await _roles
                .Where(r => roleIds.Contains(r.Id)).Select(r => r.Name)
                .ToListAsync();
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName!));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id!.ToString()));
            claims.Add(new Claim(ClaimTypes.Surname, user.Family));
            claims.Add(new Claim(ClaimTypes.GivenName, user.Name));
            claims.Add(new Claim(ClaimTypes.System, user.IsConstantData.ToString()));

            if (roles.Any())
            {
                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            }
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = viewModel.RememberMe
            };
            await Context.SignInAsync(principal, properties);
            return IdentityResult.Success;
        }

        public async Task CustomLogOutAsync()
        {
            await Context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        #endregion Custom Methods
    }
}