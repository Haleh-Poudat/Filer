using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBooks.ViewModel.Common
{
    public class DeleteMsgViewModel
    {
        #region MyRegion

        public DeleteMsgViewModel()
        {
            Refresh = false;
            DeleteType = DeleteType.Other;
        }

        #endregion MyRegion

        public string Message { set; get; }

        public DeleteStatus DeleteStatus { set; get; }

        public DeleteType DeleteType { set; get; }

        public bool Refresh { set; get; }
    }

    public enum DeleteStatus
    {
        Done,
        Error
    }

    public enum DeleteType
    {
        OrgChart,
        Other,
        ReloadOrder,
        FancyTree
    }
}