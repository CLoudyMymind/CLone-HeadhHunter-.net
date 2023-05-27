using HeadHunterVer1._0.Models;
using Microsoft.AspNetCore.Identity;

namespace HeadHunterVer1._0.Services.DataSeed;

public static class RoleInitial
{
    /// <summary>
    /// создание ролей в бд 
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="roleManager"></param>
    public static async Task SeedRoleAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        //employee == Работник || employer == Работодатель 
        var roles = new string[] { "employer", "employee" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

}