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
        //private readonly static List<JournalEntryModel> entries = new List<JournalEntryModel>();
        public HttpResponseMessage Get()
        {
            var connStr = ConfigurationManager.ConnectionStrings["running-journal"].ConnectionString;
            var db = Database.OpenConnection(connStr);

            var entries = db.JournalEntry.FindAll(db.JournalEntry.User.UserName == "foo")
                .ToArray<JournalEntryModel>();

            return Request.CreateResponse(System.Net.HttpStatusCode.OK,
                new JournalModel { Entries = entries });
        }

        public HttpResponseMessage Post(JournalEntryModel journalEntry)
        {
            var connStr = ConfigurationManager.ConnectionStrings["running-journal"].ConnectionString;
            var db = Database.OpenConnection(connStr);
            var user = db.User.Insert(UserName: "foo");
            db.JournalEntry.Insert(
                UserId: user.UserId,
                Time: journalEntry.Time,
                Distance: journalEntry.Distance,
                Duration: journalEntry.Duration);
            
            return Request.CreateResponse();
        }
    }
}
