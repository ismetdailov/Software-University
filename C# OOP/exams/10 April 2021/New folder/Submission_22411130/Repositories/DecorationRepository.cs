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
        private readonly List<IDecoration> decoration;
        public DecorationRepository()
        {
            decoration = new List<IDecoration>();
        }

        public IReadOnlyCollection<IDecoration> Models => decoration.AsReadOnly();

        //  IReadOnlyCollection<Decoration> IRepository<Decoration>.Models => throw new NotImplementedException();

        public void Add(IDecoration model) => decoration.Add(model);



        public IDecoration FindByType(string type) => decoration.FirstOrDefault(x => x.GetType().Name == type);


        public bool Remove(IDecoration model) => decoration.Remove(model);
     

       

        
    }
}
