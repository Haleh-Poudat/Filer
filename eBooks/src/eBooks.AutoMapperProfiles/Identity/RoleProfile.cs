using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eBooks.Domain.Entities.Identity;
using eBooks.ViewModel.Role;
using eBooks.ViewModel.User;

namespace eBooks.AutoMapperProfiles.Identity
{
    public class RoleProfile:Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleViewModel>().ReverseMap();
            CreateMap<Role, EditRoleViewModel>().ReverseMap();
        }
    }
}
