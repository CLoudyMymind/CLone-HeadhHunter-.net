using System.Security.Claims;
using HeadHunterVer1._0.ViewModels;

namespace HeadHunterVer1._0.Services.Abstractions;

public interface IEmployeeService
{
    Task CreateResume(CreateResumeViewModel viewModel, ClaimsPrincipal user);
}