using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using FungiJournal.Domain.Models;
using System;
using System.Threading.Tasks;

namespace FungiJournal.DataAccess.Test
{
    public class SQLiteDataAccessTest
    {
        [Fact]
        public async Task TestIfEntryWasAdded()
        {
            //arrange
            using var sut = new SQLiteDataAccess(DataAccessMock.CreateMockDBContext());
            Entry mockEntry = DataAccessMock.CreateMockEntry();

            //act
            await sut.AddEntryAsync(mockEntry);
            var result = await sut.GetEntriesAsync();

            //assert
            result.Should().BeEquivalentTo(
                new[] { mockEntry },
                options => options.Excluding(x => x.EntryId));
        }


        [Fact]
        public async Task TestIfEntryWasDeletedAsync()
        {
            //arrange
            using var sut = new SQLiteDataAccess(DataAccessMock.CreateMockDBContext());

            Entry mockEntry = DataAccessMock.CreateMockEntry();
            Entry mockEntry_toDelete = DataAccessMock.CreateMockEntry();
            await sut.AddEntryAsync(mockEntry);
            await sut.AddEntryAsync(mockEntry_toDelete);

            var expected = new[] {
                new Entry {
                    EntryId = mockEntry.EntryId,
                    Description = mockEntry.Description
                }
            };

            //act
            await sut.DeleteEntryAsync(mockEntry_toDelete);
            var entries = await sut.GetEntriesAsync();

            //assert
            entries.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void TestIfDatabaseIsInMemory()
        {
            //arrange
            using var sut = DataAccessMock.CreateMockDBContext();

            //act
            var result = sut.Database.IsInMemory();

            //assert
            result.Should().Be(true);
        }

        
        [Fact]
        public async Task TestIfEntryWasUpdated()
        {
            //arrange
            using var sut = new SQLiteDataAccess(DataAccessMock.CreateMockDBContext());

            var mockEntry = DataAccessMock.CreateMockEntry();

            await sut.AddEntryAsync(mockEntry);

            mockEntry.Description = "Mocki Mock";

            var expected = new[] {
                new Entry {
                    EntryId = mockEntry.EntryId,
                    Description = "Mocki Mock"
                }
            };

            //act
            await sut.UpdateEntryAsync(mockEntry);

            var entries = await sut.GetEntriesAsync();

            //assert
            entries.Should().BeEquivalentTo(expected);
        }
        

    }
}