﻿using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMvcApp.Controllers
{
    public class UsersControllers : Controller
    {
        public HttpResponse Login(HttpRequest request)
        {
            return this.View("Views/Users/Login.html");
            //var responseHtml = File.ReadAllText("Views/Users/Login.html");
            //var resposneBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            //var response = new HttpResponse("text/html", resposneBodyBytes);
            //return response;
        }
        public HttpResponse Register(HttpRequest request)
        {
            return this.View("Views/Users/Register.html");

            //var responseHtml = File.ReadAllText("Views/Users/Register.html");
            //var resposneBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            //var response = new HttpResponse("text/html", resposneBodyBytes);
            //return response;
        }
    }
}
