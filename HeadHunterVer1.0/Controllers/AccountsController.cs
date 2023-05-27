using System.Net.Mime;
using System.Security.Claims;
using HeadHunterVer1._0.Models;
using HeadHunterVer1._0.Services.Abstractions;
using HeadHunterVer1._0.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace HeadHunterVer1._0.Controllers;

[Authorize]
public class AccountsController : Controller
{
    private readonly IAccountService _accountService;
    private readonly UserManager<User> _userManager;
    private readonly IFileService _fileService;
    private readonly IUserService _userService;

    public AccountsController(IAccountService accountService, UserManager<User> userManager, IFileService fileService,
        IUserService userService)
    {
        _accountService = accountService;
        _userManager = userManager;
        _fileService = fileService;
        _userService = userService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> AboutProfile(string id)
    {
        
        return View(await _accountService.AboutProfileAsync(id , HttpContext.User));
    } 
}