using HeadHunterVer1._0.Models;
using HeadHunterVer1._0.Models.Employer;
using HeadHunterVer1._0.Services.Abstractions;
using HeadHunterVer1._0.ViewModels;

namespace HeadHunterVer1._0.Extensions;

public class MapTo
{
   
    public Vacancy MapToVacancy(VacancyJobsCreateViewModel model, string id)
    {
        var newVacancy = new Vacancy
        {
            Id = Guid.NewGuid().ToString(),
            CategoryId = model.CategoryId,
            ExperienceYearsFrom = model.ExperienceYearsFrom,
            ExperienceYearsTo = model.ExperienceYearsTo,
            IsPublished = model.IsPublished,
            Wages = model.Wages,
            UserId = id,
            UpdateVacancyBid = Convert.ToDateTime(DateTime.Now.ToUniversalTime().ToString("F"))
        };
        return newVacancy;
    }

    public List<CategoryViewModel> MapToListCategories(List<Category> categories)
    {
        return  categories.Select(c => new CategoryViewModel
        {
            Id = c.Id,
            Name = c.Name
        }).ToList();
    }
    public  VacancyJobsCreateViewModel MapToCategoryViewModel(List<CategoryViewModel> model) => new() { CategoryViewModels = model};
    


}