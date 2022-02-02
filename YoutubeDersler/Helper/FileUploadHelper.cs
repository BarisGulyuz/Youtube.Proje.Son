using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace YoutubeDersler.Helper
{
    public class FileUploadHelper
    {

        private readonly IWebHostEnvironment _env;

        public FileUploadHelper(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<string> ImageUpload(IFormFile formFile, string name)
        {
            var pictureFile = formFile.FileName;
            string wwwroot = _env.WebRootPath;
            DateTime dateTime = DateTime.Now;

            string fileExtension = Path.GetExtension(pictureFile);
            string fileName = $"{name}_{dateTime.ToShortDateString()}_{fileExtension}";
            var path = Path.Combine($"{wwwroot}/images", fileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
            return fileName;
        }
    }
}
