using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using FungiJournal.API.Controllers;
using FungiJournal.DataAccess;
using FungiJournal.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace FungiJournal.Api.Test
{
    public class EntriesControllerTest
    {
        [Fact]
        public void TestIfPostWasSuccessful()
        {
            //arrange
            //open the connection to the cnn
            var options = Options.Create(new DataAccessConfiguration { ConnectionString = "TestDB" });
            var dbContextOptions = new DbContextOptionsBuilder<CodeFirstDbContext>()
                .UseInMemoryDatabase("123").Options;
            var dbContext = new CodeFirstDbContext(dbContextOptions, options);
            var sqLiteDataAccess = new SQLiteDataAccess(dbContext);
            var sut = new EntriesController(sqLiteDataAccess);

            Entry mockEntry = new Entry { EntryId = 1, Description = "Hello" };

            //act
            var result = sut.PostEntry(mockEntry);

            //assert
            result.Should().BeEquivalentTo(new ActionResult<Entry>(new OkObjectResult(mockEntry)));
        }
    }
}
