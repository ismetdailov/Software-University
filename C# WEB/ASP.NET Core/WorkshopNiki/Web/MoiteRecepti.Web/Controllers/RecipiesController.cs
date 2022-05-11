namespace MoiteRecepti.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MoiteRecepti.Services.Data;
    using MoiteRecepti.Web.ViewModels.RecipieViewModels;

    public class RecipiesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public RecipiesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Create()
        {
            var viewModel = new CreateRecipieInputModel();
            viewModel.CategoriesItems =this.categoriesService.GetAllAsKeyValuePairs();
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateRecipieInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }
            //Todo: Redirect info page
            return this.Redirect("/");
        }
    }
}
