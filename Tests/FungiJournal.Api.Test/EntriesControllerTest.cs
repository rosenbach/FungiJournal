using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using FungiJournal.API.Controllers;
using FungiJournal.DataAccess;
using FungiJournal.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using FungiJournal.DataAccess.Test;

namespace FungiJournal.Api.Test
{
    public class EntriesControllerTest
    {
        [Fact]
        public void TestIfPostWasSuccessful()
        {
            //arrange
            var sqLiteDataAccess = new SQLiteDataAccess(DataAccessMock.SetupMockDBContext());
            var sut = new EntriesController(sqLiteDataAccess);
            Entry mockEntry = DataAccessMock.CreateMockEntry();

            //act
            var result = sut.PostEntry(mockEntry);

            //assert
            result.Should().BeEquivalentTo(new ActionResult<Entry>(new OkObjectResult(mockEntry)));
        }
    }
}
