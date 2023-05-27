using HeadHunterVer1._0.Models;
using HeadHunterVer1._0.Services.Abstractions;
using HeadHunterVer1._0.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace HeadHunterVer1._0.Services;

public class RegisterService : IRegisterService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IUserService _userService;

    public RegisterService(UserManager<User> userManager, SignInManager<User> signInManager, IUserService userService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userService = userService;
    }

    public async Task<IdentityResult> RegisterUserAsync(RegisterViewModel model, string imgPath)
    {
        User user = new()
        {
            Email = model.Email,
            UserName = model.NameCompanyOrUser,
            PathFile = imgPath!
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded) return result;
        await _userManager.AddToRoleAsync(user, model.Role);
        await _signInManager.SignInAsync(user, false);
        return result;
    }
}