using MoiteRecepti.Web.ViewModels.RecipieViewModels;

namespace MoiteRecepti.Services.Data
{
    public interface IRecipesService
    {
        Task CreateAsync(CreateRecipieInputModel input);
    }
}
