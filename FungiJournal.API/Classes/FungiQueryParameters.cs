namespace FungiJournal.API.Classes
{
    public class FungiQueryParameters : QueryParameters
    {
        public int EntryId { get; set; }
        public String Name { get; set; }
        public int FungiId { get; set; }


        public Boolean HasFungiId()
        {
            return EntryId != 0;
        }
    }
}
