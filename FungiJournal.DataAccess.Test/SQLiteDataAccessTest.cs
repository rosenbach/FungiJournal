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
            var sut = new SQLiteDataAccess(SetupTestDBContext());

            Entry mockEntry = CreateMockEntry();

            //act
            sut.AddEntry(mockEntry);
            var result = await sut.GetEntries();

            //assert
            result.Should().BeEquivalentTo(
                new[] { mockEntry },
                options => options.Excluding(x => x.EntryId));
        
        }


        [Fact]
        public async void TestIfEntryWasDeletedAsync()
        {
            //arrange
            var sut = new SQLiteDataAccess(SetupTestDBContext());

            Entry mockEntry = CreateMockEntry();
            sut.AddEntry(mockEntry);
            var entries = await sut.GetEntries();
            int entriesCount = entries.Count;

            //act
            sut.DeleteEntry(mockEntry.EntryId);
            entries = await sut.GetEntries();
            var result = entries.Count;



            //assert
            result.Should().Be(entriesCount - 1);
            sut.Dispose();
        }

        [Fact]
        public void TestIfDatabaseIsInMemory()
        {
            //arrange
            var sut = SetupTestDBContext();

            //act
            var result = sut.Database.IsInMemory();

            //assert
            result.Should().Be(true);
        }


        private Entry CreateMockEntry()
        {
            return new Entry { EntryId = 1, Description = "Mock Entry" };
        }

        private CodeFirstDbContext SetupTestDBContext()
        {
            var options = Options.Create(new DataAccessConfiguration { UseInMemoryDatabase = true });
            var dbContextOptions = new DbContextOptionsBuilder<CodeFirstDbContext>().Options;
            return new CodeFirstDbContext(dbContextOptions, options);
        }
    }
}