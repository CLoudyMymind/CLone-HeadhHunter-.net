using HeadHunterVer1._0.Models;
using HeadHunterVer1._0.Models.Employee;
using HeadHunterVer1._0.Models.Employer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HeadHunterVer1._0.Context;

public class HeadHunterContext : IdentityDbContext<User>
{
    public DbSet<Vacancy> Vacancies { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<WorkExperience> WorkExperiences { get; set; }
    public DbSet<Resume> Resumes { get; set; }

    public HeadHunterContext(DbContextOptions<HeadHunterContext> options) : base(options)
    {
    }

}
