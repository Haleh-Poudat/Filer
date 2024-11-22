using System;
using Microsoft.AspNetCore.Identity;
using eBooks.Utility;

namespace eBooks.Domain.Entities.Identity
{
    public class RoleClaim : IdentityRoleClaim<Guid>
    {
        public RoleClaim()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
        }
        public  Guid Id { get; set; }
        public virtual Role Roles { get; set; }
    }
}
