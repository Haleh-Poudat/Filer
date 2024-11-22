using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using eBooks.Utility;

namespace eBooks.Domain.Entities.Identity
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
            IsConstantData = false;
            CreateDateTime = DateTime.Now;
            IsDelete = false;
        }

        public override sealed Guid Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public DateTime CreateDateTime { get; set; }
        public bool IsDelete { get; set; }
        public bool IsConstantData { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserClaim> UserClaims { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }
    }
}