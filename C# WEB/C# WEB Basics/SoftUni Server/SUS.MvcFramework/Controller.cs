using SUS.HTTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SUS.MvcFramework
{
  public abstract class Controller
    {
       
        public HttpResponse View([CallerMemberName]string viewPath=null)
        {
            var layout = System.IO.File.ReadAllText("Views/Shared/_Layout.cshtml");

            
                var viewContent = System.IO.File.ReadAllText("Views/"+ this.GetType()
                    .Name.Replace("Controller",string.Empty)+"/" +viewPath + ".cshtml" );

            var responeHtml = layout.Replace("@RenderBody()", viewContent);
                var resposneBodyBytes = Encoding.UTF8.GetBytes(responeHtml);
                var response = new HttpResponse("text/html", resposneBodyBytes);
                return response;
            
        }
        public HttpResponse File(string filePath,string contenType)
        {
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var response = new HttpResponse(contenType, fileBytes);
            ///var response = new HttpResponse("image/vnd.microsoft.icon",fileBytes);
            return response;
        }
        public HttpResponse Redirect(string url)
        {
            var response = new HttpResponse(HttpStatusCode.Found);
            response.Headers.Add(new Header("Location", url));
            return response;
        }
    }
}
