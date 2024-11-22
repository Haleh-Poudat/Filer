using eBooks.ViewModel.Library.Category;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBooks.Utility.Constants;
using Microsoft.AspNetCore.Mvc;
using eBooks.ViewModel.SiteSetting;
using Microsoft.Extensions.Options;

namespace eBooks.ViewModel.Library.Ebook
{
    public class AddEbookViewModel
    {
        public Guid LibraryCategoryId { set; get; }

        public LibraryCategoryViewModel? LibraryCategory { set; get; }

        [Required(ErrorMessage = "Please enter the {0}.")]
        [StringLength(200, ErrorMessage = "The {0} can be a maximum of 200 characters.")]
        public string Title { set; get; }

        [Required(ErrorMessage = "Please select a file to upload.")]
        public IFormFile File { set; get; }
    }
}