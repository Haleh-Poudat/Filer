﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using eBooks.Domain.Entities.Identity;
using eBooks.ViewModel.Accounts;

namespace eBooks.ServiceLayer.Contracts.Identity
{
    public interface ISignInManagerService
    {
        #region Main Part

        Task<ClaimsPrincipal> CreateUserPrincipalAsync(User user);
        bool IsSignedIn(ClaimsPrincipal principal);
        Task<bool> CanSignInAsync(User user);
        Task RefreshSignInAsync(User user);
        Task SignInAsync(User user, bool isPersistent, string authenticationMethod);
        Task SignInAsync(User user, AuthenticationProperties authenticationProperties, string authenticationMethod);
        Task SignOutAsync();
        Task<User> ValidateSecurityStampAsync(ClaimsPrincipal principal);
        Task<bool> ValidateSecurityStampAsync(User user, string securityStamp);
        Task<User> ValidateTwoFactorSecurityStampAsync(ClaimsPrincipal principal);
        Task<SignInResult> PasswordSignInAsync(User user, string password, bool isPersistent, bool lockoutOnFailure);
        Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);
        Task<SignInResult> CheckPasswordSignInAsync(User user, string password, bool lockoutOnFailure);
        Task<bool> IsTwoFactorClientRememberedAsync(User user);
        Task RememberTwoFactorClientAsync(User user);
        Task ForgetTwoFactorClientAsync();
        Task<SignInResult> TwoFactorRecoveryCodeSignInAsync(string recoveryCode);
        Task<SignInResult> TwoFactorAuthenticatorSignInAsync(string code, bool isPersistent, bool rememberClient);
        Task<SignInResult> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberClient);
        Task<User> GetTwoFactorAuthenticationUserAsync();
        Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent);
        Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent, bool bypassTwoFactor);
        Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync();
        Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string expectedXsrf);
        Task<IdentityResult> UpdateExternalAuthenticationTokensAsync(ExternalLoginInfo externalLogin);
        AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl, string userId);
        ILogger Logger { get; set; }
        UserManager<User> UserManager { get; set; }
        IUserClaimsPrincipalFactory<User> ClaimsFactory { get; set; }
        IdentityOptions Options { get; set; }
        HttpContext Context { get; set; }

        #endregion

        #region Custom Part

        public Task<IdentityResult> CustomSignInAsync(LogInViewModel viewModel);
        public Task CustomLogOutAsync();

        #endregion
    }
}