namespace MoiteRecepti.Web.ViewModels.RecipieViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using MoiteRecepti.Web.ViewModels.Recipie;

    public class CreateRecipieInputModel
    {

        [Required]

        [MinLength(4)]
        public string Name { get; set; }

        [Required]
        [MinLength(100)]
        public string Instruction { get; set; }

        [Range(0, 24 * 60)]
        [Display(Name ="Preparation time (in minutes)")]
        public int PreparationTime { get; set; }
        [Range(0, 24 * 60)]
        [Display(Name = "Cooking time (in minutes)")]

        public int CookingTime { get; set; }

        [Range(1, 100)]
        public int PortionCount { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<RecipieIngredientInputModel> Ingredients { get; set; }

        public IEnumerable<KeyValuePair<string,string>> CategoriesItems  { get; set; }
    }
}
