using System.Security.Claims;
using HeadHunterVer1._0.Models.Employer;
using HeadHunterVer1._0.ViewModels;

namespace HeadHunterVer1._0.Services.Abstractions;

public interface IEmployerService
{
    public Task CreateVacancyAsync(VacancyJobsCreateViewModel model, ClaimsPrincipal user);
    Task<List<VacancyViewModel>> GetALlVacancyAsync(ClaimsPrincipal user);
    Task<VacancyViewModel> AboutVacancy(string id, ClaimsPrincipal user);
    public Task<Vacancy> GetByIdVacancy(string id , ClaimsPrincipal user);
    Task UpdateDate(string id, ClaimsPrincipal user);
    public  Task UpdatePublishStatus(string id, ClaimsPrincipal user, bool isPublished);

}