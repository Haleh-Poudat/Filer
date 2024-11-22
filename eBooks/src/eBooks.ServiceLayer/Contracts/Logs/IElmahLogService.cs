using eBooks.ViewModel.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBooks.ServiceLayer.Contracts.Logs
{
    public interface IElmahLogService
    {
        public Task<List<ErrorViewModel>> GetErrorsAsync();

        public Task ClearErrorsAsync();
    }
}