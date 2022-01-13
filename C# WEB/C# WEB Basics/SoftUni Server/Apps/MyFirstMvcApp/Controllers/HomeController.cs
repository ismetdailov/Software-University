using SUS.HTTP;
using SUS.MvcFramework;

namespace MyFirstMvcApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public HttpResponse Index()
        {
      
            if (this.IsUserSignedIn())
            {
                return this.Redirect("Cards/All");
            }
            else
            {
            return this.View();
            }
        }
   
    }
}
