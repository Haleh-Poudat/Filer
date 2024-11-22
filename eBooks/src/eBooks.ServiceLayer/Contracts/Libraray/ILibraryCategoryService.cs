using eBooks.ViewModel.Common;
using eBooks.ViewModel.Library.Category;
using Microsoft.AspNetCore.Identity;

namespace eBooks.ServiceLayer.Contracts.Libraray
{
    public interface ILibraryCategoryService
    {
        #region Custom Part

        public Task<bool> IsExistsLibraryCategoryTitleAsync(string title, Guid id);

        public Task<List<CategoryJsTreeViewModel>> GetAllLibraryAsync(Guid? parentId);

        public Task<AddLibraryCategoryViewModel> GetForCreateAsync(Guid? parentId);

        public Task<LibraryCategoryViewModel> CreateAsync(AddLibraryCategoryViewModel viewModel);

        public Task<EditLibraryCategoryViewModel> GetForEditAsync(Guid id);

        public Task<LibraryCategoryViewModel> EditAsync(EditLibraryCategoryViewModel viewModel);

        public Task<DeleteMsgViewModel> DeleteAsync(Guid id);

        Task<List<LibraryTreeViewModel>> GetTreeViewModelAsync(LibraryTreeViewModel viewModel);

        public Task<Guid> GetFirstCategoryIdAsync();

        #endregion Custom Part
    }
}