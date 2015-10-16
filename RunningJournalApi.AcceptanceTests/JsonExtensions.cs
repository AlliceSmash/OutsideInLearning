using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningJournalApi.AcceptanceTests
{
    public static class JsonExtensions
    {
        public static dynamic ToJObject(this Object value)
        {
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(value));
        }
    }
}
