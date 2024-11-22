using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBooks.ViewModel.Library.Ebook
{
    public class EditBookFileViewModel
    {
        public string Name { get; set; }
        public string OldPath { get; set; }
        public IFormFile File { get; set; }
    }
}