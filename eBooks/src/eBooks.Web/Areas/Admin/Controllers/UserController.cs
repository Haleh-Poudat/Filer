using Microsoft.AspNetCore.Mvc;
using eBooks.ServiceLayer.Contracts.Identity;
using eBooks.ViewModel.User;
using eBooks.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using eBooks.Utility.Common;
using eBooks.ViewModel.SiteSetting;
using Microsoft.Extensions.Options;
using eBooks.Utility.Constants;

namespace eBooks.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : BaseController
    {
        #region Fields

        private readonly IUserManagerService _userManagerService;
        private readonly IRoleManagerService _roleManagerService;
        private readonly SiteSettings _siteSettings;
        private readonly PasswordBanListSettings _banList;

        #endregion Fields

        public UserController(IUserManagerService userManagerService, IRoleManagerService roleManagerService, IOptionsMonitor<SiteSettings> siteSettings)
        {
            _userManagerService = userManagerService;
            _roleManagerService = roleManagerService;
            _siteSettings = siteSettings.CurrentValue;
            _banList = _siteSettings.PasswordBanListSettings;
        }

        #region Methods

        [HttpPost]
        public JsonResult IsNationalCodeValid(string userName)
        {
            var isvalid = userName.IsValidNationalCode();
            if (!isvalid) return Json("The entered national ID is not valid.");
            return Json(true);
        }

        [HttpGet]
        public async Task<bool> CheckExistUserNameAsync(string? userName, Guid id)
        {
            if (userName != null)
            {
                var result = !await _userManagerService.IsExistUserNameAsync(userName, id);
                return result;
            }
            return false;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(ListViewModel viewModel, bool isDelete)
        {
            var model = await _userManagerService.GetAllUserAsync(viewModel, isDelete);
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserAjax(ListViewModel viewModel, bool isDelete, int pageNumber)
        {
            var model = await _userManagerService.GetAllUserAsync(viewModel, isDelete);
            if (model.UserListViewModel == null || !model.UserListViewModel.Any()) return Content("no-more-info");
            return PartialView("_UserList", model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAsync()
        {
            var model = new AddUserViewModel()
            {
                ListAllRoles = await _roleManagerService.GetAllRoleAsSelectList()
            };
            if (model.ListAllRoles == null)
                return BadRequest("A system error has occurred. Please follow up with support for assistance.");
            return PartialView("_Add", model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAsync(AddUserViewModel viewModel)
        {
            if (!string.IsNullOrEmpty(viewModel.Password))
            {
                var password = viewModel.Password.ToLower();
                if (GetPasswordBanList().Contains(password))
                    return BadRequest("The password is weak. Please choose a stronger password.");
            }

            if (await _userManagerService.IsExistUserNameAsync(viewModel.UserName, new Guid()))
                return BadRequest("This national ID has already been registered.");

            var result = await _userManagerService.AddUserAsync(viewModel);
            return PartialView("_UserItem", result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return BadRequest("The national ID cannot be empty.");

            var viewModel = await _userManagerService.GetUserForEditAsync(userName);
            if (viewModel == null)
                return BadRequest("Editing the specified user is not possible. Please contact support for further assistance.");

            return PartialView("_Edit", viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditAsync(EditUserViewModel viewModel)
        {
            if (!string.IsNullOrEmpty(viewModel.Password))
            {
                var password = viewModel.Password.ToLower();
                if (GetPasswordBanList().Contains(password))
                    return BadRequest("The password is weak. Please choose a stronger password.");
            }

            if (!ModelState.IsValid)
                return BadRequest("Please enter the field values carefully.");

            if (!string.IsNullOrWhiteSpace(viewModel.UserName) &&
                await _userManagerService.IsExistUserNameAsync(viewModel.UserName, viewModel.Id))
                return BadRequest("The username is already taken.");
            var model = await _userManagerService.EditUserAsync(viewModel);
            return PartialView("_UserItem", model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (!await _userManagerService.IsExistIdAsync(id))
                return BadRequest("The specified user was not found in the system.");

            var result = await _userManagerService.DeleteUserAsync(id);
            return Json(result.Succeeded ? "success" : "error");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ReactivateAsync(Guid id)
        {
            if (!await _userManagerService.IsExistIdAsync(id))
                return BadRequest("The user was not found in the system.");

            var result = await _userManagerService.ReactivateUserAsync(id);
            return Json(result.Succeeded ? "success" : "error");
        }

        public List<string> GetPasswordBanList()
        {
            var list = _banList.PasswordBanList.Select(p => p.ToLower()).ToList();
            return list;
        }

        #endregion Methods

        #region Change Password

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ChangePasswordAsync()
        {
            var viewModel = new ChangePasswordViewModel();
            if (User.Identity is { IsAuthenticated: true })
            {
                var userName = GetCurrentUserName();
                var userViewModel = await _userManagerService.GetUserForEditAsync(userName);
                if (userViewModel.IsConstantData)
                    return BadRequest("The system user cannot be edited.");
                viewModel.Name = userViewModel.Name;
                viewModel.Family = userViewModel.Family;
                viewModel.UserName = userViewModel.UserName;
                return PartialView("_ChangePassword", viewModel);
            }
            return BadRequest();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordViewModel viewModel)
        {
            var password = viewModel.NewPassword.ToLower();
            if (GetPasswordBanList().Contains(password))
                return BadRequest("The password is weak. Please choose a stronger password.");

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", MessageText.WrongInput);
                return BadRequest("The entered information is incorrect.");
            }

            var exist = await _userManagerService.IsExistPasswordAsync(viewModel.UserName, viewModel.CurrentPassword);
            if (!exist)
                return BadRequest("The entered information is incorrect.");

            var result = await _userManagerService.ChangeUserPasswordAsync(viewModel); ;
            if (result.Succeeded)
            {
                await _userManagerService.ChangeSecurityStampAsync(viewModel.UserName);
                return Json(result.Succeeded ? "success" : "error");
            }

            return Json("error");
        }

        #endregion Change Password
    }
}