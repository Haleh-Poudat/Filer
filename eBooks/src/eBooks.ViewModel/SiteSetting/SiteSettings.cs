using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBooks.ViewModel.SiteSetting
{
    public class SiteSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public IdentityOptionsSettings IdentityOptionsSettings { get; set; }
        public FileUploadSettings FileUploadSettings { get; set; }
        public PasswordBanListSettings PasswordBanListSettings { get; set; }
    }
}