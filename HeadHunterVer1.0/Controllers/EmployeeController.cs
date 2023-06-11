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
    private readonly IFileService _fileService;

    public EmployeeController(
        IAccountService accountService, 
        ICategoryService categoryService, 
        IEmployeeService employeeService, 
        UserManager<User> userManager, 
        IFileService fileService)
    {
        _accountService = accountService;
        _categoryService = categoryService;
        _employeeService = employeeService;
        _userManager = userManager;
        _fileService = fileService;
    }
    
    [Authorize(Roles = "employee")]
    [HttpGet]
    public async Task<IActionResult> AboutProfile(string id)
    {
        if (HttpContext.User.IsInRole("employee"))
            return View(await _accountService.AboutProfileAsync(id , HttpContext.User));
        return NotFound();
    }
    
    [Authorize(Roles = "employee")]
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> CreateResume()
    {
        return View(new CreateResumeViewModel { CategoryViewModels = await _categoryService.GetAllCategoryViewModelListAsync() });
    }
    
    [Authorize(Roles = "employee")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateResume(CreateResumeViewModel viewModel)
    {
        await _employeeService.CreateResumeAsync(viewModel, User);
        var currentUser = await _userManager.GetUserAsync(User);
        return RedirectToAction("AboutProfile", new {id = currentUser.Id});
    }

    [HttpGet]
    [Authorize(Roles = "employer,employee")]
    public async Task<IActionResult> AboutResume(int id)
    {
        var data = await _employeeService.GetAllResume();
        if (data.Any( r => r.Id == id))
        {
            return View(data.FirstOrDefault(r => r.Id == id));
        }

        return NotFound();
    }
    
    [HttpGet]
    public async Task<IActionResult> DownloadPdf(int id)
    {
        try
        {
            return File(await _fileService.GeneratePdfAsync(id, HttpContext.User),
                _fileService.ContentTypeFile(), _fileService.GeneratePdfFileName(id));
        }
        catch (Exception e)
        {
            throw new Exception("Произошла ошибка при скачивание файла");
        }
    }

    // [HttpGet]
    // public IActionResult EditResume(int id)
    // {
    //     return View();
    // }
}