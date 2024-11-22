using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using eBooks.Utility.Constants;
using eBooks.ViewModel.Common;

namespace eBooks.ViewModel.Accounts
{
    public class RegisterViewModel : CaptchaModel
    {
        [Display(Name = "National Code")]
        [Required(ErrorMessage = AttributesErrorMessages.EnterName)]
        [RegularExpression("^[0-9]*$", ErrorMessage = AttributesErrorMessages.PhoneNumberStructure)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = AttributesErrorMessages.PhoneNumberStructure)]
        public string UserName { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = AttributesErrorMessages.EnterName)]
        [RegularExpression("^[0-9]*$", ErrorMessage = AttributesErrorMessages.PhoneNumberStructure)]
        [StringLength(11, MinimumLength = 11, ErrorMessage = AttributesErrorMessages.PhoneNumberStructure)]
        public string PhoneNumber { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = AttributesErrorMessages.EnterName)]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF, \s]*$", ErrorMessage = AttributesErrorMessages.NameStructure)]
        public string Name { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = AttributesErrorMessages.EnterName)]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF, \s]*$", ErrorMessage = AttributesErrorMessages.NameStructure)]
        public string Family { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = AttributesErrorMessages.EnterName)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$", ErrorMessage = AttributesErrorMessages.PasswordStructure)]
        [StringLength(6, MinimumLength = 6, ErrorMessage = AttributesErrorMessages.PasswordStructure)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = AttributesErrorMessages.EnterName)]
        [Compare(nameof(Password), ErrorMessage = AttributesErrorMessages.ComparePassword)]
        public string ConfirmPassword { get; set; }

        public bool LockoutEnabled { get; set; } = true;
    }
}