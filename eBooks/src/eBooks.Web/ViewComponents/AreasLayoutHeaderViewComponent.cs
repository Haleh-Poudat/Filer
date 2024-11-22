using Microsoft.AspNetCore.Mvc;

namespace eBooks.Web.ViewComponents
{
    public class AreasLayoutHeaderViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("AreasLayoutHeader");
        }
    }
}
