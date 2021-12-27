using AquaShop.Models.Decorations.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Core
{
    public abstract class Decoration : IDecoration
    {
        //private int comfort;
        ///private decimal price;

        public Decoration(int comfort, decimal price)
        {
            this.Comfort = comfort;
            this.Price = price;


        }

        public int Comfort { get; }
        //{
        //    get
        //    {
        //    }
        //    //private set
        //    //{
        //    //    this.comfort = value;
        //    //}
        //}

       public decimal Price { get; }
        //{
        //    get
        //    {
        //        return this.price;
        //    }
        //   //private set 
        //   // {
        //   //    this.price = value; 
        //   // }
        //}

    }
}
