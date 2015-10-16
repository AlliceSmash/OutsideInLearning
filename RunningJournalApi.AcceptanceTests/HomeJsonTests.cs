using Microsoft.Owin.Testing;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using Xunit;

namespace RunningJournalApi.AcceptanceTests
{
    public class HomeJsonTests :IDisposable
    {
        private TestServer server;
        public HomeJsonTests()
        {
            server = TestServer.Create<Startup>();
        }

        public void Dispose()
        {
            server.Dispose();
        }

        [Fact]
        public void GetResponseReturnCorrectStatusCode()
        {
            var response = server.HttpClient.GetAsync("/journal").Result;
            Assert.True(response.IsSuccessStatusCode, "Actual status code: " + response.StatusCode);
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

            var response = server.HttpClient.PostAsJsonAsync("/journal", json).Result;
            Assert.True(response.IsSuccessStatusCode, "Actual status code: " + response.StatusCode);
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
            server.HttpClient.PostAsJsonAsync("/journal", json).Wait();
            var response = server.HttpClient.GetAsync("/journal").Result;
            var actual = response.Content.ReadAsJsonAsync().Result;
            var actualJObjects = actual.entries.Children<JObject>();
            Assert.Contains(expected, actualJObjects);
        }
    }
}
