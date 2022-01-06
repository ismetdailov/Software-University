using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMvcApp.Controllers
{
    public class UsersController : Controller
    {
        public HttpResponse Login()
        {
            return this.View();
        }
            // [HttpPost]
        public HttpResponse Register()
        {
            return this.View();

        }

        [HttpPost]
        public HttpResponse DoLogin()
        {
            //Todo read data
            //check user
            //log user
            return this.Redirect("/");
        }
    }
}
