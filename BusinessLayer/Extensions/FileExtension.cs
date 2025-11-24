using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Extensions
{
    public static class FileExtension
    {
        public static bool CheckContentType(this IFormFile file, string contentType)
        {
            return file.ContentType.ToLower().Trim().Contains(contentType.ToLower().Trim());
        }
        public static bool CheckSize(this IFormFile file, double size)
        {
            return file.Length / 1024 < size;
        }
        public static async Task<string> SaveAsync(this IFormFile file, string rootpath)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            string filename = $"{Guid.NewGuid()}{fileExtension}";
            string root = Path.Combine(rootpath, filename);

            using (FileStream fileStream = new FileStream(root, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return filename;
        }

    }
}
