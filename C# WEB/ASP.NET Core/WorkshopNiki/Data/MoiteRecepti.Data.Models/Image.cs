namespace MoiteRecepti.Data.Models
{
    using MoiteRecepti.Data.Common.Models;

    public class Image: BaseModel<string>
    {
        public Image()
        {
            this.Id= Guid.NewGuid().ToString();
        }

        public int RecipieId { get; set; }

        public virtual Recipie Recipie { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        public string Extension { get; set; }

        //The contents of the image is in the file system
    }
}
