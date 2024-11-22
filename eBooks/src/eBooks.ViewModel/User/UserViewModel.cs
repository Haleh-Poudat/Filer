using Microsoft.AspNetCore.Mvc.Rendering;
using eBooks.ViewModel.SelectViewModel;

namespace eBooks.ViewModel.User
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            ListUserRoles = new List<Guid>();
        }

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string Email { get; set; }
        public bool IsDelete { get; set; }
        public List<SelectListItem> ListAllRoles { get; set; }
        public List<Guid>? ListUserRoles { get; set; }
    }
}