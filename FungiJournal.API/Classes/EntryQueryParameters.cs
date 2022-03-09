namespace FungiJournal.API.Classes
{
    public class EntryQueryParameters : QueryParameters
    {
        public String Name { get; set; }
        public String LatinName { get; set; }
        public bool IsToxic { get; set; }
    }
}
