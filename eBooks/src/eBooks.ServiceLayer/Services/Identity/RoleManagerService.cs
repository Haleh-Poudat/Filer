using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using eBooks.Datalayer.Context;
using eBooks.DataLayer.Context;
using eBooks.Domain.Entities.Identity;
using eBooks.ServiceLayer.Contracts.Identity;
using eBooks.ViewModel.Role;
using System.Linq.Dynamic.Core;
using eBooks.ViewModel.User;
using System.Collections.Generic;
using eBooks.ViewModel.SelectViewModel;

namespace eBooks.ServiceLayer.Services.Identity
{
    public class RoleManagerService : RoleManager<Role>, IRoleManagerService
    {
        #region Fields

        private readonly IUnitOfWork _uow;
        private readonly DbSet<Role> _roles;
        private readonly DbSet<UserRole> _userRoles;
        private readonly DbSet<User> _users;
        private readonly IMapper _mapper;

        #endregion Fields

        #region Constructor

        public RoleManagerService(IRoleStoreService store,
            IEnumerable<IRoleValidator<Role>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<RoleManagerService> logger, IUnitOfWork uow, IMapper mapper) : base(
            (RoleStore<Role, BestDbContext, Guid, UserRole, RoleClaim>)store,
            roleValidators, keyNormalizer, errors, logger)
        {
            _uow = uow;
            _mapper = mapper;
            _userRoles = _uow.Set<UserRole>();
            _roles = _uow.Set<Role>();
            _users = _uow.Set<User>();
        }

        #endregion Constructor

        #region Custom Methods

        public async Task<List<SelectListItem>> GetAllRoleAsSelectList()
        {
            var roleList = await _roles.ToListAsync();
            var selectList = roleList.Select(
                    item => new SelectListItem()
                    {
                        Value = item.Id.ToString(),
                        Text = item.Name
                    })
                .ToList();
            return selectList;
        }

        private int GetUserCountInRole(Guid rolId)
        {
            var userIdList = _userRoles.AsNoTracking()
                .Where(p => p.RoleId == rolId)
                .Select(p => p.UserId)
                .ToList();
            var userList = new List<User>();
            foreach (var item in userIdList)
            {
                userList.AddRange(_users.Where(p => p.Id == item && !p.IsConstantData && p.IsDelete == false && !p.IsConstantData));
            }
            return userList.Count;
        }

        public async Task<RoleSearchViewModel> GetAllRoleAsync(RoleSearchViewModel viewModel)
        {
            IQueryable<Role> list = _roles;
            if (!string.IsNullOrWhiteSpace(viewModel.FilterRoleName))
            {
                list = list.Where(p => p.Name.Contains(viewModel.FilterRoleName));
            }

            list = list.OrderBy($"{viewModel.SelectedSortByItem} {viewModel.SelectedOrderByItem}");

            var take = viewModel.Take;
            var skip = (viewModel.PageNumber - 1) * take;
            var pageCount = (list.Count()) / take;
            if ((list.Count() % take) != 0)
            {
                pageCount = (list.Count() / take) + 1;
            }

            var roleList = await list.Skip(skip).Take(take).ToListAsync();

            var roleListViewModel = _mapper.Map<List<RoleViewModel>>(roleList);
            var userList = new List<User>();
            foreach (var item in roleListViewModel)
            {
                item.UserCountInRole = GetUserCountInRole(item.Id);
            }

            var roleSearchViewModel = new RoleSearchViewModel()
            {
                RoleListViewModel = roleListViewModel,
                PageCount = pageCount,
                PageNumber = viewModel.PageNumber,
                RoleSortListItems = SelectListViewModel.RoleSortListItems()
            };
            return roleSearchViewModel;
        }

        public async Task<UserByRoleIdViewModel> UserListByRoleIdAsync(Guid id)
        {
            var userIdList = await _userRoles.AsNoTracking().Where(u => u.RoleId == id)
                  .Select(q => q.UserId).ToListAsync();
            var userList = new List<User>();

            foreach (var item in userIdList)
            {
                userList.AddRange(_users.Where(p => p.Id == item && !p.IsConstantData && !p.IsDelete).ToList());
            }

            var listUserByRoleId = _mapper.Map<List<UserByRoleId>>(userList);
            var userByRoleIdViewModel = new UserByRoleIdViewModel()
            {
                ListUserByRoleId = listUserByRoleId,
                RoleId = id
            };
            return userByRoleIdViewModel;
        }

        public async Task<bool> RemoveUserInRoleAsync(Guid roleId, Guid userId)
        {
            if (_users.FirstOrDefault(p => p.Id == userId)!.IsConstantData)
                return false;
            var userRole = await _userRoles
                .Where(p => p.Roles.Id == roleId && p.Users.Id == userId)
                .FirstOrDefaultAsync();
            var removeUserRole = _userRoles.Remove(userRole);
            await _uow.SaveChangesAsync();
            GetUserCountInRole(roleId);
            return true;
        }

        public async Task<bool> IsExistsRoleNameAsync(string roleName)
        {
            var result = await _roles.AnyAsync(p => p.Name == roleName);
            return result;
        }

        public async Task<IdentityResult> AddRoleAsync(AddRoleViewModel viewModel)
        {
            var role = new Role
            {
                Name = viewModel.Name
            };
            var result = await CreateAsync(role);
            await _uow.SaveChangesAsync();

            return result;
        }

        public async Task<EditRoleViewModel> GetRoleForEditAsync(Guid id)
        {
            var role = await _roles.FindAsync(id);
            var editViewModel = _mapper.Map<EditRoleViewModel>(role);
            return editViewModel;
        }

        public async Task<RoleViewModel> EditRoleAsync(EditRoleViewModel viewModel)
        {
            var role = await _roles.FindAsync(viewModel.Id);
            if (viewModel.NewName != null)
            {
                role.Name = viewModel.NewName;
                await UpdateNormalizedRoleNameAsync(role);
            }

            await _uow.SaveChangesAsync();

            var result = _mapper.Map<RoleViewModel>(role);
            result.UserCountInRole = GetUserCountInRole(viewModel.Id);
            return result;
        }

        public async Task<bool> DeleteRoleAsync(Guid id)
        {
            var role = await _roles.FindAsync(id);
            if (role is { IsConstantData: true })
                return false;
            var user = await _roles.FindAsync(id);
            if (user != null) await DeleteAsync(user);
            return true;
        }

        #endregion Custom Methods
    }
}