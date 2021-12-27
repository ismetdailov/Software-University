
using AquaShop.Models.Fish;
using AquaShop.Models.Fish.Contracts;

public class FreshwaterFish : Fish
{
    public FreshwaterFish(string name, string species, decimal price) : base(name, species, price)
    {
        Size = 3;
    }
   public override void Eat()
    {
        this.Size += 3;
    }
}

