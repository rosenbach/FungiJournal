namespace FungiJournal.API.Classes
{
    public class QueryParameters
    {
        const int _maxSize = 100;
        private int _size = 50;

        public int Page { get; set; }
        public int Size
        {
            get { return _size; }
            set
            {
                if (value < 0)
                {
                    _size = 0;
                }
                else
                {
                    _size = Math.Min(_maxSize, value);
                }
            }
        }
    }
}
