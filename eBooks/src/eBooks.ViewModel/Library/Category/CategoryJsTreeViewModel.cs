using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBooks.ViewModel.Library.Category
{
    public class CategoryJsTreeViewModel
    {
        public Guid id { get; set; }
        

        public string text { get; set; }

        public List<CategoryJsTreeViewModel> children { get; set; }
    }

    public class SubCategoryModel
    {
        public string text { get; set; }
    }
}