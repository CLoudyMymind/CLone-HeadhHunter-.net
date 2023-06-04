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
   

    public EmployerService(HeadHunterContext headHunterContext, 
        UserManager<User> userManager, MapTo mapTo)
    {
        _headHunterContext = headHunterContext;
        _userManager = userManager;
        _mapTo = mapTo;
        
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
            .Include(v => v.Category).OrderByDescending(v => v.UpdateVacancyBid).Where(v => v.UserId == dataUser)
            .Select(v => _mapTo.MapVacancyToVacancyViewModel(v)).ToListAsync();
        return vacancyViewModels;
    }

    public async Task<Vacancy> GetByIdVacancy(string? id, ClaimsPrincipal user)
    {
        return  await _headHunterContext.Vacancies.Include(c => c.Category).OrderByDescending(v => v.UpdateVacancyBid)
            .Where(v => v.UserId == _userManager.GetUserId(user)).FirstOrDefaultAsync(v => v.Id == id)
                 ?? throw new Exception("Такой ваканси нету в бд");
        
    }

    public async Task UpdateDate(string id , ClaimsPrincipal user)
    {
      var data = await GetByIdVacancy(id, user);
      data.UpdateVacancyBid = Convert.ToDateTime(DateTime.Now.ToUniversalTime().ToString("F"));
      _headHunterContext.Vacancies.Update(data);
      await _headHunterContext.SaveChangesAsync();
    }

    public async Task<VacancyViewModel> AboutVacancy(string id , ClaimsPrincipal user )=> 
        _mapTo.MapVacancyToVacancyViewModel(await GetByIdVacancy(id, user));
    public async Task UpdatePublishStatus(string id, ClaimsPrincipal user, bool isPublished)
    {
        var data = await GetByIdVacancy(id, user);
        data.IsPublished = isPublished;
        _headHunterContext.Vacancies.Update(data);
        await _headHunterContext.SaveChangesAsync();
    }

}