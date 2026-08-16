using System;

namespace myshop.Web.Utilities;

public static class FileHelper
{
    public static bool ImageExists(string? imgPath, string webRootPath)
    {
        if (string.IsNullOrEmpty(imgPath))
            return false;

        var absolutePath = Path.Combine(webRootPath, imgPath.TrimStart('/', '\\'));
        return File.Exists(absolutePath);
    }
}
