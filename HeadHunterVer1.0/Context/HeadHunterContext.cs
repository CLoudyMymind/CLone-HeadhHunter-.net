using HeadHunterVer1._0.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HeadHunterVer1._0.Context;

public class HeadHunterContext : IdentityDbContext<User>
{
    

    public HeadHunterContext(DbContextOptions<HeadHunterContext> options) : base(options){}

}
