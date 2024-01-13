using Microsoft.Extensions.Hosting;
using Backend_Project_Amado.Entities;
using System;
namespace Backend_Project_Amado.Services
{
    public class FileService
    {
        public List<string> AddFile(List<IFormFile> files, string targetDirectory)
        {
            List<string> fileNames = new();

            string path = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("wwwroot", targetDirectory));
            
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (files != null)
            {
                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file.FileName);
                    var fileName = fileInfo.Name + Guid.NewGuid().ToString() + fileInfo.Extension;

                    string fileNameWithPath = Path.Combine(path, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    fileNames.Add(fileName);
                }
            }
            return fileNames;
        }

        public void DeleteFile(string fileName, string targetDirectory)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("wwwroot", targetDirectory, fileName));

            if (!File.Exists(path)) return;

            File.Delete(path);
        }

    }
}

