using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMvcApp.Controllers
{
    public class CardsController : Controller
    {
        public HttpResponse Add(HttpRequest request)
        {
            return this.View("Views/Cards/Add.html");
        }
        public HttpResponse All(HttpRequest request)
        {
            return this.View("Views/Cards/All.html");
        }
        public HttpResponse Collection(HttpRequest request)
        {
            return this.View("Views/Cards/Collection.html");
        }
    }
}
