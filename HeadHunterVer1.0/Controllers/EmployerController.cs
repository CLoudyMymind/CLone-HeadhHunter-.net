using HeadHunterVer1._0.Extensions;
using HeadHunterVer1._0.Filters;
using HeadHunterVer1._0.Services;
using HeadHunterVer1._0.Services.Abstractions;
using HeadHunterVer1._0.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeadHunterVer1._0.Controllers;

/// <summary>
/// Вся логика действий с работодателем
/// </summary>
///

public class EmployerController : Controller
{
    private readonly IEmployerService _employerService;
    private readonly IAccountService _accountService;
    private readonly MapTo _mapTo;
    private readonly ICategoryService _categoryService;

    [HttpGet]
    public async Task<IActionResult> AboutProfile(string id)
    {
        if (HttpContext.User.IsInRole("employer"))
            return View(await _accountService.AboutProfileAsync(id , HttpContext.User));
        return NotFound();
    }
    public EmployerController(IEmployerService employerService, IAccountService accountService, MapTo mapTo, ICategoryService categoryService)
    {
        _employerService = employerService;
        _accountService = accountService;
        _mapTo = mapTo;
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Create(string id) => 
        View(  _mapTo.MapToCategoryViewModel( await _categoryService.GetAllCategoryListAsync()));
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(VacancyJobsCreateViewModel model)
    {
        await _employerService.CreateVacancyAsync(model, HttpContext.User);
        return RedirectToAction("AboutProfile");
    }
    
    [HttpGet]
    public async Task<IActionResult> AboutVacancy(string id)
    {
        if (HttpContext.User.IsInRole("employer"))
            return View(await _employerService.AboutVacancy(id, HttpContext.User));
        return NotFound();
    }
    [HttpGet]
    public async Task<IActionResult> UpdateVacancyDatePublished(string id)
    {
        if (!HttpContext.User.IsInRole("employer")) return NotFound();
        await _employerService.UpdateDate(id, HttpContext.User);
        return RedirectToAction("AboutProfile");

    }
    [HttpGet]
    public async Task<IActionResult> UnPublish(string id)
    {
        if (!HttpContext.User.IsInRole("employer")) return NotFound();
        await _employerService.UpdatePublishStatus(id, HttpContext.User, false);
        return RedirectToAction("AboutProfile");

    }
    [HttpGet]
    public async Task<IActionResult> Publish(string id)
    {
        if (!HttpContext.User.IsInRole("employer")) return NotFound();
        await _employerService.UpdatePublishStatus(id, HttpContext.User,true);
        return RedirectToAction("AboutProfile");

    }
}