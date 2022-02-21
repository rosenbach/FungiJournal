using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Options;

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
            var options = Options.Create(new DataAccessConfiguration { ConnectionString = "Data Source=:memory:;Version=3;New=True;" });
            var sut = new SQLiteDataAccess(options);

            //act
            sut.AddEntry(new Domain.Models.Entry { Description = "Test"});
            var result = sut.LoadEntries();

            //assert
            result.Should().BeEquivalentTo(new[] { new Domain.Models.Entry { Description = "Test" } });
        }
    }
}