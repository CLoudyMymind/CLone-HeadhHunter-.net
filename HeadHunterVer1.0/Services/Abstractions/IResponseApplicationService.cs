using System.Security.Claims;
using HeadHunterVer1._0.Models;
using HeadHunterVer1._0.ViewModels;

namespace HeadHunterVer1._0.Services.Abstractions;

public interface IResponseApplicationService
{
    public Task<bool> Create(VacancyViewModel model);

    Task<ResponseApplication?> GetByIdApplicationViewModelAsync(string? id);
    Task<List<ApplicationViewModel>> GetAllApplicationViewModelInEmployerAsync(ClaimsPrincipal user);
    Task<List<ApplicationViewModel>> GetAllApplicationViewModelInEmployeeAsync(ClaimsPrincipal user);
}