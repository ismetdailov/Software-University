using System;
using System.Collections.Generic;
using System.Text;

namespace SoftJail.Data.Models
{
  public  class Department
    {
        public Department()
        {
            this.Cells = new HashSet<Cell>();
        }
        public int Id { get; set; }
        [Rerquired]
        public string Name { get; set; }
        public ICollection<Cell> Cells { get; set; }
    }
}
