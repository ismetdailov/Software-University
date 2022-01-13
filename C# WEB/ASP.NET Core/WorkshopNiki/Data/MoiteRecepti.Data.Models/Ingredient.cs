namespace MoiteRecepti.Data.Models
{
    using MoiteRecepti.Data.Common.Models;

    public class Ingredient : BaseDeletableModel<int>
    {
        public Ingredient()
        {
            this.Recipies = new HashSet<RecipieIngredient>();
        }

        public string Name { get; set; }

        public virtual ICollection<RecipieIngredient> Recipies { get; set; }
    }
}
