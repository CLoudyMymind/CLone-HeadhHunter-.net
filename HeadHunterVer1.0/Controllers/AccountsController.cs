using HeadHunterVer1._0.Filters;
using HeadHunterVer1._0.Services.Abstractions;
using HeadHunterVer1._0.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeadHunterVer1._0.Controllers;
[CustomExceptionFilter]
[Authorize]
public class AccountsController : Controller
{
    private readonly IAccountService _accountService;
    private readonly IFileService _fileService;

    public AccountsController(IAccountService accountService, IFileService fileService)
    {
        _accountService = accountService;
        _fileService = fileService;
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProfile(EditAccountProfileViewModels model, string id, string? imageUrl)
    {
        try
        {
            if (ModelState.IsValid)
            {
                imageUrl = model.AvatarFile != null ? Url.Content($"/images/{await _fileService.FileEditAsync(model)}") : imageUrl;
                var checkIdentityResult = await _accountService.Edit(model, id, HttpContext.User, imageUrl);
                if (!checkIdentityResult.Succeeded)
                {
                    foreach (var error in checkIdentityResult.Errors)
                        TempData["Error"] = error.Description;
                    return Redirect(Request.Headers["Referer"].ToString());
                }
            }
            else
            {
                foreach (var modelStateValue in ModelState.Values)
                foreach (var error in modelStateValue.Errors)
                    TempData["Error"] = error.ErrorMessage;
                return Redirect(Request.Headers["Referer"].ToString());
            }
            var role = HttpContext.User.IsInRole("employer") ? "Employer" : HttpContext.User.IsInRole("employee") ? "Employee" : throw new Exception("произошла ошибка при изменение данных");
            return RedirectToAction("AboutProfile", role, new { Id = id });
        }
        catch (Exception e)
        {
            throw new Exception($"Произошла ошибка при изменение данных {e}");
        }
    }


    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string? returnUrl)
    {
        var isAuthenticated = User.Identity.IsAuthenticated;
        if (isAuthenticated) return RedirectToAction("Index", "Home");
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

                        return RedirectToAction("Index", "Home");
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
            return RedirectToAction("index", "Home");
        }
        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error.Description);
        return View(model);
    }
}