using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using eBooks.Datalayer.Context;
using eBooks.DataLayer.Context;
using eBooks.Domain.Entities.Identity;
using eBooks.ServiceLayer.Contracts.Identity;

namespace eBooks.ServiceLayer.Services.Identity
{
    public class UserStoreService : UserStore<User, Role, BestDbContext, Guid, UserClaim, UserRole, UserLogin,
        UserToken, RoleClaim>, IUserStoreService
    {
        public UserStoreService(IUnitOfWork context, IdentityErrorDescriber describer = null) : base(
            (BestDbContext)context, describer)
        {

        }
    }
}
  