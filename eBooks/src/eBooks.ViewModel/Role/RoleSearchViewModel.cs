using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using eBooks.ViewModel.SelectViewModel;
using eBooks.ViewModel.User;

namespace eBooks.ViewModel.Role
{
    public class RoleSearchViewModel
    {
        public RoleSearchViewModel()
        {
            PageNumber = 1;
            TakeListItem = SelectListViewModel.TakeListItem();
            Take = 5;
            OrderByTypeList = SelectListViewModel.OrderByTypeList();
            RoleSortListItems = SelectListViewModel.RoleSortListItems();
            SelectedSortByItem = "CreateDateTime";
            SelectedOrderByItem = "DESC";
        }
        public List<RoleViewModel> RoleListViewModel { get; set; }
        public string? FilterRoleName { get; set; }
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
