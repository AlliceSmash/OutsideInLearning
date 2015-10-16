using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace RunningJournalApi
{
    public class JournalController : ApiController
    {
        private readonly static List<JournalEntryModel> entries = new List<JournalEntryModel>();
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(System.Net.HttpStatusCode.OK,
                new JournalModel {Entries = entries.ToArray<JournalEntryModel>() });
        }

        public HttpResponseMessage Post(JournalEntryModel journEntry)
        {
            entries.Add(journEntry);
            return Request.CreateResponse();
        }
    }
}
