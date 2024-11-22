using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace eBooks.ServiceLayer.Services.Tools
{
    public class ImageService
    {
        protected async Task<string> SaveImageAsync(IFormFile file, string folderName, int width, int height)
        {
            string imageName = Path.ChangeExtension(Guid.NewGuid().ToString(), ".jpg");
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images/{folderName}/", imageName);

            await using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            if (width != 0 && height != 0)
            {
                using var image = await Image.LoadAsync(imagePath);
                image.Mutate(x => x.Resize(227, 227));
                await image.SaveAsync(imagePath);
            }
            return imageName;
        }

        protected void DeleteImage(string imageName, string folderName)
        {
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images/{folderName}/", imageName);
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }

        protected async Task<string> UpdateImageAsync(IFormFile file, string folderName, int width, int height, string imageName)
        {
            DeleteImage(imageName, folderName);
            var newImageName = await SaveImageAsync(file, folderName, width, height);
            return newImageName;
        }
    }
}