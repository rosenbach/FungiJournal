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
            using var sut = new SQLiteDataAccess(DataAccessMock.SetupMockDBContext());
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
            using var sut = new SQLiteDataAccess(DataAccessMock.SetupMockDBContext());

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
            using var sut = DataAccessMock.SetupMockDBContext();

            //act
            var result = sut.Database.IsInMemory();

            //assert
            result.Should().Be(true);
        }



    }
}