using System.ComponentModel.DataAnnotations;
using HeadHunterVer1._0.Attributes;

namespace HeadHunterVer1._0.ViewModels;

public class EditAccountProfileViewModels
{
    [DataType(DataType.Upload, ErrorMessage = "Загрузите изображение")]
    [Display(Name = "Загрутите Обложку")]
    [AllowedExtensions(new []{".png" , ".jpeg" , ".jpg" , ".webp" , ".ico" , ".svg"})]
    public IFormFile? AvatarFile { get; set; }
    
    public string? PathToAvatarFile { get; set; }
    
    [DataType(DataType.Text , ErrorMessage = "Тут должен быть только текст")]
    [Display(Name = "Укажите ваше имя")]
    public string? NameCompanyOrUser { get; set; }
    
    [DataType(DataType.EmailAddress , ErrorMessage = "Тут должен быть только email")]
    [Display(Name = "Укажите Email адрес")]
    public string? Email { get; set; }
    
    [DataType(DataType.Password,ErrorMessage = "Тут должен быть только пароль")]
    [Display(Name = "Введите пароль")]
    public string? OldPassword { get; set; }
    [DataType(DataType.Password,ErrorMessage = "Тут должен быть только пароль")]
    [Display(Name = "Введите пароль")]
    public string? NewPassword { get; set; }
    
    [Required(ErrorMessage = "Выберете вашу роль")]
    public string?  Role { get; set; }
}