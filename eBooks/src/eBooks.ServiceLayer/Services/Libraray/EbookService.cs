using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eBooks.Datalayer.Context;
using eBooks.Domain.Entities.Library;
using eBooks.ServiceLayer.Contracts.Identity;
using eBooks.ServiceLayer.Contracts.Libraray;
using eBooks.ServiceLayer.Services.Tools;
using eBooks.Utility.Extentions;
using eBooks.ViewModel.Common;
using eBooks.ViewModel.Library.Ebook;
using eBooks.ViewModel.Library.Category;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using eBooks.Domain.Entities.Identity;
using eBooks.ServiceLayer.Services.Identity;
using eBooks.ViewModel.User;

namespace eBooks.ServiceLayer.Services.Libraray
{
    public class EbookService : FileOperationService, IEbookService
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<LibraryCategory> _libraryCategories;
        private readonly IMapper _mapper;
        private readonly DbSet<Ebook> _eBooks;

        public EbookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _eBooks = _unitOfWork.Set<Ebook>();
            _libraryCategories = _unitOfWork.Set<LibraryCategory>();
        }

        #endregion Fields

        #region GetForCreateAsync

        public async Task<AddEbookViewModel> GetForCreateAsync(Guid categoryId)
        {
            var viewModel = new AddEbookViewModel
            {
                LibraryCategory = _mapper.Map<LibraryCategoryViewModel>(
                    await _libraryCategories.AsNoTracking().Include(p => p.CreatedBy)
                    .FirstOrDefaultAsync(p => p.Id == categoryId)),
                LibraryCategoryId = categoryId
            };
            return viewModel;
        }

        #endregion GetForCreateAsync

        #region CreateAsync

        public async Task<EbookViewModel> CreateAsync(AddEbookViewModel viewModel)
        {
            var saveFile = new BookFileViewModel();
            if (viewModel.File != null)
                saveFile = await SaveFileAsync(viewModel.File);
            var entity = _mapper.Map<Ebook>(viewModel);
            entity.Name = saveFile.Name;
            entity.Path = saveFile.Path;
            _eBooks.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            var categoryId = entity.LibraryCategoryId;
            var entityList = await _eBooks.Where(p => p.LibraryCategoryId == categoryId).ToListAsync();
            var library = _mapper.Map<EbookViewModel>(await _eBooks.SingleOrDefaultAsync(p => p.Id == entity.Id));
            return library;
        }

        #endregion CreateAsync

        #region GetEbookViewModelAsync

        public async Task<EbookViewModel> GetEbookViewModelAsync(Guid id)
        {
            return _mapper.Map<EbookViewModel>(await _eBooks.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id));
        }

        #endregion GetEbookViewModelAsync

        #region DeleteAsync

        public async Task<DeleteMsgViewModel> DeleteAsync(Guid id)
        {
            var eBook = await _eBooks.SingleOrDefaultAsync(p => p.Id == id);

            if (eBook != null)
            {
                var remove = DeleteFile(eBook.Name);
            }

            _eBooks.Remove(eBook);
            await _unitOfWork.SaveChangesAsync();
            return new DeleteMsgViewModel { DeleteStatus = DeleteStatus.Done };
        }

        #endregion DeleteAsync

        #region GetPageListAsync

        public async Task<EbookListViewModel> GetPageListAsync(EbookListViewModel viewModel)
        {
            var list = _eBooks.AsNoTracking();
            if (viewModel.LibraryCategoryId != Guid.Empty)
            {
                list = list.Where(p => p.LibraryCategoryId == viewModel.LibraryCategoryId);
            }

            if (!string.IsNullOrWhiteSpace(viewModel.Title))
            {
                list = list.Where(p => p.Title.Contains(viewModel.Title));
            }

            list = list.OrderBy($"{viewModel.SelectedSortByItem} {viewModel.SelectedOrderByItem}");

            var take = viewModel.Take;
            var skip = (viewModel.PageNumber - 1) * take;
            var pageCount = (list.Count()) / take;
            if ((list.Count()) % take != 0)
            {
                pageCount = ((list.Count()) / take) + 1;
            }

            var eBooks = list.Skip(skip).Take(take).ToList();
            var eBooksViewModel = _mapper.Map<List<EbookViewModel>>(eBooks);
            var ebookListViewModel = new EbookListViewModel()
            {
                Ebooks = eBooksViewModel,
                PageCount = pageCount,
                PageNumber = viewModel.PageNumber,
                Title = viewModel.Title
            };

            return ebookListViewModel;
        }

        #endregion GetPageListAsync

        #region GetForEditAsync

        public async Task<EditEbookViewModel> GetForEditAsync(Guid id)
        {
            return _mapper.Map<EditEbookViewModel>(await _eBooks.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id));
        }

        #endregion GetForEditAsync

        #region EditAsync

        public async Task<EbookViewModel> EditAsync(EditEbookViewModel viewModel, Guid currentUserId)
        {
            var entity = _eBooks.Find(viewModel.Id);
            var deleteBookFileViewModel = new EditBookFileViewModel();
            var result = new BookFileViewModel();
            if (viewModel.File != null && viewModel.File.Length > 0)
            {
                deleteBookFileViewModel.Name = entity.Name;
                deleteBookFileViewModel.OldPath = entity.Path;
                deleteBookFileViewModel.File = viewModel.File;
                result = await EditFile(deleteBookFileViewModel);
                entity.Name = result.Name;
                entity.Path = result.Path;
            }
            entity.Title = viewModel.Title;
            _eBooks.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return await GetEbookViewModelAsync(entity.Id);
        }

        #endregion EditAsync

        public async Task<string> GetFileNameAsync(Guid id)
        {
            var fileName = await _eBooks.Where(p => p.Id == id).Select(p => p.Name).SingleOrDefaultAsync();
            return fileName ?? null;
        }
    }
}