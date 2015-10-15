using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;

namespace RunningJournalApi.AcceptanceTests
{
    internal class MyOwinServer
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // Setup WebAPI configuration  
            var configuration = new HttpConfiguration();

            configuration.Routes.Add("API Default", new HttpRoute("{Controller}"));

            // Register the WebAPI to the pipeline  
            appBuilder.UseWebApi(configuration);
            //appBuilder.Run((owinContext) =>
            //{
            //    owinContext.Response.ContentType = "text/plain";

            //    // here comes the performance, everythign in the Katana is Async. Living in the current century.  
            //    // Let's print our obvious message: :)  
            //    return owinContext.Response.WriteAsync("Hello World.");
            //});
            //var config = new HttpConfiguration();
            //config.Routes.MapHttpRoute(
            //    "API Default",
            //    "api/{controller}/{action}/{id}",
            //    new { id = RouteParameter.Optional });
            //builder.UseWebApi(config);
        }
    }
}
