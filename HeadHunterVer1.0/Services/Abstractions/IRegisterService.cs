using HeadHunterVer1._0.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace HeadHunterVer1._0.Services.Abstractions;

public interface IRegisterService
{
    public Task<IdentityResult> RegisterUserAsync(RegisterViewModel model, string ImgPath);

}