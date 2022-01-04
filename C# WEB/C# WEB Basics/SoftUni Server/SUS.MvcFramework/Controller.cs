using SUS.HTTP;
using SUS.MvcFramework.ViewEngine;
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
        private SusViewEngine viewEngine;

        public Controller()
        {
            this.viewEngine = new SusViewEngine();
        }
       
        public HttpResponse View(object viewModel=null,[CallerMemberName]string viewPath=null)
        {
            var layout = System.IO.File.ReadAllText("Views/Shared/_Layout.cshtml");
            layout = layout.Replace("@RenderBody()", "____VIEW_GOES_HERE____");
            layout = this.viewEngine.GetHtml(layout, viewModel);
            
                var viewContent = System.IO.File.ReadAllText("Views/"+ this.GetType()
                    .Name.Replace("Controller",string.Empty)+"/" +viewPath + ".cshtml" );
            viewContent = this.viewEngine.GetHtml(viewContent, viewModel);

            var responeHtml = layout.Replace("____VIEW_GOES_HERE____", viewContent);
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
