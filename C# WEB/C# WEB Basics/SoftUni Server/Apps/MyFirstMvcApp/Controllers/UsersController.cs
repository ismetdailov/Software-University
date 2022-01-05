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
        public HttpResponse Login(HttpRequest request)
        {
            return this.View();
            //var responseHtml = File.ReadAllText("Views/Users/Login.html");
            //var resposneBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            //var response = new HttpResponse("text/html", resposneBodyBytes);
            //return response;
        }
            // [HttpPost]
        public HttpResponse Register(HttpRequest request)
        {
            return this.View();

            //var responseHtml = File.ReadAllText("Views/Users/Register.html");
            //var resposneBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            //var response = new HttpResponse("text/html", resposneBodyBytes);
            //return response;
        }

        [HttpPost]
        public HttpResponse DoLogin(HttpRequest request)
        {
            //Todo read data
            //check user
            //log user
            return this.Redirect("/");
        }
    }
}
