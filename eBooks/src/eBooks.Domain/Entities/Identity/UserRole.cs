using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using eBooks.Utility;

namespace eBooks.Domain.Entities.Identity
{
    public class UserRole : IdentityUserRole<Guid>
    {


        public virtual User Users { get; set; }
        public virtual Role Roles { get; set; }
    }   
}
