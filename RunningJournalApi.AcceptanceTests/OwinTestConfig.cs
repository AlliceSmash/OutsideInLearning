using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace RunningJournalApi.AcceptanceTests
{
    public class OwinTestConfig
    {
        public void Configuration(IAppBuilder appBuilder)
        {
          
            var configuration = new HttpConfiguration();
            configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            appBuilder.UseWebApi(configuration);
        }
    }
}
