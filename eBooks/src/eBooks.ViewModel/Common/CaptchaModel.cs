using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBooks.Utility.Constants;

namespace eBooks.ViewModel.Common
{
    public class CaptchaModel
    {
        [Required(ErrorMessage = AttributesErrorMessages.EnterName)]
        public string Captcha { get; set; }
    }
}