using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBooks.ViewModel.SiteSetting
{
    public class FileUploadSettings
    {
        public string[] PermittedExtensions { get; set; }
        public long FileSizeLimit { get; set; }
    }
}