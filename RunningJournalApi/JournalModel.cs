using System.Collections.Generic;

namespace RunningJournalApi
{
    internal class JournalModel
    {
        public IList<JournalEntryModel> Entries { get; internal set; }
    }
}