using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBooks.ViewModel.Library.Category
{
    public class LibraryTreeViewModel
    {
        public Guid id { set; get; }

        public string title { set; get; }

        public int ownCount { set; get; }

        public int subCount { set; get; }

        public bool lazy { set; get; }

        public bool folder { set; get; }

        public string path { set; get; }

        public LibraryTreeType type { set; get; }
    }

    public enum LibraryTreeType
    {
        Category,
        Ebook
    }
}