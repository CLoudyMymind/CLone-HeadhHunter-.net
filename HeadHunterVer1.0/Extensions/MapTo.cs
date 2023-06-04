using HeadHunterVer1._0.Models;
using HeadHunterVer1._0.Models.Employer;
using HeadHunterVer1._0.ViewModels;

namespace HeadHunterVer1._0.Extensions;

public class MapTo
{
    public Vacancy MapToVacancyCreate(VacancyJobsCreateViewModel model, string id)
    {
        var newVacancy = new Vacancy
        {
            Id = Guid.NewGuid().ToString(),
            CategoryId = model.CategoryId,
            Description = model.Description,
            ExperienceYearsFrom = model.ExperienceYearsFrom,
            ExperienceYearsTo = model.ExperienceYearsTo,
            IsPublished = model.IsPublished,
            Wages = model.Wages,
            UserId = id,
            Title = model.NameOfVacancy,
            UpdateVacancyBid = Convert.ToDateTime(DateTime.Now.ToUniversalTime().ToString("F"))
        };
        return newVacancy;
    }

    public List<CategoryViewModel> MapToListCategories(List<Category> categories)
    {
        return categories.Select(c => new CategoryViewModel
        {
            Id = c.Id,
            Name = c.Name
        }).ToList();
    }

    public VacancyJobsCreateViewModel MapToCategoryViewModel(List<CategoryViewModel> model)
    {
        return new VacancyJobsCreateViewModel { CategoryViewModels = model };
    }

    public VacancyViewModel MapVacancyToVacancyViewModel(Vacancy model)
    {
        
        return  new VacancyViewModel
        {
            Id = model.Id,
            Wages = model.Wages,
            IsPublished = model.IsPublished,
            ExperienceYearsFrom = model.ExperienceYearsFrom,
            ExperienceYearsTo = model.ExperienceYearsTo,
            UpdateVacancyBid = model.UpdateVacancyBid,
            Description = model.Description,
            Title = model.Title,
            SelectedCategoryName =model.Category.Name,
        };
    }

   
}