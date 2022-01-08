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
        [HttpPost("/User/Login")]
        public HttpResponse DoLogin()
        {
            return this.Redirect("/");

        }
        public HttpResponse Register()
        {
            return this.View();

        }

        [HttpPost("/Users/Register")]
        public HttpResponse DoRegister()
        {
            //Todo read data
            //check user
            //log user
            return this.Redirect("/");
        }
        public HttpResponse Logout()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("Only logget-in users can logout");
            }
            this.SignOut();
            return this.Redirect("/");
        }
    }
}
