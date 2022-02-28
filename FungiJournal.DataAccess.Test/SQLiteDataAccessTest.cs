using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using FungiJournal.Domain.Models;

namespace FungiJournal.DataAccess.Test
{
    public class SQLiteDataAccessTest
    {
        //create SQLite inMemory database

        [Fact]
        public void TestIfEntryWasAdded()
        {
            //arrange
            //open the connection to the cnn
            var options = Options.Create(new DataAccessConfiguration { UseInMemoryDatabase = true });

            var dbContextOptions = new DbContextOptionsBuilder<CodeFirstDbContext>().Options;

            var dbContext = new CodeFirstDbContext(dbContextOptions, options);
            var sut = new SQLiteDataAccess(dbContext);

            Entry mockEntry = CreateMockEntry();

            //act
            sut.AddEntry(mockEntry);
            var result = sut.LoadEntries();

            //assert
            result.Should().BeEquivalentTo(
                new[] { mockEntry },
                options => options.Excluding(x => x.EntryId));
        }

        [Fact]
        public void TestIfDatabaseIsInMemory()
        {
            //arrange
            var options = Options.Create(new DataAccessConfiguration { UseInMemoryDatabase = true });
            var dbContextOptions = new DbContextOptionsBuilder<CodeFirstDbContext>().Options;

            //act
            var sut = new CodeFirstDbContext(dbContextOptions, options);
            var result = sut.Database.IsInMemory();

            //assert
            result.Should().Be(true);
        }

        private Entry CreateMockEntry()
        {
            return new Entry { EntryId = 1, Description = "Mock Entry" };
        }
    }
}