using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS.MvcFramework.Tests
{
    //Dependensi container
  public interface IServiceCollection
    {
        void Add<TSource, TDestrination>();
    }
}
