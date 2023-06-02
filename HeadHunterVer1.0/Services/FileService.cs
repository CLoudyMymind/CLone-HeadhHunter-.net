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

    private void ValidateAvatarFile(IFormFile? avatarFile)
    {
        if (avatarFile is null) return;
        var allowedMimeTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/webp" };
        var mimeType = MimeTypes.GetMimeType(Path.GetExtension(avatarFile.FileName));
        if (!allowedMimeTypes.Contains(mimeType)) throw new Exception("Invalid avatar file format.");
    }
    
    private async Task<string> SaveAvatarFile(IFormFile avatarFile)
    {
        var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        var imagesPath = Path.Combine(wwwrootPath, "images");
        if (!Directory.Exists(imagesPath)) Directory.CreateDirectory(imagesPath);
        var fileName = Guid.NewGuid() + Path.GetExtension(avatarFile.FileName);
        var filePath = Path.Combine(imagesPath, fileName);
        await using var stream = new FileStream(filePath, FileMode.Create);
        await avatarFile.CopyToAsync(stream);
        return fileName;
    }

    public async Task<string> FileRegisterCheckAsync(RegisterViewModel model)
    {
        ValidateAvatarFile(model.AvatarFile);
        if (model.AvatarFile is not null) return await SaveAvatarFile(model.AvatarFile);
        var defaultImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "StandartPhoto", "standart.png");
        var defaultImageBytes = await File.ReadAllBytesAsync(defaultImagePath);
        model.AvatarFile = new FormFile(new MemoryStream(defaultImageBytes), 0, defaultImageBytes.Length, "standart.png", "standart.png")
        {
            Headers = new HeaderDictionary(),
            ContentType = "image/png"
        };
        return await SaveAvatarFile(model.AvatarFile);
    }
    
    public async Task<string> FileEditAsync(EditAccountProfileViewModels model)
    {
        ValidateAvatarFile(model.AvatarFile);
        return await SaveAvatarFile(model.AvatarFile);
    }


    /// <summary>
    /// метод для генирации файла pdf  пользователя с 2 параметрами
    /// </summary>
    /// <param name="id">айдишник юзера</param>
    /// <param name="user">сам юзер</param>
    /// <returns></returns>
    public async Task<byte[]> GeneratePdfAsync(string id, ClaimsPrincipal user)
    {
        var dataSearchToUser = await _userService.UserSearchAsync(id, user);
        var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        var imagesPath = Path.Combine(wwwrootPath + dataSearchToUser.PathFile);
        var dataUserName = dataSearchToUser.UserName;
        var avatarFile = imagesPath;
        var dataUserEmail = dataSearchToUser.Email;
        using var document = new Document();
        using var outputStream = new MemoryStream();
        using (var writer = PdfWriter.GetInstance(document, outputStream))
        {
            document.Open();
            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
            var title = new Paragraph("Данные пользователя", titleFont) { Alignment = Element.ALIGN_CENTER };
            document.Add(title);
            var contentFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
            var userData = new Paragraph
            {
                new Chunk("Name of user: ", contentFont),
                new Chunk(dataUserName, contentFont) { Font = contentFont },
                Chunk.NEWLINE,
                new Chunk("Email of user: ", contentFont),
                new Chunk(dataUserEmail, contentFont) { Font = contentFont }
            };
            document.Add(userData);
            if (!string.IsNullOrEmpty(avatarFile))
            {
                var avatarImage = Image.GetInstance(avatarFile);
                avatarImage.ScaleToFit(100f, 100f);
                document.Add(avatarImage);
            }
            document.Close();
        }
        return outputStream.ToArray();
    }
    
    public string ContentTypeFile() =>   "application/pdf";
    public string GeneratePdfFileName(string id) => $"{id}.pdf";
}