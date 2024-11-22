using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eBooks.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        public void ShowMessage(string icon, string color, string message)
        {
            var msg = "<script>" +
                      "const Toast = Swal.mixin({" +
                      "toast: true," +
                      "position: 'top-start'," +
                      "showConfirmButton: false," +
                      "background: '" + color + "'," +
                      "color: '#000000'," +
                      "timer: 2000," +
                      "timerProgressBar: true," +
                      //"customClass: {" +
                      //"popup: 'custom-toast-success'" +
                      //"}," +
                      "width: '600px'," +
                      "didOpen: (toast) => {" +
                      "toast.onmouseenter = Swal.stopTimer;" +
                      "toast.onmouseleave = Swal.resumeTimer;" +
                      "}" +
                      "});" +
                      "Toast.fire({" +
                      "icon: '" + icon + "'," +
                      "title: '" + message + "'" +
                      "});" +
                      "</script>";

            TempData["notification"] = msg;
        }

        public string GetCurrentUserName()
        {
            var userNameClaim = User.FindFirst(ClaimTypes.Name);

            if (userNameClaim != null)
            {
                return userNameClaim.Value;
            }

            return "";
        }

        public Guid GetCurrentUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userId != null)
            {
                return Guid.Parse(userId.Value);
            }

            return Guid.Empty;
        }
    }
}