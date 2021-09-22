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
    class PlanetsControllerIntegrationTest
    {
        private PlanetsController _controller;
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
        public async Task GetAsync_Should_SendAGetRequestAndReturnPlanet()
        {
            var expectedObject = _dbFixture.Context.Planets.FirstOrDefault();
            var result = await _controller.GetAsync(expectedObject.Id);
            var objResult = (OkObjectResult)result.Result;
            var planet = objResult.Value;

            Assert.AreEqual(expectedObject, planet);
            Assert.AreEqual((int)HttpStatusCode.OK, objResult.StatusCode);

        }

        [Test]
        public async Task GetAsync_Should_SendAGetRequestAndReturnPlanetsList()
        {
            var planetsListExpected = await _repository.GetAsync<Planet>();
            var result = await _controller.GetAsync();
            var objResult = (OkObjectResult)result.Result;
            var planetsListResult = objResult.Value;

            Assert.AreEqual(planetsListResult, planetsListExpected);
            Assert.AreEqual((int)HttpStatusCode.OK, objResult.StatusCode);
        }

        [Test]
        public async Task CreateAsync_Should_SendAPostRequestAndReturnCreatedPlanetId()
        {
            var name = "TestObject";
            var planet = new Planet() { Name = name, ExistenceOfLife = true, Description = "" };
            var result = await _controller.CreateAsync(planet);
            var codeResult = (OkObjectResult)result;
            var expectedObject = _dbFixture.Context.Planets.FirstOrDefault(s => s.Name == name);

            Assert.AreEqual((int)HttpStatusCode.OK, codeResult.StatusCode);
            Assert.AreEqual(expectedObject, planet);
        }

        [Test]
        public async Task UpdateAsync_Should_SendAPutRequestAndReturnUpdatedPlanet()
        {
            var expectedObject = _dbFixture.Context.Planets.FirstOrDefault();

            expectedObject.Name = "TestName";

            var result = await _controller.UpdateAsync(expectedObject);
            var CodeResult = (OkObjectResult)result;
            var objectResult = await _repository.GetAsync<Planet>(expectedObject.Id);

            Assert.AreEqual((int)HttpStatusCode.OK, CodeResult.StatusCode);
            Assert.AreEqual(expectedObject.Name, objectResult.Name);
        }

        [Test]
        public async Task DeleteAsync_Should_SendADeleteRequestAndReturnNull()
        {
            var expectedObject = _dbFixture.Context.Planets.FirstOrDefault();
            var result = await _controller.DeleteAsync(expectedObject.Id);
            var codeResult = (OkResult)result;
            var planet = await _repository.GetAsync<Planet>(expectedObject.Id);

            Assert.AreEqual((int)HttpStatusCode.OK, codeResult.StatusCode);
            Assert.AreEqual(planet, null);
        }
    }
}
