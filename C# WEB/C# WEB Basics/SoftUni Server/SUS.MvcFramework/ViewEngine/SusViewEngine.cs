using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SUS.MvcFramework.ViewEngine
{
    public class SusViewEngine : IViewEngine
    {
        public string GetHtml(string templateCode, object viewModel)
        {
            string csharpCode = GenerateCSharpFromTemplate(templateCode);
            IView executableObject = GenerateExecutableCode(csharpCode,viewModel);
            string html = executableObject.ExecuteTemplate(viewModel);
            return html;
        }


        private string GenerateCSharpFromTemplate(string templateCode)
        {
            string methodBody = GetMethodBody(templateCode);
            string csharpCode = @"
using Sysemt;
using Sysemt.Text;
using Sysemt.Linq;
using Sysemt.Linq.Collections.Generic;
using SUS.MvcFramework.ViewEngine;
namespace ViewNamespace
{
     public class ViewClass : IView
     {
        public class ViewClass : IVew
        {
            var html = new StrinBuilder();
            " + methodBody + @";
            
               return html.ToString();
           }
    }
}
";


            return csharpCode;
        }

        private string GetMethodBody(string templateCode)
        {
            return string.Empty;
        }

        private IView GenerateExecutableCode(string csharpCode, object viewModel)
        {
            var compileResult = CSharpCompilation.Create("ViewAssembly")
                  .WithOptions(new CSharpCompilationOptions(Microsoft.CodeAnalysis.OutputKind.DynamicallyLinkedLibrary))
                  .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                  .AddReferences(MetadataReference.CreateFromFile(typeof(IView).Assembly.Location));
            if (viewModel !=null)
            {
                compileResult = compileResult
                    .AddReferences(MetadataReference.CreateFromFile(viewModel.GetType().Assembly.Location));
            }
            //ToDo net5.0
            var libraries = Assembly.Load(new AssemblyName("netstandart")).GetReferencedAssemblies();

            foreach (var library in libraries)
            {
                compileResult = compileResult.AddReferences(MetadataReference.CreateFromFile(Assembly.Load(library).Location));
            }
          compileResult=  compileResult.AddSyntaxTrees(SyntaxFactory.ParseSyntaxTree(csharpCode));
            compileResult.Emit("view.dll");
            return null;

            //Roslyn
            //C# ->ececute -> IView
            
        }
    }
}
