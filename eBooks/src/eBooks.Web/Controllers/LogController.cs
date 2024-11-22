using eBooks.ServiceLayer.Contracts.Logs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eBooks.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LogController : Controller
    {
        private readonly IElmahLogService _logService;

        public LogController(IElmahLogService logService)
        {
            _logService = logService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var list = await _logService.GetErrorsAsync();
            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> ClearAsync()
        {
            await _logService.ClearErrorsAsync();
            return RedirectToAction("Index");
        }
    }
}