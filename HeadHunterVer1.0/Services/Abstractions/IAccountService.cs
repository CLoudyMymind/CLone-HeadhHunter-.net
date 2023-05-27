using System.Security.Claims;
using HeadHunterVer1._0.Models;
using HeadHunterVer1._0.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace HeadHunterVer1._0.Services.Abstractions;

public interface IAccountService
{
    Task<AboutViewModel> AboutProfileAsync(string id, ClaimsPrincipal user);
 }