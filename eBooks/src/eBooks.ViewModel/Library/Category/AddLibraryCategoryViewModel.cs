using eBooks.Utility.Constants;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBooks.ViewModel.Library.Category
{
    public class AddLibraryCategoryViewModel
    {
        public string? ParentTitle { set; get; }
        public Guid? ParentId { set; get; }

        [Required(ErrorMessage = "Please insert the {0}")]
        [StringLength(200, ErrorMessage = "The {0} can be a maximum of 200 characters.")]
        [Remote("CheckLibraryCategoryTitle", "Category", ErrorMessage = AttributesErrorMessages.RemoteRoleNameMessage)]
        public string Title { set; get; }

        [StringLength(1024, ErrorMessage = "The {0} can be a maximum of 1024 characters.")]
        public string? Description { set; get; }
    }
}