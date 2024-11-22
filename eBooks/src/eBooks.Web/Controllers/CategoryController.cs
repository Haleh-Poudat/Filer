using eBooks.ServiceLayer.Contracts.Libraray;
using eBooks.ViewModel.Library.Category;
using Microsoft.AspNetCore.Mvc;
using eBooks.ServiceLayer.Services.Identity;
using eBooks.ViewModel.Common;
using Microsoft.AspNetCore.Authorization;

namespace eBooks.Web.Controllers
{
    [Authorize]
    public class CategoryController : BaseController
    {
        #region Fields

        private readonly ILibraryCategoryService _libraryCategoryService;

        #endregion Fields

        #region Ctor

        public CategoryController(ILibraryCategoryService libraryCategoryService, IEbookService ebookService)
        {
            _libraryCategoryService = libraryCategoryService;
        }

        #endregion Ctor

        #region Methods

        #region List

        public async Task<ActionResult> AjaxTree(LibraryTreeViewModel viewModel)
        {
            var models = await _libraryCategoryService.GetTreeViewModelAsync(viewModel);
            return Json(models);
        }

        #endregion List

        [HttpGet]
        public async Task<bool> CheckLibraryCategoryTitleAsync(string? title, string? newTitle, Guid id)
        {
            if (title != null)
            {
                return !await _libraryCategoryService.IsExistsLibraryCategoryTitleAsync(title, id);
            }
            if (newTitle != null)
            {
                return !await _libraryCategoryService.IsExistsLibraryCategoryTitleAsync(newTitle, id);
            }
            return false;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAsync(Guid? id)
        {
            var viewModel = await _libraryCategoryService.GetForCreateAsync(id);
            return PartialView("_Create", viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAsync(AddLibraryCategoryViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Please enter the form information carefully.");
            if (await _libraryCategoryService.IsExistsLibraryCategoryTitleAsync(viewModel.Title, new Guid()))
                return BadRequest("The name is duplicate.");
            var result = await _libraryCategoryService.CreateAsync(viewModel);
            var libraries = await _libraryCategoryService.GetAllLibraryAsync(Guid.Empty);
            return Json(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditAsync(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("The information for the selected user group is not available.");

            var viewModel = await _libraryCategoryService.GetForEditAsync(id);
            if (viewModel == null)
                return BadRequest("The information for the selected user group was not found.");

            return PartialView("_Edit", viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditAsync(EditLibraryCategoryViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Please be careful when editing the user group name and use only Persian letters.");
            var category = await _libraryCategoryService.EditAsync(viewModel);
            return Json(category);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("The selected user group information is not available.");

            var result = await _libraryCategoryService.DeleteAsync(id);
            if (result.DeleteStatus == DeleteStatus.Error)
                return BadRequest(result.Message);
            return Json("success");
        }

        #endregion Methods
    }
}