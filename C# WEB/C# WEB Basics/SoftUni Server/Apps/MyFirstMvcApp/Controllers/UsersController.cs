using MyFirstMvcApp.Servises;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyFirstMvcApp.Controllers
{
    public class UsersController : Controller
    {
        //otgovornost na kontrolera e da proverqva validaciite
        private UserService userService;

        public UsersController()
        {
            this.userService = new UserService();
        }
        public HttpResponse Login()
        {
            return this.View();
        }
        [HttpPost("/Users/Login")]
        public HttpResponse DoLogin()
        {
            var username = this.Request.FormData["username"];
            var password = this.Request.FormData["password"];
            var userId = this.userService.GetUserId(username, password);
            if (userId ==null)
            {
                return this.Error("Invalid username or password");
            }
            this.SignIn(userId);
            return this.Redirect("/");

        }
        public HttpResponse Register()
        {

            return this.View();

        }

        [HttpPost("/Users/Register")]
        public HttpResponse DoRegister()
        {
            var username = this.Request.FormData["username"];
            var email = this.Request.FormData["email"];
            var password = this.Request.FormData["password"];
            var confirmpassword = this.Request.FormData["confirmPassword"];
            //tova e surver side validaciq
            if (username == null || username.Length<5|| username.Length>20)
            {
                return this.Error("Invalid usernamer. The username should be between 5 and 20 characters");
            }
            if (!Regex.IsMatch(username,@"^[a-zA-Z0-9\.]+$"))
            {
                return this.Error("Invalid usename. Only aplhanumeric charaactes");
            }
            if (string.IsNullOrWhiteSpace(email)||!new EmailAddressAttribute().IsValid(email))
            {
                return this.Error("Invalid email.");
            }
            if (password == null || password.Length <6 || password.Length>20)
            {
                return this.Error("Invalid password. The password should be between 6 and 20 symbols");

            }
            if (password != confirmpassword)
            {
                return this.Error("Passwords should be the same");
            }
            if (!this.userService.IsUsernameAvialable(username))
            {
                return this.Error("Username already taken.");
            }
            if (!this.userService.IsEmailAvialable(email))
            {
                return this.Error("Email already taken");
            }
         this.userService.CreateUser(username, email, password);


            return this.Redirect("/Users/Login");
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
