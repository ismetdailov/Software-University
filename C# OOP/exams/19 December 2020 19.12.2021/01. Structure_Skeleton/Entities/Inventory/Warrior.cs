using System;
using System.Collections.Generic;
using System.Text;
using WarCroft.Entities.Characters.Contracts;

namespace WarCroft.Entities.Inventory
{
    public class Warrior : IAttacker
    {
        private string name;
        public Warrior(string name)
        {
            this.Name = name;
        }
        public string Name
        {
            get { return name; }
            set 
            {
                name = value;
            }
        }
        public void Attack(Character character)
        {

            throw new NotImplementedException();
        }
    }
}
