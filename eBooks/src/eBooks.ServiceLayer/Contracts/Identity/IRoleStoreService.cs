﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using eBooks.Domain.Entities.Identity;

namespace eBooks.ServiceLayer.Contracts.Identity
{
    public interface IRoleStoreService:IDisposable
    {
        Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken);
        Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken);
        Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken);
        Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken);
        Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken);
        Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken);
        Guid ConvertIdFromString(string id);
        string ConvertIdToString(Guid id);
        Task<Role> FindByIdAsync(string id, CancellationToken cancellationToken);
        Task<Role> FindByNameAsync(string normalizedName, CancellationToken cancellationToken);
        Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken);
        Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken);
        Task<IList<Claim>> GetClaimsAsync(Role role, CancellationToken cancellationToken);
        Task AddClaimAsync(Role role, Claim claim, CancellationToken cancellationToken);
        Task RemoveClaimAsync(Role role, Claim claim, CancellationToken cancellationToken);
        IdentityErrorDescriber ErrorDescriber { get; set; }
        bool AutoSaveChanges { get; set; }
        IQueryable<Role> Roles { get; }
    }
}