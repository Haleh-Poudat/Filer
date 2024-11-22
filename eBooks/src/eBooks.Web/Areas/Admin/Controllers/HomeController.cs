using Microsoft.AspNetCore.Mvc;
using eBooks.ServiceLayer.Contracts.Identity;

namespace eBooks.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
