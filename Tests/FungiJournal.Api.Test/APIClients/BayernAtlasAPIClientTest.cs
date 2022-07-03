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
using FungiJournal.API.APIClients;

namespace FungiJournal.Api.Test.APIClients
{
    public class BayernAtlasAPIClientTest
    {
        //create a test that checks if the uri is correct
        [Fact]
        public async Task TestIfIsNatureReservoir()
        {
            //arrange
            var sut = new BayernAtlasAPIClient();

            //act
            var result = await sut.IsNatureReservoir(47.3835, 10.87069441716961116);
            
            //assert
            result.Should().Be(true);
        }
    }
}

