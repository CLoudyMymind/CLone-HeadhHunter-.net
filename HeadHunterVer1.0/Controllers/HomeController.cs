using HeadHunterVer1._0.Extensions.Enums;
using HeadHunterVer1._0.Services.Abstractions;
using HeadHunterVer1._0.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeadHunterVer1._0.Controllers;
[Authorize]

public class HomeController : Controller
{
    private readonly IHomeService _homeService;
    private readonly IResponseApplicationService _applicationService;

    public HomeController(IHomeService homeService, IResponseApplicationService applicationService)
    {
        _homeService = homeService;
        _applicationService = applicationService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(SearchFilterViewModel modelFilter,
        SortState sortState = SortState.CategoryAsc, int currentPage = 1)
    {
        return View(await _homeService.FilterProd(HttpContext.User, modelFilter, sortState, currentPage));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Application(VacancyViewModel  model)
    {
        if (model.CreateApplicationViewModel != null)
        { 
            await _applicationService.Create(model);
          TempData["successfully"] = "Вы успешно отправили отклик";
          return Redirect(Request.Headers["Referer"].ToString());
        }

        throw new Exception("Произошла ошибка");
    }

}