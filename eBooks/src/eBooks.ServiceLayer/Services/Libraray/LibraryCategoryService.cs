using AutoMapper;
using eBooks.Datalayer.Context;
using eBooks.Domain.Entities.Library;
using eBooks.ServiceLayer.Contracts.Libraray;
using eBooks.ViewModel.Common;
using eBooks.ViewModel.Library.Category;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace eBooks.ServiceLayer.Services.Libraray
{
    public class LibraryCategoryService : ILibraryCategoryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly DbSet<LibraryCategory> _libraryCategories;
        private readonly DbSet<Ebook> _ebooks;

        public LibraryCategoryService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
            _libraryCategories = _uow.Set<LibraryCategory>();
            _ebooks = _uow.Set<Ebook>();
        }

        #region Custom Methods

        public async Task<bool> IsExistsLibraryCategoryTitleAsync(string title, Guid id)
        {
            var result = false;
            if (id == Guid.Empty)
            {
                result = await _libraryCategories.AnyAsync(p => p.Title == title);
            }
            else
            {
                var libraryCategory = await _libraryCategories.AsNoTracking().Where(p => p.Id == id).SingleOrDefaultAsync();
                if (libraryCategory != null && libraryCategory.Title != title)
                {
                    result = await _libraryCategories.AnyAsync(p => p.Title == title);
                }
            }
            return result;
        }

        #region GetCategoryViewModelAsync

        public async Task<LibraryCategoryViewModel> GetCategoryViewModelAsync(Guid id)
        {
            var libraryCategory = await _libraryCategories.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if (libraryCategory == null)
                return new LibraryCategoryViewModel();
            var libraryCategoryViewModel = _mapper.Map<LibraryCategoryViewModel>(libraryCategory);
            return libraryCategoryViewModel;
        }

        #endregion GetCategoryViewModelAsync

        #region GetParentsAsync

        public async Task<List<CategoryJsTreeViewModel>> GetAllLibraryAsync(Guid? parentId)
        {
            var roots = await _libraryCategories.AsNoTracking()
                   .Where(p => p.ParentId == null)
                   .ToListAsync();
            var model = _mapper.Map<List<CategoryJsTreeViewModel>>(roots);
            foreach (var root in model)
            {
                root.children = GetChildren(root.id);
            }

            return model;
        }

        private List<CategoryJsTreeViewModel> GetChildren(Guid parentId)
        {
            var children = _libraryCategories.AsNoTracking()
                .Where(p => p.ParentId == parentId)
                .ToList();
            if (!children.Any()) return new List<CategoryJsTreeViewModel>();
            return children.Select(p => new CategoryJsTreeViewModel { text = p.Title, id = p.Id, children = GetChildren(p.Id) }).ToList();
        }

        #endregion GetParentsAsync

        #region GetForCreateAsync

        public async Task<AddLibraryCategoryViewModel> GetForCreateAsync(Guid? parentId)
        {
            var viewModel = new AddLibraryCategoryViewModel();
            if (!parentId.HasValue) return viewModel;
            var category = await _libraryCategories.AsNoTracking().FirstOrDefaultAsync(p => p.Id == parentId.Value);
            if (category != null)
            {
                viewModel.ParentId = parentId;
                viewModel.ParentTitle = category.Title;
            }
            return viewModel;
        }

        #endregion GetForCreateAsync

        public async Task<LibraryCategoryViewModel> CreateAsync(AddLibraryCategoryViewModel viewModel)
        {
            var entity = new LibraryCategory()
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                ParentId = viewModel.ParentId
            };
            var result = await _libraryCategories.AddAsync(entity);
            await _uow.SaveChangesAsync();
            var test = await _libraryCategories.SingleOrDefaultAsync(p => p.Id == entity.Id);
            var model = _mapper.Map<LibraryCategoryViewModel>(test);
            return model;
        }

        public async Task<EditLibraryCategoryViewModel> GetForEditAsync(Guid id)
        {
            var libraryCategories = await _libraryCategories.FindAsync(id);
            var editViewModel = _mapper.Map<EditLibraryCategoryViewModel>(libraryCategories);
            return editViewModel;
        }

        public async Task<LibraryCategoryViewModel> EditAsync(EditLibraryCategoryViewModel viewModel)
        {
            var entity = await _libraryCategories.FindAsync(viewModel.Id);

            if (entity == null) return await GetCategoryViewModelAsync(Guid.Empty);
            var bb = _mapper.Map(viewModel, entity);
            _libraryCategories.Update(bb);
            await _uow.SaveChangesAsync();
            return await GetCategoryViewModelAsync(entity.Id);
        }

        public async Task<DeleteMsgViewModel> DeleteAsync(Guid id)
        {
            if (await _libraryCategories.AsNoTracking().AnyAsync(p => p.ParentId == id))
                return new DeleteMsgViewModel { DeleteStatus = DeleteStatus.Error, Message = "Deletion is not possible due to the existence of subcategories." };
            if (await _ebooks.AsNoTracking().AnyAsync(p => p.LibraryCategoryId == id))
                return new DeleteMsgViewModel { DeleteStatus = DeleteStatus.Error, Message = "Deletion is not possible due to the existence of files in this category." };

            var libraryCategory = await _libraryCategories.FindAsync(id);
            _libraryCategories.Remove(libraryCategory);
            await _uow.SaveChangesAsync();
            return new DeleteMsgViewModel { DeleteStatus = DeleteStatus.Done, DeleteType = DeleteType.FancyTree };
        }

        #endregion Custom Methods

        #region GetTreeViewModelAsync

        public async Task<List<LibraryTreeViewModel>> GetTreeViewModelAsync(LibraryTreeViewModel viewModel)
        {
            var categoryViewModels = await
                _libraryCategories.AsNoTracking()
                    .Where(p => p.ParentId == viewModel.id)
                    .ToListAsync();

            var viewModels = new List<LibraryTreeViewModel>();
            viewModels.AddRange(categoryViewModels.Select(item => new LibraryTreeViewModel
            {
                lazy = true,
                id = item.Id,
                title = item.Title,
                folder = true,
                type = LibraryTreeType.Category
            }));

            return viewModels;
        }

        #endregion GetTreeViewModelAsync

        public async Task<Guid> GetFirstCategoryIdAsync()
        {
            return await _libraryCategories.OrderBy(p => p.CreatedOn).Select(p => p.Id).FirstOrDefaultAsync();
        }
    }
}