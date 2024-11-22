using AutoMapper;
using eBooks.Domain.Entities.Library;
using eBooks.ViewModel.Library.Ebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBooks.AutoMapperProfiles.Library
{
    public class EbookProfile : Profile
    {
        public EbookProfile()
        {
            CreateMap<Ebook, EbookViewModel>();
            CreateMap<Ebook, EditEbookViewModel>();
            CreateMap<EditEbookViewModel, Ebook>();
            CreateMap<AddEbookViewModel, Ebook>()
                .ForMember(p => p.LibraryCategory, p => p.Ignore());
        }
    }
}