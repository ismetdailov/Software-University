using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkiRental
{
  public  class SkiRental
    {
        private List<Ski> data;
        private SkiRental()
        {
            this.data = new List<Ski>();
        }
        public SkiRental(string name, int capacity)
            :this()
        {
            this.Name = name;
            this.Capacity = capacity;
        }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int Count => this.data.Count();
        public void Add(Ski ski)
        {
            if (this.data.Count + 1 < this.Capacity)
            {
                this.data.Add(ski);
            }
        }
        public bool Remove(string manufacturer, string model)
        {
            Ski sky = this.data.FirstOrDefault(x => x.Manufacturer == manufacturer);
            if (sky != null)
            {
                this.data.Remove(sky);
                return true;

            }
            else
            {
                return false;
            }
        }
        public Ski GetNewestSki()
        {
            Ski newest = this.data.OrderByDescending(a => a.Year).FirstOrDefault();
            return newest;
        }
        public Ski GetSki(string manufacturer, string model)
        {
            Ski findSki = this.data.FirstOrDefault(e => e.Manufacturer == manufacturer);
            if (findSki!=null)
            {
                return findSki;

            }
            else
            {
                return null;
            }
        }
        public string GetStatistics()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"The skis stored in {this.Name}:");
            foreach (var item in data)
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
