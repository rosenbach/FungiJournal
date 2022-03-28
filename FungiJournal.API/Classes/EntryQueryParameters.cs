namespace FungiJournal.API.Classes
{
    public class EntryQueryParameters : QueryParameters
    {
        public int EntryId { get; set; }
        public String Description { get; set; }
        public int FungiId { get; set; }


        public Boolean HasEntryId()
        {
            return EntryId != 0;
        }
    }
}
