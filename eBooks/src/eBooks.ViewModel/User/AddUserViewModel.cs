using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using eBooks.Utility.Constants;

namespace eBooks.ViewModel.User
{
    public class AddUserViewModel
    {
        public AddUserViewModel()
        {
        }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = AttributesErrorMessages.EnterName)]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF, \s]*$", ErrorMessage = AttributesErrorMessages.NameStructure)]
        public string? Name { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = AttributesErrorMessages.EnterName)]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF, \s]*$", ErrorMessage = AttributesErrorMessages.NameStructure)]
        public string? Family { get; set; }

        [Display(Name = "National ID")]
        [Required(ErrorMessage = AttributesErrorMessages.EnterName)]
        [RegularExpression("^[0-9]*$", ErrorMessage = AttributesErrorMessages.UserNameStructure)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = AttributesErrorMessages.UserNameStructure)]
        [Remote("IsNationalCodeValid", "User", "Admin", ErrorMessage = "The entered national ID is invalid.", HttpMethod = "POST")]
        public string UserName { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = AttributesErrorMessages.EnterName)]
        [RegularExpression("^[0-9]*$", ErrorMessage = AttributesErrorMessages.PhoneNumberStructure)]
        [StringLength(11, MinimumLength = 11, ErrorMessage = AttributesErrorMessages.PhoneNumberStructure)]
        public string PhoneNumber { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$", ErrorMessage = AttributesErrorMessages.AllowedEmail)]
        public string? Email { get; set; }

        [Required(ErrorMessage = AttributesErrorMessages.EnterName)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$", ErrorMessage = AttributesErrorMessages.PasswordStructure)]
        public string Password { get; set; }

        public List<SelectListItem>? ListAllRoles { get; set; }
        public List<Guid>? ListUserRoles { get; set; }
    }
}