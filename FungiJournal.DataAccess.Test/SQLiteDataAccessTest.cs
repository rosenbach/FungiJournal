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
        public void TestIfEntryWasAdded()
        {
            //arrange
            var sut = new SQLiteDataAccess(SetupTestDBContext());

            Entry mockEntry = CreateMockEntry();

            //act
            sut.AddEntry(mockEntry);
            var result = sut.LoadEntries();

            //assert
            result.Should().BeEquivalentTo(
                new[] { mockEntry },
                options => options.Excluding(x => x.EntryId));
        }


        [Fact]
        public void TestIfEntryWasDeleted()
        {
            //arrange
            var sut = new SQLiteDataAccess(SetupTestDBContext());

            Entry mockEntry = CreateMockEntry();
            sut.AddEntry(mockEntry);
            int entriesCount = sut.LoadEntries().Count;

            //act
            sut.DeleteEntry(mockEntry.EntryId);
            var result = sut.LoadEntries().Count;

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
            Random rnd = new Random();
            return new Entry { EntryId = rnd.Next(), Description = "Mock Entry" };
        }

        private CodeFirstDbContext SetupTestDBContext()
        {
            var options = Options.Create(new DataAccessConfiguration { UseInMemoryDatabase = true });
            var dbContextOptions = new DbContextOptionsBuilder<CodeFirstDbContext>().Options;
            return new CodeFirstDbContext(dbContextOptions, options);
        }
    }
}