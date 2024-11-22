using AutoMapper;
using eBooks.Domain.Entities.Library;
using eBooks.ViewModel.Library.Category;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBooks.AutoMapperProfiles.Library
{
    public class LibraryCategoryProfile : Profile
    {
        public LibraryCategoryProfile()
        {
            CreateMap<LibraryCategory, LibraryCategoryViewModel>();
            CreateMap<LibraryCategory, EditLibraryCategoryViewModel>();
            CreateMap<EditLibraryCategoryViewModel, LibraryCategory>().ForMember(p => p.Parent, p => p.Ignore());
            CreateMap<AddLibraryCategoryViewModel, LibraryCategory>();
            CreateMap<LibraryCategory, SelectListItem>()
                .ForMember(p => p.Text, p => p.MapFrom(q => q.Title))
                .ForMember(p => p.Value, p => p.MapFrom(q => q.Id.ToString()));

            CreateMap<LibraryCategory, CategoryJsTreeViewModel>()
                .ForMember(p => p.text, p => p.MapFrom(q => q.Title))
                .ReverseMap();
        }
    }
}