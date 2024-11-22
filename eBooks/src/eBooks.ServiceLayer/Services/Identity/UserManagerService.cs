using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using eBooks.Datalayer.Context;
using eBooks.DataLayer.Context;
using eBooks.Domain.Entities.Identity;
using eBooks.ServiceLayer.Contracts.Identity;
using eBooks.ViewModel.Accounts;
using eBooks.ViewModel.User;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Http;

namespace eBooks.ServiceLayer.Services.Identity
{
    public class UserManagerService : UserManager<User>, IUserManagerService
    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly DbSet<User> _user;
        private readonly IRoleManagerService _roleManagerService;
        private readonly DbSet<UserRole> _userRoles;
        private readonly IHttpContextAccessor _httpContextAccessore;

        #endregion Fields

        #region Constructor

        public UserManagerService(IUserStoreService store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManagerService> logger,
            IUnitOfWork uow,
            IMapper mapper, IRoleManagerService roleManagerService, IHttpContextAccessor httpContextAccessore)
            : base((UserStore<User, Role, BestDbContext, Guid, UserClaim,
                    UserRole, UserLogin, UserToken, RoleClaim>)store,
                optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services,
                logger)
        {
            _uow = uow;
            _mapper = mapper;
            _roleManagerService = roleManagerService;
            _httpContextAccessore = httpContextAccessore;
            _user = _uow.Set<User>();
            _userRoles = _uow.Set<UserRole>();
        }

        #endregion Constructor

        #region Account

        public async Task<bool> IsExistUserNameAsync(string? userName, Guid? id)
        {
            var result = false;
            if (id == Guid.Empty)
            {
                result = await _user.AnyAsync(p => p.UserName == userName);
            }
            else
            {
                var user = await _user.AsNoTracking().Where(p => p.Id == id).SingleOrDefaultAsync();
                if (user != null && user.UserName != userName)
                {
                    result = await _user.AnyAsync(p => p.UserName == userName);
                }
            }

            return result;
        }

        public async Task<bool> IsExistIdAsync(Guid id)
        {
            var result = await _user.AsNoTracking().AnyAsync(u => u.Id == id);
            return result;
        }

        public async Task<bool> IsExistPasswordAsync(string userName, string password)
        {
            var user = await _user.SingleOrDefaultAsync(p => p.UserName == userName);
            if (user != null)
                return await CheckPasswordAsync(user, password);
            return false;
        }

        public async Task<bool> IsUserNameActivatedAsync(string userName)
        {
            var user = await FindByNameAsync(userName);
            var result = await IsPhoneNumberConfirmedAsync(user!);
            return result;
        }

        public async Task<bool> IsExistUserHaveRole(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return false;
            var user = await _user.SingleOrDefaultAsync(p => p.UserName == userName);
            var userIdList = await _userRoles
                .Where(p => p.UserId == user.Id)
                .Select(p => p.RoleId)
                .ToListAsync();
            return userIdList.Any();
        }

        public async Task<string> UserRegisterAsync(RegisterViewModel viewModel)
        {
            var user = _mapper.Map<User>(viewModel);
            user.UserName = viewModel.PhoneNumber;
            user.LockoutEnabled = true;
            await CreateAsync(user, viewModel.Password);
            return user.PhoneNumber;
        }

        public async Task<bool> UserIsLockedOutAsync(string userName)
        {
            var user = await FindByNameAsync(userName);
            var result = await IsLockedOutAsync(user!);
            return result;
        }

        public async Task<bool> ActivationPhoneNumberAsync(string userName)
        {
            var user = await FindByNameAsync(userName);
            user!.PhoneNumberConfirmed = true;
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task ClearAccessFailedCountAsync(string userName)
        {
            var user = await FindByNameAsync(userName);
            if (user != null)
            {
                await ResetAccessFailedCountAsync(user);
            }
        }

        public async Task ChangeSecurityStampAsync(string userName)
        {
            var user = await FindByNameAsync(userName);
            if (user != null)
            {
                await UpdateSecurityStampAsync(user);
            }
        }

        public async Task<bool> ModifyPhoneNumber(string oldPhoneNumber, string newPhoneNumber)
        {
            if (string.IsNullOrWhiteSpace(oldPhoneNumber) || string.IsNullOrWhiteSpace(newPhoneNumber))
                return false;
            var user = await FindByNameAsync(oldPhoneNumber);
            user!.PhoneNumber = newPhoneNumber;
            user!.UserName = newPhoneNumber;
            await UpdateNormalizedUserNameAsync(user);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<string> CreatePasswordResetTokenAsync(string phoneNumber)
        {
            var user = await FindByNameAsync(phoneNumber);
            var token = await GeneratePasswordResetTokenAsync(user);
            return token;
        }

        public async Task<bool> ModifyPasswordAsync(string phoneNumber, string token, string password)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber) && string.IsNullOrWhiteSpace(password))
                return false;
            var user = await FindByNameAsync(phoneNumber);
            var result = await ResetPasswordAsync(user, token, password);
            return (result.Succeeded);
        }

        #endregion Account

        #region User

        private async Task<List<Guid>> ListUserRolesAsync(Guid userId)
        {
            var rolesIdList = await _userRoles.AsNoTracking()
                .Where(r => r.UserId == userId)
                .Select(u => u.RoleId)
                .ToListAsync();
            return rolesIdList;
        }

        public async Task<ListViewModel> GetAllUserAsync(ListViewModel viewModel, bool isDelete)
        {
            var list = _user.Where(u => u.IsDelete == isDelete);
            list = list.Where(p => p.IsConstantData == false);
            if (!string.IsNullOrWhiteSpace(viewModel.FilterNameAndFamily))
            {
                list = list.Where(u =>
                    u.Name.Contains(viewModel.FilterNameAndFamily) ||
                    u.Family.Contains(viewModel.FilterNameAndFamily));
            }

            if (!string.IsNullOrWhiteSpace(viewModel.FilterPhoneNumber))
            {
                list = list.Where(u => u.PhoneNumber.Contains(viewModel.FilterPhoneNumber));
            }

            if (!string.IsNullOrWhiteSpace(viewModel.UserName))
            {
                list = list.Where(u => u.UserName.Contains(viewModel.UserName));
            }

            list = list.OrderBy($"{viewModel.SelectedSortByItem} {viewModel.SelectedOrderByItem}");

            var take = viewModel.Take;
            var skip = (viewModel.PageNumber - 1) * take;
            var pageCount = (list.Count()) / take;
            if ((list.Count()) % take != 0)
            {
                pageCount = ((list.Count()) / take) + 1;
            }

            var userList = list.Skip(skip).Take(take).ToList();
            var userListViewModel = _mapper.Map<List<UserViewModel>>(userList);

            foreach (var item in userListViewModel)
            {
                var userId = item.Id;
                item.ListUserRoles = await ListUserRolesAsync(userId);
                item.ListAllRoles = await _roleManagerService.GetAllRoleAsSelectList();
            }

            var resultList = new ListViewModel()
            {
                UserListViewModel = userListViewModel,
                PageCount = pageCount,
                PageNumber = viewModel.PageNumber,
                Take = viewModel.Take,
                IsDelete = isDelete,
            };
            return resultList;
        }

        public async Task<UserViewModel> AddUserAsync(AddUserViewModel viewModel)
        {
            var user = _mapper.Map<User>(viewModel);
            user.UserName = viewModel.UserName;
            user.LockoutEnabled = true;
            user.PhoneNumberConfirmed = true;
            var result = await CreateAsync(user, viewModel.Password);
            if (result.Succeeded && viewModel.ListUserRoles != null)
            {
                foreach (var item in viewModel.ListUserRoles!)
                {
                    _userRoles.Add(new UserRole()
                    {
                        RoleId = item,
                        UserId = user.Id
                    });
                }
                await _uow.SaveChangesAsync();
            }

            var model = _mapper.Map<UserViewModel>(await _user.SingleOrDefaultAsync(p => p.Id == user.Id));
            return model;
        }

        public async Task<EditUserViewModel> GetUserForEditAsync(string userName)
        {
            var user = await _user.SingleOrDefaultAsync(p => p.UserName == userName);
            if (user is { IsConstantData: true })
                return new EditUserViewModel()
                {
                    IsConstantData = true
                };
            var viewModel = _mapper.Map<EditUserViewModel>(user);
            viewModel.ListUserRoles = await _userRoles
                .Where(i => i.UserId == user.Id)
                .Select(i => i.RoleId)
                .ToListAsync();
            viewModel.ListAllRoles = await _roleManagerService.GetAllRoleAsSelectList();
            return viewModel;
        }

        public async Task<UserViewModel> EditUserAsync(EditUserViewModel viewModel)
        {
            var user = await _user.SingleOrDefaultAsync(p => p.Id == viewModel.Id);
            if (user.IsConstantData)
                return new UserViewModel();
            user = _mapper.Map(viewModel, user);
            if (viewModel.Password != null)
            {
                user.PasswordHash = PasswordHasher.HashPassword(user, viewModel.Password);
            }

            if (!user.IsConstantData)
            {
                var listUserRole = await _userRoles.Where(i => i.UserId == user.Id).ToListAsync();
                _userRoles.RemoveRange(listUserRole);
                if (viewModel.ListUserRoles != null)
                {
                    foreach (var item in viewModel.ListUserRoles)
                    {
                        _userRoles.Add(new UserRole()
                        {
                            UserId = user.Id,
                            RoleId = item
                        });
                    }
                }
            }

            user.UserName = viewModel.UserName;
            await UpdateAsync(user);
            await _uow.SaveChangesAsync();
            var model = _mapper.Map<UserViewModel>(user);
            model.ListAllRoles = await _roleManagerService.GetAllRoleAsSelectList();
            model.ListUserRoles = await _userRoles
                .Where(p => p.UserId == user.Id)
                .Select(p => p.RoleId)
                .ToListAsync();
            return model;
        }

        public async Task<IdentityResult> ChangeUserPasswordAsync(ChangePasswordViewModel viewModel)
        {
            var user = await _user.SingleOrDefaultAsync(p => p.UserName == viewModel.UserName);
            if (user is { IsConstantData: true })
                return IdentityResult.Failed();
            user.PasswordHash = PasswordHasher.HashPassword(user, viewModel.NewPassword);
            await UpdateAsync(user);
            await _uow.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteUserAsync(Guid id)
        {
            var user = await FindByIdAsync(id.ToString());
            if (user == null || user.IsConstantData)
                return IdentityResult.Failed();
            if (user.IsConstantData)
                return IdentityResult.Failed();
            user.IsDelete = true;
            var result = await UpdateAsync(user);
            return result;
        }

        public async Task<IdentityResult> ReactivateUserAsync(Guid id)
        {
            var user = await FindByIdAsync(id.ToString());
            if (user == null)
                return IdentityResult.Failed();
            user.IsDelete = false;
            var result = await UpdateAsync(user);
            return result;
        }

        #endregion User
    }
}