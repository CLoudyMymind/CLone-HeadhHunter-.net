using HeadHunterVer1._0.Models;
using HeadHunterVer1._0.Services.Abstractions;
using HeadHunterVer1._0.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace HeadHunterVer1._0.Services;

public class LoginService : ILoginsService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IUserService _userService;

    public LoginService(UserManager<User> userManager, SignInManager<User> signInManager, IUserService userService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userService = userService;
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
}