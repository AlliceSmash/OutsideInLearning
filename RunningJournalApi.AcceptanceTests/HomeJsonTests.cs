using Microsoft.Owin.Hosting;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
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

            using (WebApp.Start<Startup>(startOptions))
            {
                var client = new HttpClient { BaseAddress = new Uri(baseUrl) };
                var response = client.GetAsync("/journal").Result;
                Assert.True(response.IsSuccessStatusCode, "Actual status code: " + response.StatusCode);
            }
        }

        [Fact]
        public void PostReturnCorrectStatusCode()
        {
            var json = new
            {
                time = DateTimeOffset.Now,
                distance = 8500,
                duration = TimeSpan.FromMinutes(44)
            };

            const string baseUrl = "http://localhost:5000";
            var startOptions = new StartOptions();
            startOptions.Urls.Add(baseUrl);

            using (WebApp.Start<Startup>(startOptions))
            {
                var client = new HttpClient { BaseAddress = new Uri(baseUrl) };
                var response = client.PostAsJsonAsync("/journal", json).Result;
                Assert.True(response.IsSuccessStatusCode, "Actual status code: " + response.StatusCode);
            }

        }
        [Fact]
        public void GetAfterPostReturnCorrectStatusCode()
        {
            var json = new
            {
                time = DateTimeOffset.Now,
                distance = 8100,
                duration = TimeSpan.FromMinutes(41)
            };
            var expected = json.ToJObject();

            const string baseUrl = "http://localhost:5000";
            var startOptions = new StartOptions();
            startOptions.Urls.Add(baseUrl);

            using (WebApp.Start<Startup>(startOptions))
            {
                var client = new HttpClient { BaseAddress = new Uri(baseUrl) };
                client.PostAsJsonAsync("/journal", json).Wait();

                var response = client.GetAsync("/journal").Result;
                var actual = response.Content.ReadAsJsonAsync().Result;
                var actualJObjects = actual.entries.Children<JObject>();
                Assert.Contains(expected, actualJObjects);
            }

        }
    }
}
