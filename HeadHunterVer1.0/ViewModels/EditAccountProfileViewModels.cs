using System.ComponentModel.DataAnnotations;
using HeadHunterVer1._0.Attributes;

namespace HeadHunterVer1._0.ViewModels;

public class EditAccountProfileViewModels
{
    [DataType(DataType.Upload, ErrorMessage = "Загрузите изображение")]
    [Display(Name = "Загрутите Обложку")]
    [AllowedExtensions(new []{".png" , ".jpeg" , ".jpg" , ".webp" , ".ico" , ".svg"})]
    public IFormFile? AvatarFile { get; set; }
    
    
    [DataType(DataType.Text , ErrorMessage = "Тут должен быть только текст")]
    [Display(Name = "Укажите ваше имя")]
    public string? NameCompanyOrUser { get; set; }
    
    [DataType(DataType.EmailAddress , ErrorMessage = "Тут должен быть только email")]
    [Display(Name = "Укажите Email адрес")]
    [RegularExpression (@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]


    public string? Email { get; set; }
    
    [DataType(DataType.Password,ErrorMessage = "Тут должен быть только пароль")]
    [Display(Name = "Введите пароль")]
    public string? OldPassword { get; set; }
    [DataType(DataType.Password,ErrorMessage = "Тут должен быть только пароль")]
    [Display(Name = "Введите пароль")]
    public string? NewPassword { get; set; }
    
    public string?  Role { get; set; }
    [RegularExpression(@"^(\+7)?(\d{10})$", ErrorMessage = "Номер телефона должен быть в формате +7XXXXXXXXXX.")]
    [Display(Name = "Введите номер")]
    public string? PhoneNumber  { get; set; }
}