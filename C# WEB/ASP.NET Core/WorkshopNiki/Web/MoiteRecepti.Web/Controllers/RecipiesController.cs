namespace MoiteRecepti.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MoiteRecepti.Services.Data;
    using MoiteRecepti.Web.ViewModels.RecipieViewModels;

    public class RecipiesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IRecipesService recipesService;

        public RecipiesController(
            ICategoriesService categoriesService,
            IRecipesService recipesService)
        {
            this.categoriesService = categoriesService;
            this.recipesService = recipesService;
        }

        public IActionResult Create()
        {
            var viewModel = new CreateRecipieInputModel();
            viewModel.CategoriesItems =this.categoriesService.GetAllAsKeyValuePairs();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipieInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            await this.recipesService.CreateAsync(input);

            // Todo: Redirect info page
            return this.Redirect("/");
        }
    }
}
