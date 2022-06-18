namespace FungiJournal.API.Classes
{
    public class FungiQueryParameters : QueryParameters
    {
        public String Name { get; set; }
        public int? FungiId { get; set; }
        public String Season { get; set; }
        public int? FoodValue { get; set; }


        public Boolean HasFungiId()
        {
            return (FungiId != null);
        }

        public Boolean HasFoodValue()
        {
            return (FoodValue != null);
        }
    }
}
