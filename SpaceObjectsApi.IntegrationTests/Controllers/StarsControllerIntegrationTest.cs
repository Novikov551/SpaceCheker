using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SpaceObjectsApi.Controllers;
using SpaceObjectsApi.IntegrationTests;
using SpaceObjectsApi.Models;
using SpaceObjectsApi.Repository;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SpaceObjectsApi.Test.Controllers
{
    [TestFixture]
    class StarsControllerIntegrationTest
    {
        private StarsController _controller;
        private DatabaseFixture _dbFixture;
        private SpaceObjectRepository _repository;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _dbFixture = new();
            DatabaseInitializer.InitializeDataBase(_dbFixture.Context);
            _repository = new(_dbFixture.Context);
            _controller = new(_repository);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            DatabaseInitializer.ReInitializeDataBase(_dbFixture.Context);
        }

        [Test]
        public async Task GetAsync_Should_SendAGetRequestAndReturnStar()
        {
            var expectedObject = _dbFixture.Context.Stars.FirstOrDefault();
            var result = await _controller.GetAsync(expectedObject.Id);
            var objResult = (OkObjectResult)result.Result;
            var star = objResult.Value;

            Assert.AreEqual(expectedObject, star);
            Assert.AreEqual((int)HttpStatusCode.OK, objResult.StatusCode);

        }

        [Test]
        public async Task GetAsync_Should_SendAGetRequestAndReturnStarsList()
        {
            var starsListExpected = await _repository.GetAsync<Star>();
            var result = await _controller.GetAsync();
            var objResult = (OkObjectResult)result.Result;
            var starsListResult = objResult.Value;

            Assert.AreEqual(starsListResult, starsListExpected);
            Assert.AreEqual((int)HttpStatusCode.OK, objResult.StatusCode);
        }

        [Test]
        public async Task CreateAsync_Should_SendAPostRequestAndReturnCreatedStarId()
        {
            var name = "TestObject";
            var star = new Star() { Name = name, DistanceToEarth = 20, Description = "" };
            var result = await _controller.CreateAsync(star);
            var codeResult = (OkObjectResult)result;
            var expectedObject = _dbFixture.Context.Stars.FirstOrDefault(s => s.Name == name);

            Assert.AreEqual((int)HttpStatusCode.OK, codeResult.StatusCode);
            Assert.AreEqual(expectedObject, star);
        }

        [Test]
        public async Task UpdateAsync_Should_SendAPutRequestAndReturnUpdatedStar()
        {
            var expectedObject = _dbFixture.Context.Stars.FirstOrDefault();

            expectedObject.Name = "TestName";

            var result = await _controller.UpdateAsync(expectedObject);
            var CodeResult = (OkObjectResult)result;
            var objectResult = await _repository.GetAsync<Star>(expectedObject.Id);

            Assert.AreEqual((int)HttpStatusCode.OK, CodeResult.StatusCode);
            Assert.AreEqual(expectedObject.Name, objectResult.Name);
        }

        [Test]
        public async Task DeleteAsync_Should_SendADeleteRequestAndReturnNull()
        {
            var expectedObject = _dbFixture.Context.Stars.FirstOrDefault();
            var result = await _controller.DeleteAsync(expectedObject.Id);
            var codeResult = (OkResult)result;
            var star = await _repository.GetAsync<Planet>(expectedObject.Id);

            Assert.AreEqual((int)HttpStatusCode.OK, codeResult.StatusCode);
            Assert.AreEqual(star, null);
        }
    }
}
