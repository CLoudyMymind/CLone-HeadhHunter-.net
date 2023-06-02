using System.Security.Claims;
using HeadHunterVer1._0.Extensions;
using HeadHunterVer1._0.Models;
using HeadHunterVer1._0.Services.Abstractions;
using HeadHunterVer1._0.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace HeadHunterVer1._0.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly IUserService _userService;
    private readonly SignInManager<User> _signInManager;
    private readonly AccountExtensions _accountExtensions;

    public AccountService(UserManager<User> userManager, IUserService userService, AccountExtensions accountExtensions, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _userService = userService;
        _accountExtensions = accountExtensions;
        _signInManager = signInManager;
    }
    public async Task<IdentityResult> Edit(EditAccountProfileViewModels model, string id, ClaimsPrincipal user, string? imagePath)
    {
        var userData = await _userService.UserSearchAsync(id, user);
        if (userData is null)
            throw new Exception("Произошла ошибка обратите в поддержку)");
        userData.UserName = !string.IsNullOrEmpty(model.NameCompanyOrUser) ? model.NameCompanyOrUser : userData.UserName;
        userData.NormalizedUserName = !string.IsNullOrEmpty(model.NameCompanyOrUser) ? model.NameCompanyOrUser.ToUpper() : userData.NormalizedUserName;
        userData.Email = !string.IsNullOrEmpty(model.Email) ? model.Email : userData.Email;
        userData.NormalizedEmail = !string.IsNullOrEmpty(model.Email) ? model.Email.ToUpper() : userData.NormalizedEmail;
        userData.PathFile = imagePath ?? userData.PathFile;
        if (string.IsNullOrEmpty(model.OldPassword) && string.IsNullOrEmpty(model.NewPassword))
            return await _userManager.UpdateAsync(userData);
        var changePasswordResult = await _userManager.ChangePasswordAsync(userData, model.OldPassword, model.NewPassword);
        return !changePasswordResult.Succeeded ? changePasswordResult : await _userManager.UpdateAsync(userData);
    }
    public async Task<AboutViewModel> AboutProfileAsync(string id, ClaimsPrincipal user)
    {
        var userData = await _userService.UserSearchAsync(id, user);
        if (userData != null)
            return  await _accountExtensions.CreateAboutViewModelExtensions(userData, userData.PathFile, userData);
        throw new Exception("Произошла ошибка обратите в поддержку)");
    }
    
    public async Task<SignInResult> LoginUserAsync(LoginViewModel model)
    {
        if (model.Name != null)
        {
            var user = await _userManager.FindByNameAsync(model.Name) ?? await _userManager.FindByEmailAsync(model.Name);
            if (user == null) throw new Exception("Такого пользователя нету");
            var signInResult = await _signInManager.PasswordSignInAsync(
                user,
                model.Password,
                model.RememberMe,
                false
            );
            return signInResult;
        }
        throw new Exception("Такого пользователя нету");
    }

    public async Task SignOutUserAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public  async Task<bool> IsUserExistsAsync(LoginViewModel model)
    {
        if (model.Name is null) return false;
        var user = await _userManager.FindByNameAsync(model.Name) ?? await _userManager.FindByEmailAsync(model.Name);
        return user != null;
    }

    public async Task<bool> CheckUserPasswordAsync(LoginViewModel model, string password)
    {
        if (model.Name == null) return false;
        var user = await _userManager.FindByNameAsync(model.Name) ?? await _userManager.FindByEmailAsync(model.Name);
        if (user == null) return false;
        var passwordValid = await _userManager.CheckPasswordAsync(user, password);
        return passwordValid;
    }
    public async Task<IdentityResult> RegisterUserAsync(RegisterViewModel model, string imgPath)
    {
        var user = _accountExtensions.UserModelExtensions(model, imgPath);
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded) return result;
        await _userManager.AddToRoleAsync(user, model.Role);
        await _signInManager.SignInAsync(user, false);
        return result;
    }
}