using System.Security.Claims;
using HeadHunterVer1._0.Models.Employer;
using HeadHunterVer1._0.ViewModels;

namespace HeadHunterVer1._0.Services.Abstractions;

public interface IEmployerService
{
    Task<List<VacancyViewModel>> GetListVacancyAsync();
    public Task CreateVacancyAsync(VacancyJobsCreateViewModel model, ClaimsPrincipal user);
    Task EditVacancyAsync(EditVacancyViewModel model , ClaimsPrincipal user );
    Task<EditVacancyViewModel>   EditVacancyAsync(string id);
    Task<List<VacancyViewModel>> GetALlVacancyInUserAsync(ClaimsPrincipal user);
    Task<VacancyViewModel> AboutVacancyAsync(string id);
    Task UpdateDateAsync(string id);
    public  Task UpdatePublishStatusAsync(string id, bool isPublished);
    
    public Task<bool> DeleteVacancyAsync(string id);


}