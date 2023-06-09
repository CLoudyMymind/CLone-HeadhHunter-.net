using System.ComponentModel.DataAnnotations;
using HeadHunterVer1._0.Attributes;
using HeadHunterVer1._0.Models;

namespace HeadHunterVer1._0.ViewModels;

public class VacancyJobsCreateViewModel
{
    public string CategoryId { get; set; }

    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [DataType(DataType.Currency , ErrorMessage = "тут должен быть только цифры")]
    public decimal Wages { get; set; }

    public bool IsPublished { get; set; }
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [DataType(DataType.Text , ErrorMessage = "тут должен быть только текст")]

    public string Description { get; set; }
    public List<Category>? CategoryViewModels { get; set; }
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [Range(0 , 80, ErrorMessage = "Минимальное значение опыта 0 до 80")]
    [CustomValidateExperience("ExperienceYearsFrom", "ExperienceYearsTo", ErrorMessage = "Значение От не может быть меньше До")]
    public int? ExperienceYearsFrom { get; set; }
    [Range(1 , 80, ErrorMessage = "Минимальное значение опыта 1 до 80")]
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    public int? ExperienceYearsTo { get; set; }
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [DataType(DataType.Text , ErrorMessage = "тут должен быть только текст")]
    public string NameOfVacancy { get; set; }
}