﻿namespace MoiteRecepti.Data.Seeding
{
    using MoiteRecepti.Data.Models;

    public class CategorySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

           await dbContext.Categories.AddAsync(new Category { Name = "Тарт" });

           await dbContext.Categories.AddAsync(new Category { Name = "Кекс" });

           await dbContext.Categories.AddAsync(new Category { Name = "Печено Свинско" });

            await dbContext.SaveChangesAsync();
        }
    }
}
