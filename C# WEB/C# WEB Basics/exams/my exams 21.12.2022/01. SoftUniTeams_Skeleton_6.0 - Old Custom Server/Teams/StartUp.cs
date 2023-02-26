namespace Teams
{
    using BasicWebServer.Server;
    using BasicWebServer.Server.Routing;
    using Teams.Data;
    using System.Threading.Tasks;

    public class Startup
    {
        public static async Task Main()
        {
            var server = new HttpServer(routes => routes
               .MapControllers()
               .MapStaticFiles());

            await server.Start();
        }
    }
}