using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
            List<Assembly> baseAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var apiAssembly = Assembly.LoadFrom(@"C:\tests\RunningJournalApi\RunningJournalApi\obj\Debug\RunningJournalApi.dll");
            baseAssemblies.Add(apiAssembly);
            return baseAssemblies;
        }
    }
}
