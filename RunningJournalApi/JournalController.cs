using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Simple.Data;

namespace RunningJournalApi
{
    public class JournalController : ApiController
    {
        private readonly static List<JournalEntryModel> entries = new List<JournalEntryModel>();
        public HttpResponseMessage Get()
        {
            var connStr = ConfigurationManager.ConnectionStrings["running-journal"].ConnectionString;
            var db = Database.OpenConnection(connStr);

            var entries = db.JournalEntry.FindAll(db.JournalEntry.User.UserName == "foo")
                .ToArray<JournalEntryModel>();

            return Request.CreateResponse(System.Net.HttpStatusCode.OK,
                new JournalModel { Entries = entries });
        }

        public HttpResponseMessage Post(JournalEntryModel journEntry)
        {
            entries.Add(journEntry);
            return Request.CreateResponse();
        }
    }
}
