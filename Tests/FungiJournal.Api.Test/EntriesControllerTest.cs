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
using System.Collections.Generic;
using FungiJournal.API.Classes;

namespace FungiJournal.Api.Test
{
    public class EntriesControllerTest
    {
        [Fact]
        public async Task TestIfGetByIdWasSuccessful()
        {
            //arrange
            var sqLiteDataAccess = new SQLiteDataAccess(DataAccessMock.CreateMockDBContext());
            var sut = new EntriesController(sqLiteDataAccess);
            Entry mockEntry = DataAccessMock.CreateMockEntry();
            await sut.PostEntry(mockEntry);
            var expected = mockEntry;

            //act
            var result = await sut.GetById(mockEntry.EntryId);

            //assert
            result.Should().BeOfType<OkObjectResult>();
            var typedResult = result as OkObjectResult;

            var entries = typedResult?.Value;
            entries.Should().BeEquivalentTo(expected);
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
        public async Task TestIfGetAllByEntryIdQueryWasSuccessful()
        {
            //arrange
            var sqLiteDataAccess = new SQLiteDataAccess(DataAccessMock.CreateMockDBContext());
            var sut = new EntriesController(sqLiteDataAccess);
            Entry mockEntry = DataAccessMock.CreateMockEntry();
            Entry mockEntry2 = DataAccessMock.CreateMockEntry();

            await sut.PostEntry(mockEntry);
            await sut.PostEntry(mockEntry2);

            EntryQueryParameters queryParameters = new();
            int idToSearchFor = mockEntry2.EntryId;
            queryParameters.EntryId = idToSearchFor;

            var expected = new[]
            {
                mockEntry2
            };


            //act
            var result = await sut.GetAll(queryParameters);

            //assert
            result.Should().BeOfType<OkObjectResult>();
            var typedResult = result as OkObjectResult;

            var entries = typedResult?.Value;
            entries.Should().BeEquivalentTo(expected);

        }

        [Fact]
        public async Task TestIfGetAllByDescriptionQueryWasSuccessful()
        {
            //arrange
            var sqLiteDataAccess = new SQLiteDataAccess(DataAccessMock.CreateMockDBContext());
            var sut = new EntriesController(sqLiteDataAccess);
            Entry mockEntry = DataAccessMock.CreateMockEntry();
            Entry mockEntry2 = DataAccessMock.CreateMockEntry();
            string DescriptionToSearchFor = "TestDescr";

            mockEntry2.Description = DescriptionToSearchFor;

            await sut.PostEntry(mockEntry);
            await sut.PostEntry(mockEntry2);

            EntryQueryParameters queryParameters = new()
            {
                Description = DescriptionToSearchFor
            };

            var expected = new[]
            {
                mockEntry2
            };


            //act
            var result = await sut.GetAll(queryParameters);

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
            var entriesResponse = await sut.GetAll() as OkObjectResult;
            var entries = entriesResponse?.Value as List<Entry>;
            //var entries = await sqLiteDataAccess.GetEntriesAsync();
            entries.Should().ContainSingle();
            var resultFromDatabase = entries?.Single();
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

            Fungi mockFungi = DataAccessMock.CreateMockFungi();
            string modifiedFungiName = "I was modified";
            mockFungi.CommonName = modifiedFungiName;
            string modifiedDescription = "I was modified";
            mockEntry.Description = modifiedDescription;
            mockEntry.Fungi = mockFungi;


            //act
            var result = await sut.PutEntry(mockEntry.EntryId, mockEntry);

            //assert
            var entries = await sqLiteDataAccess.GetEntriesAsync();
            entries.Should().ContainSingle();
            var resultFromDatabase = entries.Single();
            resultFromDatabase.Description.Should().Be(modifiedDescription);
            resultFromDatabase.Fungi?.CommonName.Should().Be(modifiedFungiName);
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
