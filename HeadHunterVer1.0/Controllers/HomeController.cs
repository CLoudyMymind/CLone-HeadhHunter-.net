using HeadHunterVer1._0.Extensions.Enums;
using HeadHunterVer1._0.Services.Abstractions;
using HeadHunterVer1._0.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeadHunterVer1._0.Controllers;

public class HomeController : Controller
{
    private readonly IHomeService _homeService;

    public HomeController(IHomeService homeService)
    {
        _homeService = homeService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Index(SearchFilterViewModel modelFilter,
        SortState sortState = SortState.CategoryAsc, int currentPage = 1)
    {
        return View(await _homeService.FilterProd(HttpContext.User, modelFilter, sortState, currentPage));
    }
}