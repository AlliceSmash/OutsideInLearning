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
            using (var client = new HttpClient())
            {
                var response = client.GetAsync("").Result;
                Assert.True(response.IsSuccessStatusCode, "Actual status code: " + response.StatusCode);
            }
        }
    }
}
