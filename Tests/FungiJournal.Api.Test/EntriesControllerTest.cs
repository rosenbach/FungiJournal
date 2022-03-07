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
        public async Task TestIfGetByIdWasSuccessful()
        {

        }

        [Fact]
        public async Task TestIfGetAllWasSuccessful()
        {
            //arrange
            var sqLiteDataAccess = new SQLiteDataAccess(DataAccessMock.CreateMockDBContext());
            var sut = new EntriesController(sqLiteDataAccess);
            Entry mockEntry = DataAccessMock.CreateMockEntry();
            Entry mockEntry2 = DataAccessMock.CreateMockEntry();
            await sut.PostEntry(mockEntry);
            await sut.PostEntry(mockEntry2);
            var expected = new[]
            {
                mockEntry,
                mockEntry2
            };

            //act
            var result = await sut.GetAll();

            //assert
            result.Should().BeOfType<OkObjectResult>();
            var typedResult = result as OkObjectResult;

            var entries = typedResult?.Value;
            entries.Should().BeEquivalentTo(expected);
            
        }

        [Fact]
        public async Task TestIfEntryWasDeleted()
        {
            //arrange
            var sqLiteDataAccess = new SQLiteDataAccess(DataAccessMock.CreateMockDBContext());
            var sut = new EntriesController(sqLiteDataAccess);
            Entry mockEntry = DataAccessMock.CreateMockEntry();
            Entry mockEntryToBeDeleted = DataAccessMock.CreateMockEntry();
            await sut.PostEntry(mockEntry);
            await sut.PostEntry(mockEntryToBeDeleted);

            //act
            var result = await sut.DeleteEntry(mockEntryToBeDeleted.EntryId);

            //assert
            result.Should().BeOfType<OkObjectResult>();
            var entries = await sqLiteDataAccess.GetEntriesAsync();
            entries.Should().ContainSingle();
            var resultFromDatabase = entries.Single();
            resultFromDatabase?.EntryId.Should().Be(mockEntry.EntryId);
            resultFromDatabase?.EntryId.Should().NotBe(mockEntryToBeDeleted.EntryId);

            var typedResult = result as OkObjectResult;
            var resultingEntry = typedResult?.Value as Entry;
            resultingEntry?.EntryId.Should().Be(mockEntryToBeDeleted.EntryId);
            resultingEntry?.EntryId.Should().NotBe(mockEntry.EntryId);
        }

        [Fact]
        public async Task TestIfEntryWasPut()
        {
            //arrange
            var sqLiteDataAccess = new SQLiteDataAccess(DataAccessMock.CreateMockDBContext());
            var sut = new EntriesController(sqLiteDataAccess);
            Entry mockEntry = DataAccessMock.CreateMockEntry();
            await sut.PostEntry(mockEntry);
            string modifiedDescription = "I was modified";
            mockEntry.Description = modifiedDescription;


            //act
            var result = await sut.PutEntry(mockEntry.EntryId, mockEntry);

            //assert
            result.Should().BeOfType<NoContentResult>();
            var entries = await sqLiteDataAccess.GetEntriesAsync();
            entries.Should().ContainSingle();
            var resultFromDatabase = entries.Single();
            resultFromDatabase.Description.Should().Be(modifiedDescription);
        }

        [Fact]
        public async Task TestIfEntryWasPosted()
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
