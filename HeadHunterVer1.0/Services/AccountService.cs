using System.Security.Claims;
using HeadHunterVer1._0.Models;
using HeadHunterVer1._0.Services.Abstractions;
using HeadHunterVer1._0.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace HeadHunterVer1._0.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IUserService _userService;

    public AccountService(UserManager<User> userManager, IUserService userService, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _userService = userService;
        _signInManager = signInManager;
    }
    
    public async Task<AboutViewModel> AboutProfileAsync(string id, ClaimsPrincipal user)
    {
        var userData = await _userService.UserSearchAsync(id, user);
        var model = new AboutViewModel
        {
        PathFile = userData.PathFile,
        Email = userData.Email,
        NameCompanyOrUser = userData.UserName,
        };
        return  model;
    }
}