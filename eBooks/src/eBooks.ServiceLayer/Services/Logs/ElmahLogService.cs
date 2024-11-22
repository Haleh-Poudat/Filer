using eBooks.ServiceLayer.Contracts.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBooks.Datalayer.Context;
using eBooks.Domain.Entities.Logs;
using eBooks.ViewModel.Logs;
using Microsoft.EntityFrameworkCore;

namespace eBooks.ServiceLayer.Services.Logs
{
    public class ElmahLogService : IElmahLogService
    {
        private readonly IUnitOfWork _uow;
        private readonly DbSet<ELMAH_Error> _errors;

        public ElmahLogService(IUnitOfWork uow)
        {
            _uow = uow;
            _errors = _uow.Set<ELMAH_Error>();
        }

        public async Task<List<ErrorViewModel>> GetErrorsAsync()
        {
            var errors = await _errors.OrderByDescending(p=>p.TimeUtc).ToListAsync();
            var errorList = new List<ErrorViewModel>();

            foreach (var error in errors)
            {
                errorList.Add(new ErrorViewModel()
                {
                    ErrorId = error.ErrorId,
                    Application = error.Application,
                    Host = error.Host,
                    Type = error.Type,
                    Source = error.Source,
                    Message = error.Message,
                    User = error.User,
                    StatusCode = error.StatusCode,
                    TimeUtc = error.TimeUtc,
                    AllXml = error.AllXml
                });
            }

            return errorList;
        }

        public async Task ClearErrorsAsync()
        {
            var errorList = await _errors.ToListAsync();
            _errors.RemoveRange(errorList);
            await _uow.SaveChangesAsync();
        }
    }
}