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
        public HttpResponse Index(HttpRequest request)
        {
            var responseHtml = "<h1>Welcome!</>" + HttpConstans.NewLine +
                request.Headers.FirstOrDefault(x => x.Name == "User-Agent");
            var resposneBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            var response = new HttpResponse("text/html", resposneBodyBytes);

            return response;
        }
        public HttpResponse About(HttpRequest request)
        {
            var responseHtml = "<h1>About.........!</>";
            var resposneBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            var response = new HttpResponse("text/html", resposneBodyBytes);
            return response;
        }
    }
}
