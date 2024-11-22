using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBooks.ViewModel.User
{
    public class UserByRoleIdViewModel
    {
        public Guid RoleId { get; set; }
        public List<UserByRoleId> ListUserByRoleId { get; set; }
    }
    public class UserByRoleId
    {
        public Guid Id { get; set; }    
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public DateTime CreateDateTime { get; set; }
    }

}
