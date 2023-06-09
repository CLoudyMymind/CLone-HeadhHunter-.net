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


 
    public async Task<List<VacancyViewModel>> GetALlVacancyInUserAsync(ClaimsPrincipal user)
    {
        var dataUser = _userManager.GetUserId(user);
        var vacancyViewModels = await _headHunterContext.Vacancies
            .Include(v => v.Category).OrderByDescending(v => v.UpdateVacancyBid).Where(v => v.UserId == dataUser)
            .Select(v => _mapTo.MapVacancyToVacancyViewModel(v)).ToListAsync();
        return vacancyViewModels;
    }

    public async Task<List<VacancyViewModel>> GetListVacancy()
    {
        var vacancyViewModels = await _headHunterContext.Vacancies
            .Include(v => v.Category).OrderByDescending(v => v.UpdateVacancyBid).Select(v => _mapTo.MapVacancyToVacancyViewModel(v)).ToListAsync();
        return vacancyViewModels;
    }
    private async Task<Vacancy> GetByIdVacancyAsync(string? id)
    {
        return  await _headHunterContext.Vacancies.Include(c => c.Category).OrderByDescending(v => v.UpdateVacancyBid)
            .FirstOrDefaultAsync(v => v.Id == id)
                 ?? throw new Exception("Такой ваканси нету в бд");
        
    }

    public async Task UpdateDate(string id )
    {
      var data = await GetByIdVacancyAsync(id);
      data.UpdateVacancyBid = Convert.ToDateTime(DateTime.Now.ToUniversalTime().ToString("F"));
      _headHunterContext.Vacancies.Update(data);
      await _headHunterContext.SaveChangesAsync();
    }

    public async Task<VacancyViewModel> AboutVacancyAsync(string id )=> 
        _mapTo.MapVacancyToVacancyViewModel(await GetByIdVacancyAsync(id));
    public async Task UpdatePublishStatusAsync(string id,  bool isPublished)
    {
        var data = await GetByIdVacancyAsync(id);
        data.IsPublished = isPublished;
        _headHunterContext.Vacancies.Update(data);
        await _headHunterContext.SaveChangesAsync();
    }
    public async Task<EditVacancyViewModel> EditVacancyAsync(string id)=> 
        _mapTo.MapVacancyToEditVacancyViewModel(await GetByIdVacancyAsync(id), await _categoryService.GetAllCategoryListAsync());

    public async Task EditVacancyAsync(EditVacancyViewModel model, ClaimsPrincipal user)
    {
        var loggedInUser = await _userManager.GetUserAsync(user);
        _ = loggedInUser == null
            ? throw new Exception("Произошла ошибка при создании задачи") 
            : ( _headHunterContext.Vacancies.Update(_mapTo.MapToVacancyEdit(model,await GetByIdVacancyAsync(model.Id) ))
                , await _headHunterContext.SaveChangesAsync());
    }
    public async Task<bool> DeleteVacancy(string id)
    {
        var data =  await _headHunterContext.Vacancies.FirstOrDefaultAsync(v => v.Id == id);
        if (data == null) return false;
        _headHunterContext.Vacancies.Remove(data);
        await _headHunterContext.SaveChangesAsync();
        return true;
    }
}