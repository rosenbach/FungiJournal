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
            Random rdm = new();
            return new Entry { EntryId = rdm.Next(), Description = "Mock Entry" };
        }

        public static CodeFirstDbContext SetupMockDBContext()
        {
            var options = Options.Create(new DataAccessConfiguration { UseInMemoryDatabase = true });
            var dbContextOptions = new DbContextOptionsBuilder<CodeFirstDbContext>().Options;
            return new CodeFirstDbContext(dbContextOptions, options);
        }
    }
}
