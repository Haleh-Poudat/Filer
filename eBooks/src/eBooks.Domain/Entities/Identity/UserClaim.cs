using System;
using Microsoft.AspNetCore.Identity;
using eBooks.Utility;

namespace eBooks.Domain.Entities.Identity
{
    public class UserClaim : IdentityUserClaim<Guid>
    {
        public UserClaim()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
        }

        public Guid Id { get; set; }
        public virtual User Users { get; set; }
    }
}
