using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace RunningJournalApi.AcceptanceTests
{
    public class HomeJsonTests
    {
        [Fact]
        public void GetResponseReturnCorrectStatusCode()
        {
            const string baseUrl = "http://localhost:5000";
            var startOptions = new StartOptions();
            startOptions.Urls.Add(baseUrl);

            using (WebApp.Start<MyOwinServer>(startOptions))
            {
                var client = new HttpClient { BaseAddress = new Uri(baseUrl) };
                var response =client.GetAsync("").Result;
                Assert.True(response.IsSuccessStatusCode, "Actual status code: " + response.StatusCode);
            }
        }
    }
}
