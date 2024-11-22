using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using eBooks.Datalayer.Context;
using eBooks.DataLayer.Context;
using eBooks.Domain.Entities.Identity;
using eBooks.ServiceLayer.Contracts.Identity;

namespace eBooks.ServiceLayer.Services.Identity
{
    public class RoleStoreService:RoleStore<Role, BestDbContext, Guid,UserRole,RoleClaim>, IRoleStoreService
    {
        public RoleStoreService(IUnitOfWork context,
            IdentityErrorDescriber describer = null):base((BestDbContext)context,describer)
        {
            
        }
    }
}
