using HeadHunterVer1._0.Models;
using Microsoft.AspNetCore.Identity;

namespace HeadHunterVer1._0.Services.DataSeed;

public static class RoleInitial
{
    public static async Task SeedRoleAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        //employee == Работник || employer == Работодатель 
        var roles = new [] { "employer", "employee" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

}