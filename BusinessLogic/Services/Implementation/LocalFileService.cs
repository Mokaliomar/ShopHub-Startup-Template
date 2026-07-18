using System;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BusinessLogic.Services.Implementation;

public class LocalFileService : IFileService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    public LocalFileService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public string UploadFile(IFormFile? file, string folderName)
    {
        string RootPath = _webHostEnvironment.WebRootPath;

        if (file != null)
        {
            string filename = Guid.NewGuid().ToString();
            var Upload = Path.Combine(RootPath, folderName);
            var ext = Path.GetExtension(file.FileName);

            using (var filestream = new FileStream(Path.Combine(Upload, filename + ext), FileMode.Create))
            {
                file.CopyTo(filestream);
            }
            // productVM.Product.Img = @"Images\Products\" + filename + ext;
            return filename + ext;
        }
        return "";
    }

    public bool DeleteFile(string filePath)
    {
        string root = _webHostEnvironment.WebRootPath;
        string absoluteFilePath = Path.Combine(root, filePath);

        if (File.Exists(absoluteFilePath))
        {
            File.Delete(absoluteFilePath);
            return true;
        }
        return false;
    }
    public (bool isValid, string errorMessage) ValidateImage(IFormFile? file)
    {
        if(file is null)
        {
            return (false, "The Image is not found");
        }
        
        long maxSizeBytes = 2 * 1024 * 1024;
        if(file.Length > maxSizeBytes) // If the file size is larger than 2MB don't accept it !
        {
            return (false, "The Image size is larger than 2MB");
        }

        string[] acceptedExtensions = [".jpg", ".jpeg", ".png", ".webp"];
        // var root = _webHostEnvironment.WebRootPath;
        var ImgExtension = Path.GetExtension(file.FileName);
        if (acceptedExtensions.Contains(ImgExtension))
        {
            return (true, "The Image is accepted according to the rules");
        }
        return (false, "The Image extension is not supported, please choose (.jpg, .jpeg, .png, .webp) Extensions");
    }
}
