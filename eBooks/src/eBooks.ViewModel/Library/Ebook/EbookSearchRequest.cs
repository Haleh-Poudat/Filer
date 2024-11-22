using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBooks.ViewModel.Common;
using eBooks.ViewModel.Role;
using eBooks.ViewModel.SelectViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eBooks.ViewModel.Library.Ebook
{

    public class EbookSearchRequest
    {
        public EbookSearchRequest()
        {
            PageNumber = 1;
            TakeListItem = SelectListViewModel.TakeListItem();
            Take = 5;
            OrderByTypeList = SelectListViewModel.OrderByTypeList();
            RoleSortListItems = SelectListViewModel.RoleSortListItems();
            SelectedSortByItem = "CreateDateTime";
            SelectedOrderByItem = "DESC";
        }
        public List<EbookViewModel> EbookViewModels { get; set; }
        public string? Titel { get; set; }
        public int PageNumber { get; set; }     
        public List<SelectListItem> TakeListItem { get; set; }
        public int Take { get; set; }   
        public int PageCount { get; set; }
        public List<SelectListItem> OrderByTypeList { get; set; }
        public string SelectedOrderByItem { get; set; }
        public List<SelectListItem> RoleSortListItems { get; set; }
        public string SelectedSortByItem { get; set; }

    }
}