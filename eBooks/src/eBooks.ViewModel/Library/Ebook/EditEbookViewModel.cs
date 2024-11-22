using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace eBooks.ViewModel.Library.Ebook
{
    public class EditEbookViewModel
    {
        public Guid Id { set; get; }

        [Required(ErrorMessage = "Please enter the {0}.")]
        [StringLength(200, ErrorMessage = "The {0} can be a maximum of 200 characters.")]
        public string Title { set; get; }

        [Required(ErrorMessage = "Please select a {0} to upload.")]
        public IFormFile? File { set; get; }
    }
}