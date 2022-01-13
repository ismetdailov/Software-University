namespace MoiteRecepti.Data.Models
{
    using MoiteRecepti.Data.Common.Models;

    public class Recipie : BaseDeletableModel<int>
    {
        public Recipie()
        {
            this.Ingredients = new HashSet<RecipieIngredient>();
            this.Images = new HashSet<Image>();
        }

        public string Name { get; set; }

        public string Instruction { get; set; }

        public TimeSpan PreparationTime { get; set; }

        public TimeSpan CookingTime { get; set; }

        public int PortionCount { get; set; }

        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<RecipieIngredient> Ingredients { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
