using System.Security.Claims;
using HeadHunterVer1._0.Models.Employer;
using HeadHunterVer1._0.ViewModels;

namespace HeadHunterVer1._0.Services.Abstractions;

public interface IEmployerService
{
    public Task CreateVacancyAsync(VacancyJobsCreateViewModel model, ClaimsPrincipal user);
    Task<List<VacancyViewModel>> GetALlVacancyAsync(ClaimsPrincipal user);
    Task<VacancyViewModel> AboutVacancyAsync(string id, ClaimsPrincipal user);
    public Task<Vacancy> GetByIdVacancyAsync(string id , ClaimsPrincipal user);
    Task UpdateDate(string id, ClaimsPrincipal user);
    public  Task UpdatePublishStatusAsync(string id, ClaimsPrincipal user, bool isPublished);

}