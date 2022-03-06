using FungiJournal.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FungiJournal.DataAccess.Test
{
    public static class DataAccessMock
    {
        public static Entry CreateMockEntry()
        {
            return new Entry { Description = "Mock Entry" };
        }

        public static CodeFirstDbContext CreateMockDBContext()
        {
            var options = Options.Create(new DataAccessConfiguration { UseInMemoryDatabase = true });
            var dbContextOptions = new DbContextOptionsBuilder<CodeFirstDbContext>().Options;
            return new CodeFirstDbContext(dbContextOptions, options);
        }
    }
}
