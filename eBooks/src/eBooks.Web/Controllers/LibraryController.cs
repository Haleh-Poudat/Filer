using eBooks.ServiceLayer.Contracts.Libraray;
using eBooks.ViewModel.Common;
using eBooks.ViewModel.Library.Category;
using eBooks.ViewModel.Library.Ebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System.Linq;
using eBooks.ViewModel.SiteSetting;

using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Options;

namespace eBooks.Web.Controllers
{
    [Authorize]
    public class LibraryController : BaseController
    {
        #region Fields

        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILibraryCategoryService _libraryCategoryService;
        private readonly IEbookService _ebookService;
        protected readonly ICompositeViewEngine _viewEngine;
        private readonly SiteSettings _siteSettings;
        private readonly FileUploadSettings _fileUploadSettings;

        #endregion Fields

        #region Ctor

        public LibraryController(ILibraryCategoryService libraryCategoryService, IEbookService ebookService, IWebHostEnvironment hostingEnvironment, ICompositeViewEngine viewEngine, IOptionsMonitor<SiteSettings> siteSettings)
        {
            _libraryCategoryService = libraryCategoryService;
            _ebookService = ebookService;
            _hostingEnvironment = hostingEnvironment;
            _viewEngine = viewEngine;
            _siteSettings = siteSettings.CurrentValue;
            _fileUploadSettings = _siteSettings.FileUploadSettings;
        }

        #endregion Ctor

        #region List, Ajax

        public async Task<ActionResult> Index()
        {
            var categoryId = await _libraryCategoryService.GetFirstCategoryIdAsync();
            var viewModel = new EbookListViewModel()
            {
                LibraryCategoryId = categoryId
            };
            var models = await _ebookService.GetPageListAsync(viewModel);
            models.Libraries = await _libraryCategoryService.GetAllLibraryAsync(Guid.Empty);
            return View(models);
        }

        public async Task<IActionResult> GetAllLibraryAsync()
        {
            var libraries = await _libraryCategoryService.GetAllLibraryAsync(Guid.Empty);
            return Json(libraries);
        }

        public virtual async Task<ActionResult> ListAjax(EbookListViewModel viewModel, Guid selectedId)
        {
            var pageNumber = viewModel.PageNumber;
            if (selectedId != Guid.Empty)
                viewModel.LibraryCategoryId = selectedId;
            var model = await _ebookService.GetPageListAsync(viewModel);
            var pageNumber2 = model.PageNumber;
            if (model.Ebooks == null || !model.Ebooks.Any()) return Content("no-more-info");
            return PartialView("_ListAjax", model);
        }

        public async Task<IActionResult> GetChildNodes(Guid selectedId)
        {
            var childCategories = await _libraryCategoryService.GetAllLibraryAsync(selectedId);
            var children = JsonConvert.SerializeObject(childCategories);
            return Content(children, "application/json");
        }

        #region Show File

        [HttpGet]
        public async Task<IActionResult> Download(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("The requested file was not found.");
            }

            var fileName = await _ebookService.GetFileNameAsync(id);
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", fileName);
            if (!System.IO.File.Exists(filePath))
                BadRequest("The requested file was not found.");
            var fileExtension = Path.GetExtension(filePath).ToLower();

            string contentType;
            switch (fileExtension)
            {
                case ".pdf":
                    contentType = "application/pdf";
                    break;

                case ".docx":
                    contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;

                case ".xlsx":
                    contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;

                case ".pptx":
                    contentType = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                    break;

                case ".tiff":
                    contentType = "image/tiff";
                    break;

                case ".jpg":
                case ".jpeg":
                    contentType = "image/jpeg";
                    break;

                case ".bmp":
                    contentType = "image/bmp";
                    break;

                default:
                    contentType = "application/octet-stream";
                    break;
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return new FileStreamResult(fileStream, contentType)
            {
                FileDownloadName = fileName
            };
        }

        #endregion Show File

        #endregion List, Ajax

        #region Create

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Please select a category first to add a new book.");
            var model = await _ebookService.GetForCreateAsync(id);
            return PartialView("_Create", model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public virtual async Task<ActionResult> Create(AddEbookViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.Title))
                return BadRequest("Please enter the file title.");

            if (viewModel.File == null || viewModel.File.Length == 0)
                return BadRequest("You have not selected a file.");

            var ext = Path.GetExtension(viewModel.File.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !_fileUploadSettings.PermittedExtensions.Contains(ext))
                return BadRequest("The selected file format is invalid.");

            if (viewModel.File.Length > _fileUploadSettings.FileSizeLimit * 1024 * 1024)
                return BadRequest($"The selected file size is larger than {_fileUploadSettings.FileSizeLimit} MB");

            var result = await _ebookService.CreateAsync(viewModel);
            return PartialView("_item", result);
        }

        #endregion Create

        #region Delete

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (!id.HasValue) return BadRequest();
            var ans = await _ebookService.DeleteAsync(id.Value);
            return Json((ans.DeleteStatus == DeleteStatus.Done) ? "success" : "The operation to add the user group has failed.");
        }

        #endregion Delete

        #region Edit

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var model = await _ebookService.GetForEditAsync(id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public virtual async Task<ActionResult> Edit(EditEbookViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.Title))
                return BadRequest("Please enter the file title.");
            if (viewModel.File is { Length: > 0 })
            {
                var ext = Path.GetExtension(viewModel.File.FileName).ToLowerInvariant();

                if (!string.IsNullOrEmpty(ext) && _fileUploadSettings.PermittedExtensions.Contains(ext))
                    return BadRequest("The selected file format is invalid.");

                if (viewModel.File.Length > 0 && viewModel.File.Length > _fileUploadSettings.FileSizeLimit * 1024 * 1024)
                    return BadRequest($"The selected file size is larger than {_fileUploadSettings.FileSizeLimit * 1024 * 1024} MB");
            }
            var item = await _ebookService.EditAsync(viewModel, GetCurrentUserId());
            return PartialView("_Item", item);
        }

        #endregion Edit

        [HttpGet]
        public IActionResult GetSettingsValue()
        {
            var settings = new
            {
                AllowedExtensions = _siteSettings.FileUploadSettings.PermittedExtensions,
                MaxFileSize = _siteSettings.FileUploadSettings.FileSizeLimit
            };
            return Ok(settings);
        }
    }
}