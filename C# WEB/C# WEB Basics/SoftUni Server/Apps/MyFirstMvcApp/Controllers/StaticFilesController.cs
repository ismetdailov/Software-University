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
    public class StaticFilesController : Controller
    {
        public HttpResponse Favicon(HttpRequest request)
        {
            var fileBytes = File.ReadAllBytes("wwwroot/fav.svg");
            var response = new HttpResponse("image/svg+xml", fileBytes);
            ///var response = new HttpResponse("image/vnd.microsoft.icon",fileBytes);
            return response;
        }
    }
}
