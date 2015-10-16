using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(RunningJournalApi.Startup))]
namespace RunningJournalApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var configuration = new HttpConfiguration();

            configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new
                {
                    id = RouteParameter.Optional
                });

            configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
            appBuilder.UseWebApi(configuration);
        }
    }
}
