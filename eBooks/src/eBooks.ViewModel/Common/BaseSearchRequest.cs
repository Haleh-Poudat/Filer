using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBooks.ViewModel.Common
{
    public class BaseSearchRequest
    {
        public BaseSearchRequest()
        {
            PageSize = 20;
            PageIndex = 1;
            SortDirection = SortDirectionMode.Desc;
        }

        [DisplayName("Search")]
        public string Term { get; set; }

        public string SortDirection { get; set; }

        /// <summary>
        /// All Data
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Page Number
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Count of data in one page
        /// </summary>
        public int PageSize { get; set; }

        public string CurrentSort { get; set; }
    }
}