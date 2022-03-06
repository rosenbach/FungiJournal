using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using FungiJournal.API.Controllers;
using FungiJournal.DataAccess;
using FungiJournal.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using FungiJournal.DataAccess.Test;
using System.Threading.Tasks;
using System.Linq;

namespace FungiJournal.Api.Test
{
    public class EntriesControllerTest
    {
        [Fact]
        public async Task TestIfPostWasSuccessful()
        {
            //arrange
            var sqLiteDataAccess = new SQLiteDataAccess(DataAccessMock.CreateMockDBContext());
            var sut = new EntriesController(sqLiteDataAccess);
            Entry mockEntry = DataAccessMock.CreateMockEntry();

            //act
            var result = await sut.PostEntry(mockEntry);

            //assert
            result.Should().BeOfType<CreatedAtActionResult>();
            var entries = await sqLiteDataAccess.GetEntriesAsync();
            entries.Should().ContainSingle();
            var resultFromDatabase = entries.Single();
            var typedResult = result as CreatedAtActionResult;
            typedResult?.Value.Should().BeEquivalentTo(resultFromDatabase);
            resultFromDatabase.EntryId.Should().NotBe(0);
        }
    }
}
