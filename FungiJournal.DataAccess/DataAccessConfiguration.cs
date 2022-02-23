using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FungiJournal.DataAccess
{
    public class DataAccessConfiguration
    {
        public string ConnectionString { get; set; } = default!;
        public string? InMemoryConnectionString { get; set; }
    }
}
