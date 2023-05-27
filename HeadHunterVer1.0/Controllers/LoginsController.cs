using HeadHunterVer1._0.Models;
using HeadHunterVer1._0.Services.Abstractions;
using HeadHunterVer1._0.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HeadHunterVer1._0.Controllers;

public class LoginsController : Controller
{
    private readonly ILoginsService _logins;
    private readonly UserManager<User> _userManager;
    private readonly IFileService _fileService;
    private readonly IUserService _userService;

    public LoginsController(ILoginsService logins, UserManager<User> userManager, IFileService fileService,
        IUserService userService)
    {
        _logins = logins;
        _userManager = userManager;
        _fileService = fileService;
        _userService = userService;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string? returnUrl)
    {
        var isAuthenticated = User.Identity.IsAuthenticated;
        if (isAuthenticated) return RedirectToAction("Index", "Accounts");
        return View(new LoginViewModel
        {
            ReturnUrl = returnUrl
        });
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var isUserExists = await _logins.IsUserExistsAsync(model);
            if (isUserExists)
            {
                var passwordValid = await _logins.CheckUserPasswordAsync(model, model.Password);
                if (passwordValid)
                {
                    var signInResult = await _logins.LoginUserAsync(model);
                    if (signInResult.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                            return Redirect(model.ReturnUrl);

                        return RedirectToAction("Index", "Accounts");
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "Неверный логин или пароль");
        }
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogOff()
    {
        await _logins.SignOutUserAsync();
        return RedirectToAction("Login");
    }
}