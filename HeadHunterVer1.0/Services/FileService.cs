using System.Security.Claims;
using HeadHunterVer1._0.Services.Abstractions;
using HeadHunterVer1._0.ViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace HeadHunterVer1._0.Services;

public class FileService : IFileService
{
    private readonly IEmployeeService _employeeService;
    private readonly IUserService _userService;

    public FileService(IEmployeeService employeeService, IUserService userService)
    {
        _employeeService = employeeService;
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
        var defaultImagePath =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "StandartPhoto", "standart.png");
        var defaultImageBytes = await File.ReadAllBytesAsync(defaultImagePath);
        model.AvatarFile = new FormFile(new MemoryStream(defaultImageBytes), 0, defaultImageBytes.Length,
            "standart.png", "standart.png")
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
    ///     метод для генирации файла pdf  пользователя с 2 параметрами
    /// </summary>
    /// <param name="id">айдишник юзера</param>
    /// <param name="user">сам юзер</param>
    /// <returns></returns>
   public async Task<byte[]> GeneratePdfAsync(int id, ClaimsPrincipal user)
{
    var dataUser = await _userService.UserSearchAsync(null, user);
    var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
    var imagesPath = Path.Combine(wwwrootPath + dataUser.PathFile);
    var data = await _employeeService.GetAllResume();
    var searchData = data.FirstOrDefault(r => r.Id == id);
    if (dataUser != null && searchData != null && data != null)
        searchData.Employee = dataUser;

    if (searchData != null)
    {
        var document = new Document();
        var output = new MemoryStream();
        var writer = PdfWriter.GetInstance(document, output);
        document.Open();
        var headingFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16f, Font.NORMAL, BaseColor.BLACK);
        var subheadingFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12f, Font.NORMAL, BaseColor.BLACK);
        document.Add(new Paragraph($"Resume-Num №{searchData.Id}", headingFont));
        document.Add(new Paragraph($"Name: {searchData.Employee.UserName}", subheadingFont));
        document.Add(new Paragraph($"Expected Salary: {searchData.ExpectedSalary}", subheadingFont));
        document.Add(new Paragraph($"Telegram: {searchData.TelegramLink}", subheadingFont));
        document.Add(new Paragraph($"Email: {searchData.Email}", subheadingFont));
        document.Add(new Paragraph($"Phone Number: {searchData.Phone}", subheadingFont));
        document.Add(new Paragraph($"Category: {searchData.Category.Name}", subheadingFont));

        if (searchData.FacebookLink != null)
            document.Add(new Paragraph($"Facebook: {searchData.FacebookLink}", subheadingFont));

        if (searchData.LinkedInLink != null)
            document.Add(new Paragraph($"LinkedIn: {searchData.LinkedInLink}", subheadingFont));

        if (searchData.UpdatedAt != null)
            document.Add(new Paragraph($"Last Update Resume: {searchData.UpdatedAt}", subheadingFont));
        if (searchData.WorkExperiences.Any())
        {
            document.Add(new Paragraph("Work Experiences:", headingFont));
            foreach (var experience in searchData.WorkExperiences)
            {
                var paragraph = new Paragraph($"Place of Work: {experience.CompanyName}", subheadingFont);
                paragraph.IndentationRight= 40f; 

                paragraph = new Paragraph($"Position: {experience.Post}", subheadingFont);
                paragraph.IndentationRight = 40f; 
                document.Add(paragraph);

                paragraph = new Paragraph($"Responsibilities: {experience.Responsibilities}", subheadingFont);
                paragraph.IndentationRight = 40f; 
                document.Add(paragraph);

                paragraph = new Paragraph($"Work Experience: {experience.YearsOfWork} years", subheadingFont);
                paragraph.IndentationRight = 40f; 
                document.Add(paragraph);

                document.Add(new Paragraph("----------------------------------"));
            }
        }
        var image = Image.GetInstance(imagesPath);
        image.ScaleAbsolute(100f, 100f);
        image.Alignment = Image.ALIGN_RIGHT;
        image.SetAbsolutePosition(document.Right - image.ScaledWidth, document.Top - image.ScaledHeight);
        document.Add(image);

        if (searchData.Courses.Any())
        {
            document.Add(new Paragraph("Courses:", headingFont));
            foreach (var course in searchData.Courses)
            {
                var paragraph = new Paragraph($"Course Name: {course.CourseName}", subheadingFont);
                paragraph.IndentationRight = 40f; 
                document.Add(paragraph);

                paragraph = new Paragraph($"Direction of Courses: {course.EducatedPost}", subheadingFont);
                paragraph.IndentationRight = 40f; 
                document.Add(paragraph);

                document.Add(new Paragraph("----------------------------------"));
            }
        }

        document.Close();
        return output.ToArray();
    }

    throw new Exception("An error occurred while generating the PDF file");
}



    public string ContentTypeFile()
    {
        return "application/pdf";
    }

    public string GeneratePdfFileName(int id)
    {
        return $"{id}.pdf";
    }
}