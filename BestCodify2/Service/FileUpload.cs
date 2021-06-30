using BestCodify_Server.Service.IService;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BestCodify_Server.Service
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileUpload(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _webHostEnvironment = webHostEnvironment;
        }
        public bool DeleteFile(string fileName)
        {
            bool status = false;
            try
            {
                var path = $"{_webHostEnvironment.WebRootPath}\\CourseImages\\{fileName}";
                if (File.Exists(path))
                {
                    File.Delete(path);
                    status = true;
                }
                return status;
            }
            catch (Exception ex)
            {
                return status;
                throw ex;
            }
        }

        public async Task<string> UploadFile(IBrowserFile file)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(file.Name);
                var fileName = Guid.NewGuid().ToString() + fileInfo.Extension;
                var folderDirectory = $"{_webHostEnvironment.WebRootPath}\\CourseImages";
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "CourseImages", fileName);

                var memortStream = new MemoryStream();
                //Buradakı resdım boyutundan bahset onemlı
                await file.OpenReadStream(9999999999).CopyToAsync(memortStream);
                if (!Directory.Exists(folderDirectory))
                    Directory.CreateDirectory(folderDirectory);
                await using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                  
                    memortStream.WriteTo(fs);
                }
                var fullPath = $"CourseImages/{fileName}";
                return fullPath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
