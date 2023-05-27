using HeadHunterVer1._0.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace HeadHunterVer1._0.Services.Abstractions;

public interface ILoginsService
{
    public  Task<SignInResult> LoginUserAsync(LoginViewModel model);
    public Task SignOutUserAsync();
    public Task<bool> IsUserExistsAsync(LoginViewModel email);
    public Task<bool> CheckUserPasswordAsync(LoginViewModel model, string password);
}