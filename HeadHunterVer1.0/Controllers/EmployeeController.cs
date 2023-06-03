using HeadHunterVer1._0.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace HeadHunterVer1._0.Controllers;
/// <summary>
/// Вся логика работ с  Соискателем(работником)
/// </summary>
public class EmployeeController : Controller
{
    private readonly IAccountService _accountService;

    public EmployeeController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<IActionResult> AboutProfile(string id)
    {
        if (HttpContext.User.IsInRole("employee"))
            return View(await _accountService.AboutProfileAsync(id , HttpContext.User));
        return NotFound();
    }
}