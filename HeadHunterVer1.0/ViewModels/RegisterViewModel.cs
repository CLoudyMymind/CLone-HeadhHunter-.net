namespace HeadHunterVer1._0.ViewModels;
// вьюмодель для регистрации
public class RegisterViewModel
{
    public IFormFile AvatarFile { get; set; }
    
    public string? PathToAvatarFile { get; set; }


    public string NameCompanyOrUser { get; set; }
    
    public string Email { get; set; }

    public string ConfirmPassword { get; set; }
    
    public string Password { get; set; }
    // свойство role под вопросом нужна ли.
    // задумка по ней создавать и после проверять и добавлять в юзера роль
    
    public string  Role { get; set; }
    
}