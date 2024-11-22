using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBooks.Utility.Constants;
using eBooks.ViewModel.Common;
using Microsoft.AspNetCore.Mvc;

namespace eBooks.ViewModel.Library.Category
{
    public class EditLibraryCategoryViewModel
    {
        public Guid Id { set; get; }

        [Required(ErrorMessage = "Please enter the {0}.")]
        [Remote("CheckLibraryCategoryTitle", "Category", AdditionalFields = nameof(Id), ErrorMessage = "The Category title is duplicate. Please choose another title.")]
        public string Title { get; set; }

        public string? Description { set; get; }
    }
}