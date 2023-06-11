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
                return; 
            }
            var categories = new[]
            {
                new Category { Id = Guid.NewGuid().ToString(), Name = "Веб-дизайнер" },
                new Category { Id = Guid.NewGuid().ToString(), Name = "Архитектор" },
                new Category { Id = Guid.NewGuid().ToString(), Name = "Маркетолог" },
                new Category { Id = Guid.NewGuid().ToString(), Name = "Программист Python" },
                new Category { Id = Guid.NewGuid().ToString(), Name = "Финансовый аналитик" },
                new Category { Id = Guid.NewGuid().ToString(), Name = "Программист .net Asp Core" },
                new Category { Id = Guid.NewGuid().ToString(), Name = "Программист GoLang" },
                new Category { Id = Guid.NewGuid().ToString(), Name = "Программист Java" },
                new Category { Id = Guid.NewGuid().ToString(), Name = "Программист Php" },
                new Category { Id = Guid.NewGuid().ToString(), Name = "Инженер-строитель" },
                new Category { Id = Guid.NewGuid().ToString(), Name = "Технический писатель" },
                new Category { Id = Guid.NewGuid().ToString(), Name = "Адвокат" }
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();
        }
    }
}