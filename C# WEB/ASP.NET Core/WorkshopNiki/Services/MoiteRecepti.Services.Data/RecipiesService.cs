namespace MoiteRecepti.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MoiteRecepti.Data.Common.Repositories;
    using MoiteRecepti.Data.Models;
    using MoiteRecepti.Web.ViewModels.RecipieViewModels;

    public class RecipiesService : IRecipesService
    {
        private readonly IDeletableEntityRepository<Recipie> recipieRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientRepository;

        public RecipiesService(IDeletableEntityRepository<Recipie> recipieRepository, IDeletableEntityRepository<Ingredient> ingredientRepository)
        {
            this.recipieRepository = recipieRepository;
            this.ingredientRepository = ingredientRepository;
        }

        public async Task CreateAsync(CreateRecipieInputModel input)
        {
            var recipie = new Recipie
            {
                CategoryId = input.CategoryId,
                CookingTime = TimeSpan.FromMinutes(input.CookingTime),
                Instruction = input.Instruction,
                Name = input.Name,
                PortionCount = input.PortionCount,
                PreparationTime = TimeSpan.FromMinutes(input.PreparationTime),
            };
            foreach (var inputIngredient in input.Ingredients)
            {
                var ingredient = this.ingredientRepository.All().FirstOrDefault(x => x.Name == inputIngredient.IngredientName);
                if (ingredient == null)
                {
                    ingredient = new Ingredient { Name = inputIngredient.IngredientName };
                }

                recipie.Ingredients.Add(new RecipieIngredient
                {
                    Ingredient = ingredient,
                    Quantity = inputIngredient.Quantity,
                });
            }
           await this.recipieRepository.AddAsync(recipie);
           await this.recipieRepository.SaveChangesAsync();
        }
    }
}
