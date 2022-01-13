using System;
using System.Linq;
using System.Threading.Tasks;

using MoiteRecepti.Data.Models;

namespace MoiteRecepti.Data.Seeding
{
    internal class SettingsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Settings.Any())
            {
                return;
            }

            await dbContext.Settings.AddAsync(new Setting { Name = "Setting1", Value = "value1" });
        }
    }
}
