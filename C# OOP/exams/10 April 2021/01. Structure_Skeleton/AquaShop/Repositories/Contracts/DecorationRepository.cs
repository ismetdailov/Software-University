using AquaShop.Models.Decorations.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Repositories.Contracts
{
    public class DecorationRepository : IRepository<IDecoration>
    {
        private readonly List<IDecoration> decoration;
        public DecorationRepository()
        {
            this.decoration = new List<IDecoration>();
        }
        public IReadOnlyCollection<IDecoration> Models => this.decoration.AsReadOnly();

        public void Add(IDecoration model) => decoration.Add(model);

        public IDecoration FindByType(string type) => decoration.FirstOrDefault(x => x.GetType().Name == type);

        public bool Remove(IDecoration model)
        => decoration.Remove(model);
    }
}
