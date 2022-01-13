namespace MoiteRecepti.Data.Models
{
    public class RecipieIngredient
    {
        public int Id { get; set; }

        public int RecipieId { get; set; }

        public virtual Recipie Recipie { get; set; }

        public int IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; }

        public string Quantity { get; set; }
    }
}
