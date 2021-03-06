using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using FungiJournal.DataAccess.Importer;
using System;
using System.Threading.Tasks;
using FungiJournal.Domain.Models;
using System.Collections.Generic;
using System.IO;

namespace FungiJournal.DataAccess.Test
{
    public class FungiImporterTest
    {

        [Fact]
        public void TestIfUmlauteGetReadCorrectly()
        {
            //arrange
            List<Fungi> importedFungis = FungiImporter.Read(@"./Importer/pilze_small.txt");

            //act
            var result = importedFungis[0].CommonName;

            //assert
            result.Should().Contain("Schwarzschuppiger Birkenröhrling");
        }

        [Fact]
        public void TestIfFirstFungiIsReadCorrectly()
        {
            //first fungi: "Birkenrotkappe, Heiderotkappe, Schwarzschuppiger Birkenröhrling"

            //arrange
            List<Fungi> importedFungis = FungiImporter.Read(@"./Importer/pilze_small.txt");

            //act
            var result = importedFungis[0].CommonName;

            //assert
            result.Should().Contain("Birkenrotkappe");
        }

        [Fact]
        public void TestIfAllEntriesAreRead()
        {
            //file contains 48 entries

            //arrange
            List<Fungi> importedFungis = FungiImporter.Read(@"./Importer/pilze_small.txt");

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
