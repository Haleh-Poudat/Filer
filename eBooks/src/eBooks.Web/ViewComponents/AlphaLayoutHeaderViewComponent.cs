using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace eBooks.Web.ViewComponents
{
    public class AlphaLayoutHeaderViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("AlphaLayoutHeader");
        }
    }
}
