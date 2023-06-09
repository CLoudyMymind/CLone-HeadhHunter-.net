using HeadHunterVer1._0.Extensions;
using HeadHunterVer1._0.Services.Abstractions;
using HeadHunterVer1._0.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeadHunterVer1._0.Controllers;

/// <summary>
///     Вся логика действий с работодателем
/// </summary>
public class EmployerController : Controller
{
    
    private readonly IEmployerService _employerService;
    private readonly IAccountService _accountService;
    private readonly MapTo _mapTo;
    private readonly ICategoryService _categoryService;


    public EmployerController(IEmployerService employerService, IAccountService accountService, MapTo mapTo,
        ICategoryService categoryService)
    {
        _employerService = employerService;
        _accountService = accountService;
        _mapTo = mapTo;
        _categoryService = categoryService;
    }
    [Authorize(Roles = "employer")]
    [HttpGet]
    public async Task<IActionResult> AboutProfile(string id)
    {
        if (HttpContext.User.IsInRole("employer"))
            return View(await _accountService.AboutProfileAsync(id, HttpContext.User));
        return NotFound();
    }
    [Authorize(Roles = "employer")]

    [HttpGet]
    public async Task<IActionResult> Create(string id)
    {
        return View(_mapTo.MapToCategoryViewModel(await _categoryService.GetAllCategoryListAsync()));
    }
    [Authorize(Roles = "employer")]

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(VacancyJobsCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.CategoryViewModels = await _categoryService.GetAllCategoryListAsync();
            return View(model);
        }

        await _employerService.CreateVacancyAsync(model, HttpContext.User);
        return RedirectToAction("AboutProfile");
    }

    [HttpGet]
    [Authorize(Roles = "employer,employee")]
    public async Task<IActionResult> AboutVacancy(string id)
    {
        return View(await _employerService.AboutVacancyAsync(id));
    }
    [Authorize(Roles = "employer")]

    [HttpGet]
    public async Task<IActionResult> UpdateVacancyDatePublished(string id)
    {
        if (!HttpContext.User.IsInRole("employer")) return NotFound();
        await _employerService.UpdateDate(id);
        return RedirectToAction("AboutProfile");
    }
    [Authorize(Roles = "employer")]

    [HttpGet]
    public async Task<IActionResult> UnPublish(string id)
    {
        if (!HttpContext.User.IsInRole("employer")) return NotFound();
        await _employerService.UpdatePublishStatusAsync(id, false);
        return Redirect(Request.Headers["Referer"].ToString());
    }
    [Authorize(Roles = "employer")]

    [HttpGet]
    public async Task<IActionResult> Publish(string id)
    {
        if (!HttpContext.User.IsInRole("employer")) return NotFound();
        await _employerService.UpdatePublishStatusAsync(id, true);
        return Redirect(Request.Headers["Referer"].ToString());
    }
    [Authorize(Roles = "employer")]

    [HttpGet]
    public async Task<IActionResult> EditVacancy(string id)
    {
        if (!HttpContext.User.IsInRole("employer")) return NotFound();
        return View(await _employerService.EditVacancyAsync(id));
    }
    [Authorize(Roles = "employer")]

    [HttpPost]
    public async Task<IActionResult> EditVacancy(EditVacancyViewModel model)
    {
        if (!HttpContext.User.IsInRole("employer")) return NotFound();
        await _employerService.EditVacancyAsync(model, HttpContext.User);
        return RedirectToAction("AboutProfile");
    }
    [Authorize(Roles = "employer")]

    [HttpGet]
    public async Task<IActionResult> DeleteVacancy(string? id)
    {
        if (id == null && await _employerService.DeleteVacancy(id) == false)
        {
            TempData["Error"] = "Ошибка при удаление";
            return RedirectToAction("AboutProfile");
        }

        return RedirectToAction("AboutProfile");
    }
}