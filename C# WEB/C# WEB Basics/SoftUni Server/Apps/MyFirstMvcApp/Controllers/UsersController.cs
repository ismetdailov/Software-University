﻿using MyFirstMvcApp.Servises;
using SUS.HTTP;
using SUS.MvcFramework;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MyFirstMvcApp.Controllers
{
    public class UsersController : Controller
    {
        //otgovornost na kontrolera e da proverqva validaciite
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }
        public HttpResponse Login()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }
        [HttpPost]
        public HttpResponse Login(string username,string password) 
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            //var username = this.Request.FormData["username"];
            //var password = this.Request.FormData["password"];
            var userId = this.usersService.GetUserId(username, password);
            if (userId ==null)
            {
                return this.Error("Invalid username or password");
            }
            this.SignIn(userId);
            return this.Redirect("/Cards/All");

        }
        public HttpResponse Register()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            return this.View();

        }

        [HttpPost]
        public HttpResponse Register(string username, string email, string password, string confirmPassword)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            //var username = this.Request.FormData["username"];
            //var email = this.Request.FormData["email"];
            //var password = this.Request.FormData["password"];
            //var confirmpassword = this.Request.FormData["confirmPassword"];
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
            if (password != confirmPassword)
            {
                return this.Error("Passwords should be the same");
            }
            if (!this.usersService.IsUsernameAvialable(username))
            {
                return this.Error("Username already taken.");
            }
            if (!this.usersService.IsEmailAvialable(email))
            {
                return this.Error("Email already taken");
            }
         this.usersService.CreateUser(username, email, password);


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
