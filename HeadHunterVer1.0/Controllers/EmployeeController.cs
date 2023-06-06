using HeadHunterVer1._0.Models;
using HeadHunterVer1._0.Services.Abstractions;
using HeadHunterVer1._0.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HeadHunterVer1._0.Controllers;
/// <summary>
/// Вся логика работ с  Соискателем(работником)
/// </summary>
public class EmployeeController : Controller
{
    private readonly IAccountService _accountService;
    private readonly ICategoryService _categoryService;
    private readonly IEmployeeService _employeeService;
    private readonly UserManager<User> _userManager;

    public EmployeeController(
        IAccountService accountService, 
        ICategoryService categoryService, 
        IEmployeeService employeeService, UserManager<User> userManager)
    {
        _accountService = accountService;
        _categoryService = categoryService;
        _employeeService = employeeService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> AboutProfile(string id)
    {
        if (HttpContext.User.IsInRole("employee"))
            return View(await _accountService.AboutProfileAsync(id , HttpContext.User));
        return NotFound();
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> CreateResume()
    {
        return View(new CreateResumeViewModel { CategoryViewModels = await _categoryService.GetAllCategoryListAsync() });
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateResume(CreateResumeViewModel viewModel)
    {
        await _employeeService.CreateResumeAsync(viewModel, User);
        var currentUser = await _userManager.GetUserAsync(User);
        return RedirectToAction("AboutProfile", new {id = currentUser.Id});
    }
}