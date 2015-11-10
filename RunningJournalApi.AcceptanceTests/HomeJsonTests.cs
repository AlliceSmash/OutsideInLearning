using Microsoft.Owin.Testing;
using Simple.Data;
using System;
using System.Configuration;
using System.Dynamic;
using System.Net.Http;
using Xunit;

namespace RunningJournalApi.AcceptanceTests
{
    public class HomeJsonTests :IDisposable
    {
        private TestServer server;
        public HomeJsonTests()
        {
            server = TestServer.Create<OwinTestConfig>();
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
            Assert.Contains(expected, actual.entries);
        }

        [Fact]
        [UseDatabase]
        public void AfterPoastingEntryGetRootReturnsEntryInContent()
        {
            var json = new
            {
                time = DateTimeOffset.Now,
                distance = 8200,
                duration = TimeSpan.FromMinutes(50)
            };
            var expected = json.ToJObject();
            server.HttpClient.PostAsJsonAsync("/journal", json).Wait();

            var response = server.HttpClient.GetAsync("/journal").Result;

            var actual = response.Content.ReadAsJsonAsync().Result;
            Assert.Contains(expected, actual.entries);
        }

        [Fact]
        [UseDatabase]
        public void GetRootReturnsCorrectEntryFromDataBase()
        {
            dynamic entry = new ExpandoObject();
            entry.time = DateTimeOffset.Now;
            entry.distance = 6000;
            entry.duration = TimeSpan.FromMinutes(1);

            var expected = ((object)entry).ToJObject();

            var connStr = ConfigurationManager.ConnectionStrings["running-journal"].ConnectionString;
            var db = Database.OpenConnection(connStr);
           var user= db.User.Insert(UserName: "foo");
            var userId = user.UserId;
     //       var userId = db.User.Instert(UserName: "foo").UserId;
            entry.UserId = userId;

            db.JournalEntry.Insert(entry);
            var response = server.HttpClient.GetAsync("/journal").Result;
            var actual = response.Content.ReadAsJsonAsync().Result;

            Assert.Contains(expected, actual.entries);

        }
    }
}
