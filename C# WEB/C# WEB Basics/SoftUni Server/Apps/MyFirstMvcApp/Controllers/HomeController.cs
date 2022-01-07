using MyFirstMvcApp.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMvcApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public HttpResponse Index()
        {
            var viewModel = new IndexViewModel();
            viewModel.CurrentYear = DateTime.UtcNow.Year;
            viewModel.Message = "Welcome to Battle Cards";
            if (this.Request.Session.ContainsKey("about"))
            {
                viewModel.Message+= "You Were on the About Page";
            }
            return this.View(viewModel);
        }
        public HttpResponse About()
        {
            this.Request.Session["about"] = "Yes";
            return this.View();
        }
    }
}
