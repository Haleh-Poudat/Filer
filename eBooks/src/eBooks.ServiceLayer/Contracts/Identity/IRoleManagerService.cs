using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using eBooks.Domain.Entities.Identity;
using eBooks.ViewModel.Accounts;
using eBooks.ViewModel.Role;
using eBooks.ViewModel.User;

namespace eBooks.ServiceLayer.Contracts.Identity
{
    public interface IRoleManagerService : IDisposable
    {
        #region Main Part

        Task<IdentityResult> CreateAsync(Role role);

        Task UpdateNormalizedRoleNameAsync(Role role);

        Task<IdentityResult> UpdateAsync(Role role);

        Task<IdentityResult> DeleteAsync(Role role);

        Task<bool> RoleExistsAsync(string roleName);

        string NormalizeKey(string key);

        Task<Role> FindByIdAsync(string roleId);

        Task<string> GetRoleNameAsync(Role role);

        Task<IdentityResult> SetRoleNameAsync(Role role, string name);

        Task<string> GetRoleIdAsync(Role role);

        Task<Role> FindByNameAsync(string roleName);

        Task<IdentityResult> AddClaimAsync(Role role, Claim claim);

        Task<IdentityResult> RemoveClaimAsync(Role role, Claim claim);

        Task<IList<Claim>> GetClaimsAsync(Role role);

        ILogger Logger { get; set; }
        IList<IRoleValidator<Role>> RoleValidators { get; }
        IdentityErrorDescriber ErrorDescriber { get; set; }
        ILookupNormalizer KeyNormalizer { get; set; }
        IQueryable<Role> Roles { get; }
        bool SupportsQueryableRoles { get; }
        bool SupportsRoleClaims { get; }

        #endregion Main Part

        #region Custom Part

        public Task<List<SelectListItem>> GetAllRoleAsSelectList();

        public Task<RoleSearchViewModel> GetAllRoleAsync(RoleSearchViewModel viewModel);

        public Task<UserByRoleIdViewModel> UserListByRoleIdAsync(Guid id);

        public Task<bool> RemoveUserInRoleAsync(Guid roleId, Guid userId);

        public Task<bool> IsExistsRoleNameAsync(string roleName);

        public Task<IdentityResult> AddRoleAsync(AddRoleViewModel viewModel);

        public Task<EditRoleViewModel> GetRoleForEditAsync(Guid id);

        public Task<RoleViewModel> EditRoleAsync(EditRoleViewModel viewModel);

        public Task<bool> DeleteRoleAsync(Guid id);

        #endregion Custom Part
    }
}