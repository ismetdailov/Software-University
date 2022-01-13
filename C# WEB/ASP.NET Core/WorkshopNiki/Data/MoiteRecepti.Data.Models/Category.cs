namespace MoiteRecepti.Data.Models
{
    using MoiteRecepti.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Recipies = new HashSet<Recipie>();
        }

        public string Name { get; set; }

        public virtual ICollection<Recipie> Recipies { get; set; }
    }
}
