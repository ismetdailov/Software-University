using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parking
{
    public class Parking
    {
        private List<Car> data;
        private Parking()
        {
            this.data = new List<Car>();
        }
        public Parking(string type, int capacity)
            : this()
        {
            this.Capacity = capacity;
            this.Type = type;
        }
        public string Type { get; set; }
        public int Capacity { get; set; }

        public int Count => this.data.Count;
        public void Add(Car car)
        {
            if (this.data.Count + 1 <= this.Capacity)
            {
                data.Add(car);
            }
        }
        public bool Remove(string manufacturer, string model)
        {
            
            Car manu = this.data.FirstOrDefault(m => m.Manufacturer == manufacturer);
            Car mode = this.data.FirstOrDefault(m => m.Model == model);
            if (manu != null && mode!=null)
            {
                this.data.Remove(mode);
                this.data.Remove(manu);
                return true;
            }
            return false;
        }
        public Car GetLatestCar()
        {
            Car latestCar = this.data.OrderByDescending(l => l.Year).FirstOrDefault();
            if (latestCar != null)
            {
                return latestCar;

            }
            return null;
        }
        public Car GetCar(string manufacturer, string model)
        {
            Car getCarmodel = this.data.FirstOrDefault(x => x.Model == model);
            Car getCarManufacture = this.data.FirstOrDefault(x => x.Manufacturer == manufacturer);

            if (getCarmodel != null)
            {

                return getCarmodel;
            }
            return null;
        }
        public string GetStatistics()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"The cars are parked in {this.Type}:");
            foreach (var car in this.data)
            {
                sb.AppendLine(car.ToString());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
