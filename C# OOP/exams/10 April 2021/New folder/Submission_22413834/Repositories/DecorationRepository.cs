using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Repositories
{
   public class DecorationRepository :IRepository<IDecoration>
    {
        private readonly List<IDecoration> decorations;
        public DecorationRepository()
        {
           this.decorations = new List<IDecoration>();
        }

        public IReadOnlyCollection<IDecoration> Models => this.decorations;

        //  IReadOnlyCollection<Decoration> IRepository<Decoration>.Models => throw new NotImplementedException();

        public void Add(IDecoration decoration) => this.decorations.Add(decoration);



        public IDecoration FindByType(string type) => this.decorations.FirstOrDefault(x=>x.Equals(type));


        public bool Remove(IDecoration decoration) => decorations.Remove(decoration);
     

       

        
    }
}
