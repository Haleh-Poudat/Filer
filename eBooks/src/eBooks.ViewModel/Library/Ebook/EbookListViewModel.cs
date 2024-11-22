using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBooks.ViewModel.Common;
using eBooks.ViewModel.Library.Category;
using eBooks.ViewModel.SelectViewModel;

namespace eBooks.ViewModel.Library.Ebook
{
    public class EbookListViewModel
    {
        public EbookListViewModel()
        {
            PageNumber = 1;
            TakeListItem = SelectListViewModel.TakeListItem();
            Take = 50;
            OrderByTypeList = SelectListViewModel.OrderByBType2List();
            SelectedOrderByItem = "ASC";
            SelectedSortByItem = "Title";
        }

        public List<EbookViewModel> Ebooks { set; get; }

        public string? Title { get; set; }
        public Guid LibraryCategoryId { get; set; }
        public int PageNumber { get; set; }
        public List<SelectListItem> TakeListItem { get; set; }
        public int Take { get; set; }
        public int PageCount { get; set; }
        public List<SelectListItem> OrderByTypeList { get; set; }
        public string SelectedOrderByItem { get; set; }
        public string SelectedSortByItem { get; set; }
        public List<CategoryJsTreeViewModel> Libraries { set; get; }
    }
}