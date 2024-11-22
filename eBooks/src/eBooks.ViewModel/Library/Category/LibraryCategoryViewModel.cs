using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBooks.ViewModel.Common;

namespace eBooks.ViewModel.Library.Category
{
    public class LibraryCategoryViewModel
    {
        public Guid Id { set; get; }

        public string Title { set; get; }

        public Guid? ParentId { set; get; }

        public string? Description { set; get; }
    }
}