using System.Net.Mime;
using System.Security.Claims;
using HeadHunterVer1._0.Extensions;
using HeadHunterVer1._0.Filters;
using HeadHunterVer1._0.Models;
using HeadHunterVer1._0.Services.Abstractions;
using HeadHunterVer1._0.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace HeadHunterVer1._0.Controllers;
[CustomExceptionFilter]
[Authorize]
public class AccountsController : Controller
{
    private readonly IAccountService _accountService;
    private readonly IFileService _fileService;
    private readonly IUserService _userService;
    private readonly AccountExtensions _accountExtensions;

    public AccountsController(IAccountService accountService,  IFileService fileService,
        IUserService userService, AccountExtensions accountExtensions)
    {
        _accountService = accountService;
        _fileService = fileService;
        _userService = userService;
        _accountExtensions = accountExtensions;
    }

    public IActionResult Index()
    {
        return View();
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
            var isUserExists = await _accountService.IsUserExistsAsync(model);
            if (isUserExists)
            {
                var passwordValid = await _accountService.CheckUserPasswordAsync(model, model.Password);
                if (passwordValid)
                {
                    var signInResult = await _accountService.LoginUserAsync(model);
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
        await _accountService.SignOutUserAsync();
        return RedirectToAction("Login");
    }
    
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        if (User.Identity!.IsAuthenticated)
            return RedirectToAction("Login");
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
        var result = await _accountService.RegisterUserAsync(model, imageUrl);
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