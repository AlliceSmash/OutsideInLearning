using Newtonsoft.Json.Serialization;
using Owin;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace RunningJournalApi.AcceptanceTests
{
    public class OwinTestConfig
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var configuration = new HttpConfiguration();
            configuration.Services.Replace(typeof(IAssembliesResolver), new CustomAssemblyResolver());

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

    public class CustomAssemblyResolver: IAssembliesResolver
    {
        public ICollection<Assembly> GetAssemblies()
        {
            return new List<Assembly> { typeof(RunningJournalApi.JournalController).Assembly };
        }
    }
}
