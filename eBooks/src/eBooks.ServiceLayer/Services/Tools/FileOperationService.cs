using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eBooks.Datalayer.Context;
using eBooks.Domain.Entities.Library;
using eBooks.ServiceLayer.Contracts.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using eBooks.ViewModel.Library.Ebook;
using static System.Net.Mime.MediaTypeNames;
using SixLabors.ImageSharp;

namespace eBooks.ServiceLayer.Services.Tools
{
    public class FileOperationService
    {
        protected async Task<BookFileViewModel> SaveFileAsync(IFormFile file)
        {
            var model = new BookFileViewModel();
            if (file == null || file.Length == 0)
                return model;
            var extension = Path.GetExtension(file.FileName);
            var newFileName = $"{Guid.NewGuid()}{extension}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", newFileName);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            model = new BookFileViewModel()
            {
                Name = newFileName,
                Path = path
            };
            return model;
        }

        [HttpPost]
        public async Task<BookFileViewModel> EditFile(EditBookFileViewModel model)
        {
            if (File.Exists(model.OldPath))
            {
                File.Delete(model.OldPath);
            }

            var extension = Path.GetExtension(model.Name);
            var newFileName = $"{Guid.NewGuid()}{extension}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", newFileName);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await model.File.CopyToAsync(stream);
            }

            var result = new BookFileViewModel()
            {
                Name = newFileName,
                Path = path
            };
            return result;
        }

        public IActionResult DeleteFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return null;
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
                return null;
            }
            else
            {
                return null;
            }
        }
    }
}