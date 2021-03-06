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
        private const string UserIdSessionName = "UserId";
        private SusViewEngine viewEngine;

        public Controller()
        {
            this.viewEngine = new SusViewEngine();
        }
        public HttpRequest Request { get; set; }
        protected HttpResponse View(object viewModel = null, [CallerMemberName] string viewPath = null)
        {
            var viewContent = System.IO.File.ReadAllText("Views/" + this.GetType()
                  .Name.Replace("Controller", string.Empty) + "/" + viewPath + ".cshtml");
            viewContent = this.viewEngine.GetHtml(viewContent, viewModel, this.GetUserId());

            var responeHtml = this.PutViewInLayout(viewContent, viewModel);

            var resposneBodyBytes = Encoding.UTF8.GetBytes(responeHtml);
            var response = new HttpResponse("text/html", resposneBodyBytes);
            return response;

        }

        protected HttpResponse File(string filePath, string contenType)
        {
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var response = new HttpResponse(contenType, fileBytes);
            ///var response = new HttpResponse("image/vnd.microsoft.icon",fileBytes);
            return response;
        }
        protected HttpResponse Redirect(string url)
        {
            var response = new HttpResponse(HttpStatusCode.Found);
            response.Headers.Add(new Header("Location", url));
            return response;
        }
        protected HttpResponse Error(string errorText)
        {
            var viewContent = $"<div class=\"alert alert-danger\" role=\"alert\">{errorText}</div";
            var responeHtml = this.PutViewInLayout(viewContent);

            var resposneBodyBytes = Encoding.UTF8.GetBytes(responeHtml);
            var response = new HttpResponse("text/html", resposneBodyBytes, HttpStatusCode.ServerError);
            return response;
        }
        protected void SignIn(string userId)
        {
            this.Request.Session[UserIdSessionName] = userId;
        }
        protected void SignOut()
        {
            this.Request.Session[UserIdSessionName] = null;

        }
        protected bool IsUserSignedIn() =>
             this.Request.Session.ContainsKey(UserIdSessionName) &&
             this.Request.Session[UserIdSessionName] !=null;


        protected string GetUserId() =>
            this.Request.Session.ContainsKey(UserIdSessionName) ?
            this.Request.Session[UserIdSessionName] : null;

        private string PutViewInLayout(string viewContent, object viewModel = null)
        {
            var layout = System.IO.File.ReadAllText("Views/Shared/_Layout.cshtml");
            layout = layout.Replace("@RenderBody()", "____VIEW_GOES_HERE____");
            layout = this.viewEngine.GetHtml(layout, viewModel ,this.GetUserId());
            var responeHtml = layout.Replace("____VIEW_GOES_HERE____", viewContent);
            return responeHtml;
        }
    }
}
