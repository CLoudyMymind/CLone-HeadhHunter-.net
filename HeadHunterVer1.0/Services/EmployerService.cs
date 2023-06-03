using System.Security.Claims;
using HeadHunterVer1._0.Context;
using HeadHunterVer1._0.Extensions;
using HeadHunterVer1._0.Models;
using HeadHunterVer1._0.Models.Employer;
using HeadHunterVer1._0.Services.Abstractions;
using HeadHunterVer1._0.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HeadHunterVer1._0.Services;

public class EmployerService : IEmployerService
{
    private readonly HeadHunterContext _headHunterContext;
    private readonly UserManager<User> _userManager;
    private readonly MapTo _mapTo;
    private readonly ICategoryService _categoryService;
   

    public EmployerService(HeadHunterContext headHunterContext, 
        UserManager<User> userManager, MapTo mapTo, ICategoryService categoryService)
    {
        _headHunterContext = headHunterContext;
        _userManager = userManager;
        _mapTo = mapTo;
        _categoryService = categoryService;
        
    }

    public async Task CreateVacancyAsync(VacancyJobsCreateViewModel model, ClaimsPrincipal user)
    {
        var loggedInUser = await _userManager.GetUserAsync(user);
        _ = loggedInUser != null
            ? (await _headHunterContext.Vacancies.AddAsync(_mapTo.MapToVacancyCreate(model, loggedInUser.Id)), await _headHunterContext.SaveChangesAsync())
            : throw new Exception("Произошла ошибка при создании задачи");
    }

    public async Task<List<VacancyViewModel>> GetALlVacancyAsync(ClaimsPrincipal user)
    {
        var dataUser = _userManager.GetUserId(user);
        var vacancyViewModels = await _headHunterContext.Vacancies
            .Include(v => v.Category).Where(v => v.UserId == dataUser).Select(v => new VacancyViewModel
        {
            Title = v.Title,
            Description = v.Description,
            ExperienceYearsTo = v.ExperienceYearsTo,
            ExperienceYearsFrom = v.ExperienceYearsFrom,
            Wages = v.Wages,
            UpdateVacancyBid = v.UpdateVacancyBid,
            Id = v.Id,
            IsPublished = v.IsPublished,
            SelectedCategoryName = v.Category.Name 
        }).ToListAsync();
        return vacancyViewModels;
    }

    public async Task<Vacancy> GetByIdVacancy(string? id, ClaimsPrincipal user)
    {
          var  data = await _headHunterContext.Vacancies.Include(c => c.Category).Where(v => v.UserId == _userManager.GetUserId(user)).FirstOrDefaultAsync(v => v.Id == id);
          Console.WriteLine(data);
          return data;
    }

    public async Task<VacancyViewModel> AboutVacancy(string id , ClaimsPrincipal user )=> 
        _mapTo.MapVacancyToVacancyViewModel(await GetByIdVacancy(id, user));

    
}