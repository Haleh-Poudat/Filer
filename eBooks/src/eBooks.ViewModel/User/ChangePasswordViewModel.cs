using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using eBooks.Utility.Constants;

namespace eBooks.ViewModel.User
{
    public class ChangePasswordViewModel
    {
        [ReadOnly(true)]
        public string UserName { get; set; }

        [ReadOnly(true)]
        public string Name { get; set; }

        [ReadOnly(true)]
        public string Family { get; set; }

        [Display(Name = "Current password")]
        [Required(ErrorMessage = AttributesErrorMessages.EnterName)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$", ErrorMessage = AttributesErrorMessages.PasswordStructure)]
        public string CurrentPassword { get; set; }

        [Display(Name = "New password")]
        [Required(ErrorMessage = AttributesErrorMessages.EnterName)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$", ErrorMessage = AttributesErrorMessages.PasswordStructure)]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm new password")]
        [Required(ErrorMessage = AttributesErrorMessages.EnterName)]
        [Compare(nameof(NewPassword), ErrorMessage = AttributesErrorMessages.ComparePassword)]
        public string ConfirmNewPassword { get; set; }
    }
}