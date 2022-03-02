using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using FungiJournal.Domain.Models;
using System;

namespace FungiJournal.DataAccess.Test
{
    public class SQLiteDataAccessTest
    {
        [Fact]
        public async void TestIfEntryWasAdded()
        {
            //arrange
            var sut = new SQLiteDataAccess(DataAccessMock.SetupMockDBContext());
            Entry mockEntry = DataAccessMock.CreateMockEntry();

            //act
            sut.AddEntry(mockEntry);
            var result = await sut.GetEntries();

            //assert
            result.Should().BeEquivalentTo(
                new[] { mockEntry },
                options => options.Excluding(x => x.EntryId));

            sut.DisposeAsync();
        }


        [Fact]
        public async void TestIfEntryWasDeletedAsync()
        {
            //arrange
            var sut = new SQLiteDataAccess(DataAccessMock.SetupMockDBContext());
            Entry mockEntry = DataAccessMock.CreateMockEntry();
            sut.AddEntry(mockEntry);
            var entries = await sut.GetEntries();
            int entriesCount = entries.Count;

            //act
            sut.DeleteEntry(mockEntry.EntryId);
            entries = await sut.GetEntries();
            bool result = entries.Contains(mockEntry);

            //assert
            result.Should().Be(false);
            sut.DisposeAsync();
        }

        [Fact]
        public void TestIfDatabaseIsInMemory()
        {
            //arrange
            var sut = DataAccessMock.SetupMockDBContext();

            //act
            var result = sut.Database.IsInMemory();

            //assert
            result.Should().Be(true);
            sut.DisposeAsync();
        }



    }
}