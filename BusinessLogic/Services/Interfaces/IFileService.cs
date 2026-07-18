using System;
using Microsoft.AspNetCore.Http;

namespace BusinessLogic.Services.Interfaces;

public interface IFileService
{
    string UploadFile(IFormFile? file, string folderName);
    bool DeleteFile(string filePath);
    (bool isValid, string errorMessage) ValidateImage(IFormFile? file);
}
