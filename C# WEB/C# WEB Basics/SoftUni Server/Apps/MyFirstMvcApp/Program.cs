﻿using SUS.HTTP;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMvcApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IHttpServer server = new HttpServer();
            server.AddRoute("/", HomePage);
            server.AddRoute("/favicon.ico", Favicon);
            server.AddRoute("/about", About);
            server.AddRoute("/users/login", Login);
            await server.StartAsync(80);
        }
        static HttpResponse HomePage(HttpRequest request)
        {
            var responseHtml = "<h1>Welcome!</>" + HttpConstans.NewLine +
                request.Headers.FirstOrDefault(x => x.Name == "User-Agent");
            var resposneBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            var response = new HttpResponse("text/html", resposneBodyBytes);
           
            return response;
        }
        static HttpResponse Favicon(HttpRequest request)
        {
            var fileBytes = File.ReadAllBytes("wwwroot/fav.svg");
            var response = new HttpResponse("image/svg+xml",fileBytes);
            ///var response = new HttpResponse("image/vnd.microsoft.icon",fileBytes);
            return response;
        }
        static HttpResponse About(HttpRequest request)
        {
            var responseHtml = "<h1>About.........!</>";
            var resposneBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            var response = new HttpResponse("text/html", resposneBodyBytes);
            return response;
        }
        static HttpResponse Login(HttpRequest request)
        {
            var responseHtml = "<h1>Login...!</>";
            var resposneBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            var response = new HttpResponse("text/html", resposneBodyBytes);
            return response;
        }
    }
}