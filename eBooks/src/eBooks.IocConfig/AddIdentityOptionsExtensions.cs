using eBooks.Domain.Entities.Identity;
using eBooks.Datalayer.Context;
using eBooks.ServiceLayer.Contracts.Identity;
using eBooks.ServiceLayer.Services.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using eBooks.ViewModel.SiteSetting;

namespace eBooks.IocConfig
{
    public static class AddIdentityOptionsExtensions
    {
        public static IServiceCollection AddCustomIdentityServices(this IServiceCollection services, SiteSettings siteSettings)
        {
            services.AddScoped<IdentityErrorDescriber, CustomIdentityErrorDescriber>();

            services.AddScoped<IUserStoreService, UserStoreService>();
            services.AddScoped<IUserManagerService, UserManagerService>();
            services.AddScoped<IRoleManagerService, RoleManagerService>();
            services.AddScoped<ISignInManagerService, SignInManagerService>();
            services.AddScoped<IRoleStoreService, RoleStoreService>();

            services.AddIdentity<User, Role>(options =>
                {
                    // Password Settings
                    options.Password.RequireDigit = siteSettings.IdentityOptionsSettings.PasswordRequireDigit;
                    options.Password.RequireLowercase = siteSettings.IdentityOptionsSettings.PasswordRequireLowercase;
                    options.Password.RequireUppercase = siteSettings.IdentityOptionsSettings.PasswordRequireUppercase;
                    options.Password.RequireNonAlphanumeric = siteSettings.IdentityOptionsSettings.PasswordRequireNonAlphanumeric;
                    options.Password.RequiredLength = siteSettings.IdentityOptionsSettings.PasswordRequiredLength;

                    // Lock Settings
                    options.Lockout.AllowedForNewUsers = siteSettings.IdentityOptionsSettings.LockoutAllowedForNewUsers;
                    options.Lockout.DefaultLockoutTimeSpan = siteSettings.IdentityOptionsSettings.LockoutDefaultLockoutTimeSpan;
                    options.Lockout.MaxFailedAccessAttempts = siteSettings.IdentityOptionsSettings.LockoutMaxFailedAccessAttempts;

                    // SignIn Settings
                    options.SignIn.RequireConfirmedAccount = siteSettings.IdentityOptionsSettings.SignInRequireConfirmedAccount;
                    options.SignIn.RequireConfirmedPhoneNumber = siteSettings.IdentityOptionsSettings.SignInRequireConfirmedPhoneNumber;
                    options.SignIn.RequireConfirmedEmail = siteSettings.IdentityOptionsSettings.SignInRequireConfirmedEmail;

                    // User Settings
                    options.User.RequireUniqueEmail = siteSettings.IdentityOptionsSettings.UserRequireUniqueEmail;
                })
            .AddUserStore<UserStoreService>()
            .AddUserManager<UserManagerService>()
            .AddRoleStore<RoleStoreService>()
            .AddRoleManager<RoleManagerService>()
            .AddSignInManager<SignInManagerService>()
            .AddErrorDescriber<CustomIdentityErrorDescriber>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/LogIn";
                options.LogoutPath = "/LogOut";
                options.ExpireTimeSpan = siteSettings.IdentityOptionsSettings.AddCookieExpireTimeSpan;
            });

            return services;
        }

        private static void SetIdentityOptions(IdentityOptions options)
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;

            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.MaxFailedAccessAttempts = 6;

            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedPhoneNumber = true;
            options.SignIn.RequireConfirmedEmail = false;

            options.User.RequireUniqueEmail = false;
        }
    }
}