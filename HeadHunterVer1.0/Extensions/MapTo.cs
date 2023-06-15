using HeadHunterVer1._0.Models;
using HeadHunterVer1._0.Models.Employee;
using HeadHunterVer1._0.Models.Employer;
using HeadHunterVer1._0.ViewModels;
using Microsoft.EntityFrameworkCore;

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
    public Vacancy MapToVacancyEdit(EditVacancyViewModel viewModel, Vacancy model)
    {
        if (!string.IsNullOrEmpty(viewModel.CategoryId))
            model.CategoryId = viewModel.CategoryId;
        if (!string.IsNullOrEmpty(viewModel.Description))
            model.Description = viewModel.Description;
        if (!string.IsNullOrEmpty(viewModel.ExperienceYearsFrom.ToString()))
            model.ExperienceYearsFrom = viewModel.ExperienceYearsFrom;
        if (!string.IsNullOrEmpty(viewModel.ExperienceYearsTo.ToString()))
            model.ExperienceYearsTo = viewModel.ExperienceYearsTo;
        if (!string.IsNullOrEmpty(viewModel.Caption))
            model.Title = viewModel.Caption;
        if (!string.IsNullOrEmpty(viewModel.Wages.ToString()))
            model.Wages = viewModel.Wages.Value;
        return model;
    }


    public List<CategoryViewModel> MapToListCategories(List<Category> categories)
    {
        return categories.Select(c => new CategoryViewModel
        {
            Id = c.Id,
            Name = c.Name
        }).ToList();
    }

    public VacancyJobsCreateViewModel MapToCategoryViewModel(List<Category> model)
    {
        return new VacancyJobsCreateViewModel { CategoryViewModels = model };
    }


    public VacancyViewModel MapVacancyToVacancyViewModel(Vacancy model, List<ResumeViewModel>? resumeViewModels, User? user)
    {
        
        var data =  new VacancyViewModel
        {
            
            Id = model.Id,
            Wages = model.Wages,
            IsPublished = model.IsPublished,
            ExperienceYearsFrom = model.ExperienceYearsFrom,
            ExperienceYearsTo = model.ExperienceYearsTo,
            UpdateVacancyBid = model.UpdateVacancyBid,
            Description = model.Description,
            Title = model.Title,
            SelectedCategoryName = model.Category!,
        };
        if (user != null)
            data.User = user; 
        if (resumeViewModels != null)
            data.ResumeViewModels = resumeViewModels;
        return data;
    }
    public EditVacancyViewModel MapVacancyToEditVacancyViewModel(Vacancy model, List<Category> categories)
    {
        
        return  new EditVacancyViewModel
        {
            Id = model.Id,
            Wages = model.Wages,
            IsPublished = model.IsPublished,
            ExperienceYearsFrom = model.ExperienceYearsFrom,
            ExperienceYearsTo = model.ExperienceYearsTo,
            UpdateVacancyBid = model.UpdateVacancyBid,
            Description = model.Description,
            Caption = model.Title,
            SelectedCategoryName =model.Category.Name,
            CategoryViewModels = MapToListCategories(categories)
        };
    }
    public List<ResumeViewModel> MapListResumeToResumeViewModels(List<Resume> model)
    {
        return model.Select(resume => new ResumeViewModel
        {
            Id = resume.Id,
            Employee = resume.Employee,
            NameOfResume = resume.NameOfResume,
            ExpectedSalary = resume.ExpectedSalary,
            TelegramLink = resume.TelegramLink,
            Email = resume.Email,
            Phone = resume.Phone,
            FacebookLink = resume.FacebookLink,
            LinkedInLink = resume.LinkedInLink,
            UpdatedAt = resume.UpdatedAt,
            Category = resume.Category,
            WorkExperiences = resume.WorkExperiences,
            Courses = resume.Courses
        }).ToList();
    }
    public ResumeViewModel MapResumeToResumeViewModels(Resume model)
    {
        return  new ResumeViewModel
        {
            Id = model.Id,
            Employee = model.Employee,
            NameOfResume = model.NameOfResume,
            ExpectedSalary = model.ExpectedSalary,
            TelegramLink = model.TelegramLink,
            Email = model.Email,
            Phone = model.Phone,
            FacebookLink = model.FacebookLink,
            LinkedInLink = model.LinkedInLink,
            UpdatedAt = model.UpdatedAt,
            Category = model.Category,
            WorkExperiences = model.WorkExperiences,
            Courses = model.Courses
        };
    }

    public  async Task<List<ResumeViewModel>> MapIQueryableResumeToResumeViewModel(IQueryable<Resume> resumeViewModel)
    {
        return await resumeViewModel.Select(u => new ResumeViewModel
        {
            Id = u.Id,
            Employee = u.Employee,
            NameOfResume = u.NameOfResume,
            ExpectedSalary = u.ExpectedSalary,
            TelegramLink = u.TelegramLink,
            Email = u.Email,
            Phone = u.Phone,
            FacebookLink = u.FacebookLink,
            LinkedInLink = u.LinkedInLink,
            UpdatedAt = u.UpdatedAt,
            Category = u.Category,
            WorkExperiences = u.WorkExperiences,
            Courses = u.Courses
        }).ToListAsync();
    }
    public  async Task<List<VacancyViewModel>> MapIQueryableVacancyToVacancyViewModel(IQueryable<Vacancy> vacancyViewModel)
    {
        return await vacancyViewModel.Select(u => new VacancyViewModel
        {
            Title = u.Title,
            NameOfCompany = u.User.UserName,
            Description = u.Description,
            SelectedCategoryName = u.Category,
            UpdateVacancyBid = u.UpdateVacancyBid,
            IsPublished = u.IsPublished,
            ExperienceYearsTo = u.ExperienceYearsTo,
            ExperienceYearsFrom = u.ExperienceYearsFrom,
            Wages = u.Wages,
            Id = u.Id
        }).ToListAsync();
    }

    public ResponseApplication MapToCreateResponseApplication(VacancyViewModel model)
    {
        if (model.CreateApplicationViewModel != null)
        {
            return new ResponseApplication()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = model.CreateApplicationViewModel.UserId,
                VacancyId = model.CreateApplicationViewModel.VacancyId,
                ResumeId = model.CreateApplicationViewModel.selectedResumeId,
                DispatchTime = Convert.ToDateTime(DateTime.Now.ToUniversalTime().ToString("F"))
            };
        }

        throw new Exception("Ошибка");
    }

    public List<ApplicationViewModel> MapFromResponseApplication(List<ResponseApplication> responseApplication)
    {
        
        
            return   responseApplication.Select(u => new ApplicationViewModel
        {

            Id = u.Id,
            IsAcceptOrRejectedResponse = u.IsAcceptOrRejectedResponse != null && u.IsAcceptOrRejectedResponse.Value,
            ResumeViewModel = MapResumeToResumeViewModels(u.Resume),
            VacancyViewModel = MapVacancyToVacancyViewModel(u.Vacancy, null, u.Vacancy.User)
        }).ToList();
       
    }
}