using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBooks.Utility.Constants;
using eBooks.ViewModel.Common;

namespace eBooks.ViewModel.Accounts
{
    public class LogInViewModel
    {
        [Display(Name = "National Code")]
        [Required(ErrorMessage = AttributesErrorMessages.EnterName)]
        [RegularExpression("^[0-9]*$", ErrorMessage = AttributesErrorMessages.UserNameStructure)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = AttributesErrorMessages.UserNameStructure)]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = AttributesErrorMessages.EnterName)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$", ErrorMessage = AttributesErrorMessages.PasswordStructure)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}