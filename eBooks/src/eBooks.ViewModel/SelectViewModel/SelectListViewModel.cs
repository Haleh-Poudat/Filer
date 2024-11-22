using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace eBooks.ViewModel.SelectViewModel
{
    public static class SelectListViewModel
    {
        public static List<SelectListItem> OrderByTypeList()
        {
            var list = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "Descending",
                    Value = "DESC"
                },
                new SelectListItem()
                {
                    Text = "Ascending",
                    Value = "ASC"
                }
            };
            return list;
        }

        public static List<SelectListItem> OrderByBType2List()
        {
            var list = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "Ascending",
                    Value = "ASC"
                },
                new SelectListItem()
                {
                    Text = "Descending",
                    Value = "DESC"
                }
            };
            return list;
        }

        public static List<SelectListItem> TakeListItem()
        {
            var list = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "Display 50 records",
                    Value = "50"
                },
                new SelectListItem()
                {
                    Text = "Display 100 records",
                    Value = "100"
                },
                new SelectListItem()
                {
                    Text = "Display 150 records",
                    Value = "150"
                },
                new SelectListItem()
                {
                    Text = "Display 200 records",
                    Value = "200"
                }
            };
            return list;
        }

        public static List<SelectListItem> UserSortListItems()
        {
            var list = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "Registration Date",
                    Value = "CreateDateTime"
                },
                new SelectListItem()
                {
                    Text = "Phone Number",
                    Value = "PhoneNumber"
                },
                new SelectListItem()
                {
                    Text = "First Name",
                    Value = "Name"
                },
                new SelectListItem()
                {
                    Text = "Last Name",
                    Value = "Family"
                },
                new SelectListItem()
                {
                    Text = "Email",
                    Value = "Email"
                }
            };
            return list;
        }

        public static List<SelectListItem> RoleSortListItems()
        {
            var list = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "Created Date",
                    Value = "CreateDateTime"
                },
                new SelectListItem()
                {
                    Text = "Title",
                    Value = "Name"
                }
            };
            return list;
        }
    }
}