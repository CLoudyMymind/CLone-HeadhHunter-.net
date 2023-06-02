using HeadHunterVer1._0.Context;
using HeadHunterVer1._0.Models;
using Microsoft.EntityFrameworkCore;

namespace HeadHunterVer1._0.Services.DataSeed
{
    // dataseed категорий для работодателя
    public static class CategorySeed
    {
        public static void CategoryDataSeed(IServiceProvider serviceProvider)
        {
            using var context = new HeadHunterContext(
                serviceProvider.GetRequiredService<DbContextOptions<HeadHunterContext>>());
            if (context.Categories.Any())
            {
                return; // если роли есть то они не создаются
            }
            // категории работы 
            var categories = new[]
            {
                new Category { Id = Guid.NewGuid().ToString(),Name = "FrontEnd developer" },
                new Category { Id = Guid.NewGuid().ToString(), Name = "Project Manager" },
                new Category {Id = Guid.NewGuid().ToString(),  Name = ".NET Developer" }
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();
        }
    }
}