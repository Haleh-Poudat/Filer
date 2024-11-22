using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using eBooks.Utility;

namespace eBooks.Domain.Entities.Identity
{
    public class Role : IdentityRole<Guid>
    {
        public Role()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
            CreateDateTime = DateTime.Now;
            IsConstantData = false;
        }

        public override sealed Guid Id { get; set; }
        public bool IsConstantData { get; set; }
        public string Description { get; set; }
        public DateTime CreateDateTime { get; set; }
        public virtual ICollection<RoleClaim> RoleClaims { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}