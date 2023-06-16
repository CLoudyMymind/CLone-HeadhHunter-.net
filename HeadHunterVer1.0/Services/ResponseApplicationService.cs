using System.Security.Claims;
using HeadHunterVer1._0.Context;
using HeadHunterVer1._0.Extensions;
using HeadHunterVer1._0.Models;
using HeadHunterVer1._0.Services.Abstractions;
using HeadHunterVer1._0.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HeadHunterVer1._0.Services;

public class ResponseApplicationService : IResponseApplicationService
{
    private readonly HeadHunterContext _db;
    private readonly MapTo _mapTo;
    private readonly UserManager<User> _userManager;

    public ResponseApplicationService(HeadHunterContext headHunterContext, MapTo mapTo, UserManager<User> userManager)
    {
        _db = headHunterContext;
        _mapTo = mapTo;
        _userManager = userManager;
    }

    private async Task<List<ResponseApplication>> ListResponseApplicationAsync() => await _db.ResponseApplications.ToListAsync();
    public  async Task<bool> Create(VacancyViewModel model)
    {
        var checkIsApplication = await ListResponseApplicationAsync();
        if (checkIsApplication.Any(t => t.ResumeId == model.CreateApplicationViewModel.selectedResumeId))
        {
            return false;
        }
        await _db.ResponseApplications.AddAsync(_mapTo.MapToCreateResponseApplication(model));
        await _db.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// вывод откликов для работодателя
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<List<ApplicationViewModel>> GetAllApplicationViewModelInEmployerAsync(ClaimsPrincipal user)
    {
        var dataInUser = await _userManager.GetUserAsync(user);
        if (dataInUser != null)
        {
            return _mapTo.MapFromResponseApplication( await _db.ResponseApplications.Include(r => r.Vacancy)
                .ThenInclude(r => r.User)
                .Include(r => r.Resume)
                .ThenInclude(c => c.WorkExperiences)
                .Include(r => r.Resume).ThenInclude(c => c.Courses)
                .Include(r => r.User)
                .Where(r => r.Vacancy.User.Id == dataInUser.Id)
                .OrderByDescending(r => r.DispatchTime)
                .ToListAsync());
        }
        throw new Exception("Произошла ошибка такого юзера нету");
    }
    /// <summary>
    /// список откликов соискателя
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<List<ApplicationViewModel>> GetAllApplicationViewModelInEmployeeAsync(ClaimsPrincipal user)
    {
        var dataInUser = await _userManager.GetUserAsync(user);
        if (dataInUser != null)
        {
            return _mapTo.MapFromResponseApplication( await _db.ResponseApplications.Include(r => r.Vacancy)
                .ThenInclude(r => r.User)
                .Include(r => r.Vacancy).ThenInclude(r => r.Category)
                .Include(r => r.Resume)
                .ThenInclude(c => c.WorkExperiences)
                .Include(r => r.Resume).ThenInclude(c => c.Courses)
                .Include(r => r.User)
                .Where(r => r.UserId == dataInUser.Id)
                .OrderByDescending(r => r.DispatchTime)
                .ToListAsync());
        }
        throw new Exception("Произошла ошибка такого юзера нету");
    }
    public async Task<ResponseApplication?> GetByIdApplicationViewModelAsync(string? id) =>
           id != null ? await _db.ResponseApplications.FirstOrDefaultAsync(p => p.Id == id) 
               : throw new Exception("Произошла ошибка при поиске отклика");
    

}