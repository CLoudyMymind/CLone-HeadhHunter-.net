using HeadHunterVer1._0.Models;
using HeadHunterVer1._0.Services.Abstractions;
using HeadHunterVer1._0.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HeadHunterVer1._0.Controllers;

public class RegistersController : Controller
{
    private readonly IRegisterService _registerService;
    private readonly UserManager<User> _userManager;
    private readonly IFileService _fileService;
    private readonly IUserService _userService;

    public RegistersController(UserManager<User> userManager, IFileService fileService, IUserService userService,
        IRegisterService registerService)
    {
        _userManager = userManager;
        _fileService = fileService;
        _userService = userService;
        _registerService = registerService;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        var isAuthenticated = User.Identity.IsAuthenticated;
        if (isAuthenticated)
            return RedirectToAction("Login", "Logins");
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);
        var imageUrl = Url.Content($"/images/{await _fileService.FileRegisterCheckAsync(model)}");
        var result = await _registerService.RegisterUserAsync(model, imageUrl);
        if (result.Succeeded)
        {
            TempData["successfully"] =
                $"Вы зарегистрировались в приложение. Добро пожаловать, {model.NameCompanyOrUser}";
            return RedirectToAction("index", "Accounts");
        }

        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error.Description);
        return View(model);
    }
}