using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using FungiJournal.DataAccess.Importer;
using System;
using System.Threading.Tasks;
using FungiJournal.Domain.Models;
using System.Collections.Generic;

namespace FungiJournal.DataAccess.Test
{
    public class FungiImporterTest
    {

        [Fact]
        public void TestIfFirstFungiIsReadCorrectly()
        {
            //first fungi: "Birkenrotkappe, Heiderotkappe, Schwarzschuppiger Birkenröhrling"

            //arrange
            FungiImporter fungiImporter = new();
            List<Fungi> importedFungis = fungiImporter.Read(@"C:\Users\m_kae\source\repos\FungiJournal\FungiJournal.DataAccess\Importer\pilze_small.txt");

            //act
            var result = importedFungis[0].Name;

            //assert
            result.Should().Contain("Birkenrotkappe");
        }

        [Fact]
        public void TestIfAllEntriesAreRead()
        {
            //file contains 48 entries

            //arrange
            FungiImporter fungiImporter = new();
            List<Fungi> importedFungis = fungiImporter.Read(@"C:\Users\m_kae\source\repos\FungiJournal\FungiJournal.DataAccess\Importer\pilze_small.txt");

            //act
            var result = importedFungis.Count;

            //assert
            result.Should().Be(48);
        }
        
        [Fact]
        public async Task TestIfFungiWasAdded()
        {
            //arrange
            using var sut = new SQLiteDataAccess(DataAccessMock.CreateMockDBContext());
            Fungi mockFungi = DataAccessMock.CreateMockFungi();

            //act
            await sut.AddFungiAsync(mockFungi);
            var result = await sut.GetFungisAsync();

            //assert
            result.Should().BeEquivalentTo(
                new[] { mockFungi }
                );
        }
    }

}
