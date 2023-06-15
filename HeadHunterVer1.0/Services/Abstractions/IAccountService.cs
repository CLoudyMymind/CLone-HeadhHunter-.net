using System.Security.Claims;
using HeadHunterVer1._0.Models;
using HeadHunterVer1._0.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace HeadHunterVer1._0.Services.Abstractions;

public interface IAccountService
{
    Task<AboutViewModel> AboutProfileAsync(string id, ClaimsPrincipal user, string? check);
    Task<IdentityResult> Edit(EditAccountProfileViewModels model, string id, ClaimsPrincipal user, string? image);
    
    public  Task<SignInResult> LoginUserAsync(LoginViewModel model);
    public Task SignOutUserAsync();
    public Task<bool> IsUserExistsAsync(LoginViewModel email);
    public Task<bool> CheckUserPasswordAsync(LoginViewModel model, string password);
    
    public Task<IdentityResult> RegisterUserAsync(RegisterViewModel model, string ImgPath);

}