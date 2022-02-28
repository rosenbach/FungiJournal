using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FungiJournal.DataAccess
{
    public class DataAccessConfiguration
    {
        private bool useInMemoryDatabase;
        public bool UseInMemoryDatabase
        {
            get { return useInMemoryDatabase; }
            set
            {
                if (value == true)
                {
                    ConnectionString = "TestDB";
                    useInMemoryDatabase = true;
                }
                else
                {
                    useInMemoryDatabase = false;
                }
            }
        }
        public string ConnectionString { get; set; } = default!;
    }
}
