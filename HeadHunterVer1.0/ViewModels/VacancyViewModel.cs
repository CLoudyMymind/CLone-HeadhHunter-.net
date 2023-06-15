using HeadHunterVer1._0.Models;

namespace HeadHunterVer1._0.ViewModels;

public class VacancyViewModel
{
    public string Id { get; set; }

    public string NameOfCompany { get; set; }
    public decimal Wages { get; set; }

    public User  User { get; set; }
    public bool IsPublished { get; set; }
    public int? ExperienceYearsFrom { get; set; }
    
    public int? ExperienceYearsTo { get; set; }

    public DateTime UpdateVacancyBid { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
    public Category SelectedCategoryName { get; set; }

    public List<ResumeViewModel> ResumeViewModels { get; set; }

    public CreateApplicationViewModel CreateApplicationViewModel { get; set; }
}
