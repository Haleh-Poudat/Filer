using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using eBooks.ServiceLayer.Contracts.Identity;
using eBooks.ServiceLayer.Services.Identity;
using eBooks.ViewModel.Role;

namespace eBooks.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly IRoleManagerService _roleManagerService;

        public RoleController(IRoleManagerService roleManagerService)
        {
            _roleManagerService = roleManagerService;
        }

        #region Methods

        [HttpGet]
        public async Task<bool> CheckExistRoleNameAsync(string? name, string? newName)
        {
            if (name != null)
            {
                return !await _roleManagerService.IsExistsRoleNameAsync(name);
            }
            if (newName != null)
            {
                return !await _roleManagerService.IsExistsRoleNameAsync(newName);
            }
            return false;
        }

        [HttpGet]
        [UserAccess("AccessManageRoles")]
        public async Task<IActionResult> Index(RoleSearchViewModel viewModel)
        {
            var model = await _roleManagerService.GetAllRoleAsync(viewModel);
            return View(model);
        }

        [HttpGet]
        [UserAccess("AccessManageRoles")]
        public async Task<IActionResult> GetRoleAjaxAsync(RoleSearchViewModel viewModel)
        {
            var model = await _roleManagerService.GetAllRoleAsync(viewModel);
            return PartialView("_RoleList", model);
        }

        [HttpGet]
        [UserAccess("AccessManageRoles")]
        public async Task<IActionResult> GetUserListByRoleIdAjaxAsync(Guid id)
        {
            if (id == null)
                return BadRequest("The role details are not available.");
            var userListByRoleId = await _roleManagerService.UserListByRoleIdAsync(id);

            return PartialView("_GetUsersByRole", userListByRoleId);
        }

        [HttpGet]
        [UserAccess("AccessAddRole")]
        public async Task<IActionResult> AddAsync()
        {
            var viewModel = new AddRoleViewModel();
            if (ViewData["AllPermissionsList"] == null)
                return BadRequest("A system error has occurred. Please contact support to resolve the issue.");
            return PartialView("_Add", viewModel);
        }

        [HttpPost]
        [UserAccess("AccessAddRole")]
        public async Task<IActionResult> AddAsync(AddRoleViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Please fill out the form information carefully.");
            if (await _roleManagerService.IsExistsRoleNameAsync(viewModel.Name))
                return BadRequest("The role name has already been registered. Please use a different name.");
            if (viewModel.AllPermissionToRoleList == null)
                return BadRequest("The role cannot be without permission. Please select the permissions for the user group.");

            var result = await _roleManagerService.AddRoleAsync(viewModel);

            if (!result.Succeeded)
            {
                List<IdentityError> errorList = result.Errors.ToList();
                var errors = string.Join(", ", errorList.Select(e => e.Description));

                return Content(errors);
            }
            return Json(result.Succeeded ? "success" : "The operation to add the role has failed.");
        }

        [HttpGet]
        [UserAccess("AccessEditRole")]
        public async Task<IActionResult> EditAsync(Guid id)
        {
            if (id == null)
                return BadRequest("The information for the specified role is not available.");

            var viewModel = await _roleManagerService.GetRoleForEditAsync(id);
            if (viewModel == null)
                return BadRequest("The information for the specified role was not found.");

            return PartialView("_Edit", viewModel);
        }

        [HttpPost]
        [UserAccess("AccessEditRole")]
        public async Task<IActionResult> EditAsync(EditRoleViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Please be careful when editing the role name and use only Persian letters.");
            if (viewModel.NewName != null && await _roleManagerService.IsExistsRoleNameAsync(viewModel.NewName!))
                return BadRequest("The new role name is duplicate. Please choose another name.");
            if (viewModel.AllPermissionToRoleList == null)
                return BadRequest("The user group cannot have no permission. Please select the permissions for the role.");

            var role = await _roleManagerService.EditRoleAsync(viewModel);
            return PartialView("_RoleItem", role);
        }

        [HttpPost]
        [UserAccess("AccessEditRole")]
        public async Task<IActionResult> DeleteUserInRoleAjaxAsync(Guid roleId, Guid userId)
        {
            if (roleId == null || userId == null)
                return BadRequest("The related information is not available.");

            var result = await _roleManagerService.RemoveUserInRoleAsync(roleId, userId);

            if (!result)
                return BadRequest("The operation failed.");

            return Json("success");
        }

        [HttpPost]
        [UserAccess("AccessDeleteRole")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (id == null)
                return BadRequest("The information for the specified user group is not available.");

            var result = await _roleManagerService.DeleteRoleAsync(id);
            if (!result)
                return BadRequest("The delete operation failed.");
            return Json("success");
        }

        #endregion Methods
    }
}