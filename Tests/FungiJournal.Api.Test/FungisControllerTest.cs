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
    public class FungisControllerTest
    {
        [Fact]
        public async Task TestIfGetFungiByIdWasSuccessful()
        {
            //arrange
            var sqLiteDataAccess = new SQLiteDataAccess(DataAccessMock.CreateMockDBContext());
            var sut = new FungisController(sqLiteDataAccess);
            Fungi mockFungi = DataAccessMock.CreateMockFungi();
            await sut.PostFungi(mockFungi);
            var expected = mockFungi;

            //act
            var result = await sut.GetById(mockFungi.FungiId);

            //assert
            result.Should().BeOfType<OkObjectResult>();
            var typedResult = result as OkObjectResult;

            var fungies = typedResult?.Value;
            fungies.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task TestIfGetAllFungisWasSuccessful()
        {
            //arrange
            var sqLiteDataAccess = new SQLiteDataAccess(DataAccessMock.CreateMockDBContext());
            var sut = new FungisController(sqLiteDataAccess);
            Fungi mockFungi = DataAccessMock.CreateMockFungi();
            Fungi mockFungi2 = DataAccessMock.CreateMockFungi();
            await sut.PostFungi(mockFungi);
            await sut.PostFungi(mockFungi2);
            var expected = new[]
            {
                mockFungi,
                mockFungi2
            };

            //act
            var result = await sut.GetAll();

            //assert
            result.Should().BeOfType<OkObjectResult>();
            var typedResult = result as OkObjectResult;

            var fungies = typedResult?.Value;
            fungies.Should().BeEquivalentTo(expected);

        }

        [Fact]
        public async Task TestIfGetAllFungisByFungiIdQueryWasSuccessful()
        {
            //arrange
            var sqLiteDataAccess = new SQLiteDataAccess(DataAccessMock.CreateMockDBContext());
            var sut = new FungisController(sqLiteDataAccess);
            Fungi mockFungi = DataAccessMock.CreateMockFungi();
            Fungi mockFungi2 = DataAccessMock.CreateMockFungi();

            await sut.PostFungi(mockFungi);
            await sut.PostFungi(mockFungi2);

            FungiQueryParameters queryParameters = new();
            int idToSearchFor = mockFungi2.FungiId;
            queryParameters.FungiId = idToSearchFor;

            var expected = new[]
            {
                mockFungi2
            };


            //act
            var result = await sut.GetAll(queryParameters);

            //assert
            result.Should().BeOfType<OkObjectResult>();
            var typedResult = result as OkObjectResult;

            var fungies = typedResult?.Value;
            fungies.Should().BeEquivalentTo(expected);

        }

        [Fact]
        public async Task TestIfGetAllFungisByDescriptionQueryWasSuccessful()
        {
            //arrange
            var sqLiteDataAccess = new SQLiteDataAccess(DataAccessMock.CreateMockDBContext());
            var sut = new FungisController(sqLiteDataAccess);
            Fungi mockFungi = DataAccessMock.CreateMockFungi();
            Fungi mockFungi2 = DataAccessMock.CreateMockFungi();
            string NameToSearchFor = "TestDescr";

            mockFungi2.CommonName = NameToSearchFor;

            await sut.PostFungi(mockFungi);
            await sut.PostFungi(mockFungi2);

            FungiQueryParameters queryParameters = new()
            {
                Name = NameToSearchFor
            };

            var expected = new[]
            {
                mockFungi2
            };


            //act
            var result = await sut.GetAll(queryParameters);

            //assert
            result.Should().BeOfType<OkObjectResult>();
            var typedResult = result as OkObjectResult;

            var fungies = typedResult?.Value;
            fungies.Should().BeEquivalentTo(expected);

        }

        [Fact]
        public async Task TestIfFungiWasDeleted()
        {
            //arrange
            var sqLiteDataAccess = new SQLiteDataAccess(DataAccessMock.CreateMockDBContext());
            var sut = new FungisController(sqLiteDataAccess);
            Fungi mockFungi = DataAccessMock.CreateMockFungi();
            Fungi mockFungiToBeDeleted = DataAccessMock.CreateMockFungi();
            await sut.PostFungi(mockFungi);
            await sut.PostFungi(mockFungiToBeDeleted);

            //act
            var result = await sut.DeleteFungi(mockFungiToBeDeleted.FungiId);

            //assert
            result.Should().BeOfType<OkObjectResult>();
            var fungiesResponse = await sut.GetAll() as OkObjectResult;
            var fungies = fungiesResponse?.Value as List<Fungi>;
            //var fungies = await sqLiteDataAccess.GetFungisAsync();
            fungies.Should().ContainSingle();
            var resultFromDatabase = fungies?.Single();
            resultFromDatabase?.FungiId.Should().Be(mockFungi.FungiId);
            resultFromDatabase?.FungiId.Should().NotBe(mockFungiToBeDeleted.FungiId);

            var typedResult = result as OkObjectResult;
            var resultingFungi = typedResult?.Value as Fungi;
            resultingFungi?.FungiId.Should().Be(mockFungiToBeDeleted.FungiId);
            resultingFungi?.FungiId.Should().NotBe(mockFungi.FungiId);
        }

        [Fact]
        public async Task TestIfFungiWasPut()
        {
            //arrange
            var sqLiteDataAccess = new SQLiteDataAccess(DataAccessMock.CreateMockDBContext());
            var sut = new FungisController(sqLiteDataAccess);
            Fungi mockFungi = DataAccessMock.CreateMockFungi();

            await sut.PostFungi(mockFungi);

            string modifiedFungiName = "I was modified";
            mockFungi.CommonName = modifiedFungiName;


            //act
            var result = await sut.PutFungi(mockFungi.FungiId, mockFungi);

            //assert
            var fungies = await sqLiteDataAccess.GetFungisAsync();
            fungies.Should().ContainSingle();
            var resultFromDatabase = fungies.Single();
            resultFromDatabase.CommonName.Should().Be(modifiedFungiName);
            resultFromDatabase.FungiId.Should().Be(mockFungi.FungiId);
        }

        [Fact]
        public async Task TestIfFungiWasPosted()
        {
            //arrange
            var sqLiteDataAccess = new SQLiteDataAccess(DataAccessMock.CreateMockDBContext());
            var sut = new FungisController(sqLiteDataAccess);
            Fungi mockFungi = DataAccessMock.CreateMockFungi();

            //act
            var result = await sut.PostFungi(mockFungi);

            //assert
            result.Should().BeOfType<CreatedAtActionResult>();
            var fungies = await sqLiteDataAccess.GetFungisAsync();
            fungies.Should().ContainSingle();
            var resultFromDatabase = fungies.Single();
            var typedResult = result as CreatedAtActionResult;
            typedResult?.Value.Should().BeEquivalentTo(resultFromDatabase);
            resultFromDatabase.FungiId.Should().NotBe(0);
        }

    }
}
