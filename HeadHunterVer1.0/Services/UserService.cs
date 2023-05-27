using System.Security.Claims;
using HeadHunterVer1._0.Context;
using HeadHunterVer1._0.Models;
using HeadHunterVer1._0.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HeadHunterVer1._0.Services;

public class UserService : IUserService
{
    private readonly HeadHunterContext _headHunterContext;

    private readonly UserManager<User> _userManager;
    public UserService(HeadHunterContext headHunterContext, UserManager<User> userManager)
    {
        _headHunterContext = headHunterContext;
        _userManager = userManager;
    }

    public async Task<User?> UserSearchAsync(string? id, ClaimsPrincipal user)
    {
        return await  _headHunterContext.Users.FirstOrDefaultAsync(u => u.Id == (id ?? _userManager.GetUserId(user)));
    }
}