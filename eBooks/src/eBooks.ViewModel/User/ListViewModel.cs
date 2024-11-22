using Microsoft.AspNetCore.Mvc.Rendering;
using eBooks.ViewModel.SelectViewModel;

namespace eBooks.ViewModel.User
{
    public class ListViewModel
    {
        public ListViewModel()
        {
            PageNumber = 1;
            TakeListItem = SelectListViewModel.TakeListItem();
            Take = 50;
            SortByListItems = SelectListViewModel.UserSortListItems();
            SelectedSortByItem = "CreateDateTime";
            OrderByTypeList = SelectListViewModel.OrderByTypeList();
            SelectedOrderByItem = "DESC";
        }

        public List<UserViewModel> UserListViewModel { get; set; }
        public bool? IsDelete { get; set; }
        public string? FilterNameAndFamily { get; set; }
        public string? UserName { get; set; }
        public string? FilterPhoneNumber { get; set; }
        public int PageNumber { get; set; }
        public List<SelectListItem> TakeListItem { get; set; }
        public int Take { get; set; }
        public int PageCount { get; set; }
        public List<SelectListItem> SortByListItems { get; set; }
        public string SelectedSortByItem { get; set; }
        public List<SelectListItem> OrderByTypeList { get; set; }
        public string SelectedOrderByItem { get; set; }
    }
}