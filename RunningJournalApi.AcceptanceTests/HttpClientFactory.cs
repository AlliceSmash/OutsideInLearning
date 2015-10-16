using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RunningJournalApi.AcceptanceTests
{
    public class HttpClientFactory
    {
        public static HttpClient Create()
        {
            const string baseUrl = "http://localhost:5000";
            var startOptions = new StartOptions();
            startOptions.Urls.Add(baseUrl);
            using (WebApp.Start<Startup>(startOptions))
            {
                return new HttpClient { BaseAddress = new Uri(baseUrl) };
            }
        }
    }
}

