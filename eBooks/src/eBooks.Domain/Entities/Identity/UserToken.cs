﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using eBooks.Utility;

namespace eBooks.Domain.Entities.Identity
{
    public  class UserToken : IdentityUserToken<Guid>
    {
        public UserToken()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
        }
        public  Guid Id { get; set; }
        public virtual User Users { get; set; }
    }
}