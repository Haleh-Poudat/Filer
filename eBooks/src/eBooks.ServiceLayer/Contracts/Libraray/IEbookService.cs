using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBooks.ViewModel.Common;
using eBooks.ViewModel.Library.Ebook;

namespace eBooks.ServiceLayer.Contracts.Libraray
{
    public interface IEbookService
    {
        Task<AddEbookViewModel> GetForCreateAsync(Guid categoryId);

        Task<EbookViewModel> CreateAsync(AddEbookViewModel viewModel);

        Task<EbookViewModel> GetEbookViewModelAsync(Guid id);

        Task<string> GetFileNameAsync(Guid id);

        Task<DeleteMsgViewModel> DeleteAsync(Guid id);

        Task<EbookListViewModel> GetPageListAsync(EbookListViewModel viewModel);

        Task<EditEbookViewModel> GetForEditAsync(Guid id);

        Task<EbookViewModel> EditAsync(EditEbookViewModel viewModel, Guid currentUserId);
    }
}