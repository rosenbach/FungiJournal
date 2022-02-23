using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

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
            var options = Options.Create(new DataAccessConfiguration { ConnectionString = "TestDB" });
            
            var dbContextOptions = new DbContextOptionsBuilder<CodeFirstDbContext>()
                .UseInMemoryDatabase("123").Options;

            var dbContext = new CodeFirstDbContext(dbContextOptions, options);
            var sut = new SQLiteDataAccess(dbContext);

            //act
            sut.AddEntry(new Domain.Models.Entry { Description = "Test"});
            var result = sut.LoadEntries();

            //assert
            result.Should().BeEquivalentTo(
                new[] { new Domain.Models.Entry { Description = "Test" } }, 
                options => options.Excluding(x => x.EntryId));
        }
    }
}