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
        }
    }
}
