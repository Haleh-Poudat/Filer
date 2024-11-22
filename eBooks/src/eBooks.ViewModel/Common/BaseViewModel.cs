using eBooks.Domain.Entities.Common;
using eBooks.ViewModel.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBooks.ViewModel.User;

namespace eBooks.ViewModel.Common
{
    public abstract class BaseViewModel
    {
        [DisplayName("Created By")]
        public string CreatorUserName { get; set; }

        public UserViewModel CreatedBy { get; set; }

        [DisplayName("Modified By")]
        public string LastModifierUserName { get; set; }

        [DisplayName("Created Date")]
        public DateTime CreatedOn { get; set; }

        public string PersianCreatedOn { get; set; }

        [DisplayName("Modified Date")]
        public DateTime ModifiedOn { get; set; }

        [DisplayName("Action")]
        public AuditAction Action { get; set; }

        public string CreatorIp { get; set; }

        public string ModifierIp { get; set; }

        public bool ModifyLocked { get; set; }

        public string ModifierAgent { get; set; }

        public string CreatorAgent { get; set; }

        /// <summary>
        /// gets or sets count of Modification Default is 1
        /// </summary>
        public int Version { get; set; }
    }
}