using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBooks.Domain.Entities.Identity;
using eBooks.Utility.Constants;

namespace eBooks.ViewModel.Role
{
    public class RoleViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Role Title")]
        [Required(ErrorMessage = AttributesErrorMessages.EnterName)]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF, \s]*$", ErrorMessage = AttributesErrorMessages.NameStructure)]
        public string Name { get; set; }

        public int UserCountInRole { get; set; }
        public bool IsConstantData { get; set; }
        public List<Guid>? AllPermissionToRoleList { get; set; }
    }
}