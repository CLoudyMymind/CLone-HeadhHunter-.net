using System.Security.Claims;
using HeadHunterVer1._0.Services.Abstractions;
using HeadHunterVer1._0.ViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace HeadHunterVer1._0.Services;

public class FileService : IFileService
{
    private readonly IUserService _userService;

    public FileService(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<string> FileRegisterCheckAsync(RegisterViewModel model)
    {
        if (model.AvatarFile is not null)
        {
            var allowedMimeTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/webp" };
            var mimeType = MimeTypes.GetMimeType(Path.GetExtension(model.AvatarFile.FileName));
            if (!allowedMimeTypes.Contains(mimeType))
            {
                throw new Exception();
            }
        }
        var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        var imagesPath = Path.Combine(wwwrootPath, "images");
        if (!Directory.Exists(imagesPath)) Directory.CreateDirectory(imagesPath);

        string fileName;
        string filePath;
        if (model.AvatarFile == null)
        {
            fileName = "standart.jpeg";
            filePath = Path.Combine(wwwrootPath, "ImagesStandart", fileName);
            var defaultImageBytes = await File.ReadAllBytesAsync(filePath);
            model.AvatarFile = new FormFile(new MemoryStream(defaultImageBytes), 0, defaultImageBytes.Length,
                fileName, fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };
            var userFilePath = Path.Combine(imagesPath, Guid.NewGuid() + Path.GetExtension(model.AvatarFile.FileName));
            await using var userStream = new FileStream(userFilePath, FileMode.Create, FileAccess.Write, FileShare.None,
                4096, FileOptions.Asynchronous);
            await model.AvatarFile.CopyToAsync(userStream);
            return Path.GetFileName(userFilePath);
        }

        fileName = Guid.NewGuid() + Path.GetExtension(model.AvatarFile.FileName);
        filePath = Path.Combine(imagesPath, fileName);
        await using var stream = new FileStream(filePath, FileMode.Create);
        await model.AvatarFile.CopyToAsync(stream);

        return fileName;
    }
}