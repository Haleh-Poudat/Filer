using System.Security.Claims;
using System.Text.Json;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using eBooks.ServiceLayer.Contracts.Identity;
using eBooks.Utility.Constants;
using eBooks.ViewModel.Accounts;
using Microsoft.AspNetCore.Identity;
using eBooks.ViewModel.SiteSetting;
using Microsoft.Extensions.Options;

namespace eBooks.Web.Controllers
{
    public class AccountController : BaseController
    {
        #region Fields

        private readonly IUserManagerService _userManagerService;
        private readonly ISignInManagerService _signInManagerService;
        private readonly ICaptchaValidator _captchaValidator;

        #endregion Fields

        #region Constructor

        public AccountController(
            IUserManagerService userManagerService,
            ISignInManagerService signInManagerService,
            ICaptchaValidator captchaValidator,
            ILogger<AccountController> logger)
        {
            _userManagerService = userManagerService;
            _signInManagerService = signInManagerService;
            _captchaValidator = captchaValidator;
        }

        #endregion Constructor

        [HttpGet]
        [Route("LogIn")]
        public IActionResult LogInAsync(string? returnUrl = "")
        {
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                ViewData["ReturnUrl"] = returnUrl;
            }
            return View();
        }

        [HttpPost]
        [Route("LogIn")]
        public async Task<IActionResult> LogInAsync(LogInViewModel viewModel, string? returnUrl = "")
        {
            if (User.Identity is { IsAuthenticated: true })
            {
                ShowMessage(SwalIcon.error, SwalColor.error, MessageText.IsAuthenticated);
                return RedirectToAction("Index", "Library");
            }

            if (!ModelState.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(returnUrl))
                {
                    ShowMessage(SwalIcon.error, SwalColor.error, MessageText.WrongInput);
                    return RedirectToAction("LogIn", "Account", new { returnUrl = returnUrl });
                }

                ShowMessage(SwalIcon.error, SwalColor.error, MessageText.WrongInput);
                return View(viewModel);
            }

            if (!await _userManagerService.IsExistUserNameAsync(viewModel.UserName, new Guid()))
            {
                ShowMessage(SwalIcon.error, SwalColor.error, MessageText.UserNameOrPasswordFailure);
                return View(viewModel);
            }

            if (await _userManagerService.UserIsLockedOutAsync(viewModel.UserName))
            {
                ShowMessage(SwalIcon.error, SwalColor.error, MessageText.IsLockedOut);
                return View(viewModel);
            }
            var result = await _signInManagerService.CustomSignInAsync(viewModel);
            if (result.Succeeded)
            {
                await _userManagerService.ClearAccessFailedCountAsync(viewModel.UserName);
                return RedirectToAction("Index", "Library");
            }
            ShowMessage(SwalIcon.error, SwalColor.error, MessageText.UserNameOrPasswordFailure);
            return View(viewModel);
        }

        [HttpGet]
        [Route("LogOut")]
        public async Task<IActionResult> LogOutAsync()
        {
            var userName = User.FindFirstValue(ClaimTypes.Name);
            if (userName != null)
            {
                await _signInManagerService.CustomLogOutAsync();
                await _userManagerService.ChangeSecurityStampAsync(userName);
            }
            return RedirectToAction("LogIn", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}