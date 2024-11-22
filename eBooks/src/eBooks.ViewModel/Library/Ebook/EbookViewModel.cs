using eBooks.ViewModel.Common;
using eBooks.ViewModel.Library.Category;

namespace eBooks.ViewModel.Library.Ebook
{
    public class EbookViewModel : BaseViewModel
    {
        public Guid Id { set; get; }

        public string Title { set; get; }

        public string Description { set; get; }

        public string Path { set; get; }
        public string Name { set; get; }

        public Guid CategoryId { set; get; }

        public LibraryCategoryViewModel LibraryCategory { set; get; }
    }
}