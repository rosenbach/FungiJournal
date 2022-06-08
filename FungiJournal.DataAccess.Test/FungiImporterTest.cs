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
            List<Fungi> importedFungis = FungiImporter.Read(@"C:\Users\m_kae\source\repos\FungiJournal\FungiJournal.DataAccess\Importer\pilze_small.txt");

            //act
            var result = importedFungis[0].Name;

            //assert
            result.Should().Contain("Birkenrotkappe");
        }

        [Fact]
        public void TestIfAllEntriesAreRead()
        {
            //file contains 58 entries

            //arrange
            List<Fungi> importedFungis = FungiImporter.Read(@"C:\Users\m_kae\source\repos\FungiJournal\FungiJournal.DataAccess\Importer\pilze_small.txt");

            //act
            var result = importedFungis.Count;

            //assert
            result.Should().Be(58);
        }
    }

}
