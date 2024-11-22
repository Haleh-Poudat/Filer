using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBooks.ViewModel.SiteSetting
{
    public class IdentityOptionsSettings
    {
        public TimeSpan AddCookieExpireTimeSpan { get; set; }

        public bool PasswordRequireDigit { get; set; }
        public bool PasswordRequireLowercase { get; set; }
        public bool PasswordRequireUppercase { get; set; }
        public bool PasswordRequireNonAlphanumeric { get; set; }
        public int PasswordRequiredLength { get; set; }

        public bool LockoutAllowedForNewUsers { get; set; }
        public int LockoutMaxFailedAccessAttempts { get; set; }
        public TimeSpan LockoutDefaultLockoutTimeSpan { get; set; }

        public bool SignInRequireConfirmedAccount { get; set; }
        public bool SignInRequireConfirmedPhoneNumber { get; set; }
        public bool SignInRequireConfirmedEmail { get; set; }
        public bool UserRequireUniqueEmail { get; set; }
    }
}