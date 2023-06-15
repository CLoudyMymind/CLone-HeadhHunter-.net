using System.ComponentModel.DataAnnotations;
using HeadHunterVer1._0.Attributes;

namespace HeadHunterVer1._0.ViewModels;
public class RegisterViewModel
{
    [DataType(DataType.Upload, ErrorMessage = "загрузите фотку")]
    [Display(Name = "Загрутите Обложку")]
    [AllowedExtensions(new []{".png" , ".jpeg" , ".jpg" , ".webp" , ".ico" , ".svg"})]
    public IFormFile? AvatarFile { get; set; }
    
    public string? PathToAvatarFile { get; set; }
    
    [DataType(DataType.Text , ErrorMessage = "тут должен быть только текст")]
    [Required(ErrorMessage = "Вы не заполнели имя)")]
    [Display(Name = "Укажите ваше имя")]
    public string NameCompanyOrUser { get; set; }
    
    [DataType(DataType.EmailAddress , ErrorMessage = "тут должен быть только email")]
    [Required(ErrorMessage = "Заполните Email")]
    [RegularExpression (@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
    [Display(Name = "Укажите Email адрес")]
    public string Email { get; set; }
    
    [DataType(DataType.Password,ErrorMessage = "Тут должен быть только пароль")]
    [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
    [Required(ErrorMessage = "Укажите пароль")]
    [Display(Name = "Введите пароль")]
    public string ConfirmPassword { get; set; }
    
    [DataType(DataType.Password,ErrorMessage = "Тут должен быть только пароль")]
    [Required(ErrorMessage = "Укажите пароль")]
    [Display(Name = "Введите пароль")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Выберете вашу роль")]
    public string  Role { get; set; }
    
    [Required(ErrorMessage = "Введите номер телефона")]
    [RegularExpression(@"^(\+7)?(\d{10})$", ErrorMessage = "Номер телефона должен быть в формате +7XXXXXXXXXX.")]
    public string PhoneNumber { get; set; }
    
}

