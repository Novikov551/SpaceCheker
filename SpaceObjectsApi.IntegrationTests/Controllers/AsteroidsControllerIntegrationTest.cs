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
    class AsteroidsControllerIntegrationTest
    {
        private AsteroidsController _controller;
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
        public async Task GetAsync_Should_SendAGetRequestAndReturnAsteroid()
        {
            var asteroidList = await _repository.GetAsync<Asteroid>();
            var expectedAsteroid = asteroidList.FirstOrDefault();
            var result = await _controller.GetAsync(expectedAsteroid.Id);
            var objResult = (OkObjectResult)result.Result;
            var asteroid = objResult.Value;

            Assert.AreEqual(expectedAsteroid, asteroid);
            Assert.AreEqual((int)HttpStatusCode.OK, objResult.StatusCode);

        }

        [Test]
        public async Task GetAsync_Should_SendAGetRequestAndReturnAsteroidList()
        {
            var asteroidListExpected = await _repository.GetAsync<Asteroid>();
            var result = await _controller.GetAsync();
            var objResult = (OkObjectResult)result.Result;
            var asteroidListResult = objResult.Value;

            Assert.AreEqual(asteroidListResult, asteroidListExpected);
            Assert.AreEqual((int)HttpStatusCode.OK, objResult.StatusCode);
        }

        [Test]
        public async Task CreateAsync_Should_SendAPostRequestAndReturnCreatedAsteroidId()
        {
            var name = "TestObject";
            var asteroid = new Asteroid() { Name = name, Diameter = 20, Description = "" };
            var result = await _controller.CreateAsync(asteroid);
            var codeResult = (OkObjectResult)result;
            var expectedAsteroid = _dbFixture.Context.Asteroids.FirstOrDefault(s => s.Name == name);

            Assert.AreEqual((int)HttpStatusCode.OK, codeResult.StatusCode);
            Assert.AreEqual(expectedAsteroid, asteroid);
        }

        [Test]
        public async Task UpdateAsync_Should_SendAPutRequestAndReturnUpdatedAsteroid()
        {
            var expectedAsteroid = _dbFixture.Context.Asteroids.FirstOrDefault();

            expectedAsteroid.Name = "TestName";

            var result = await _controller.UpdateAsync(expectedAsteroid);
            var CodeResult = (OkObjectResult)result;
            var asteroidResult = await _repository.GetAsync<Asteroid>(expectedAsteroid.Id);

            Assert.AreEqual((int)HttpStatusCode.OK, CodeResult.StatusCode);
            Assert.AreEqual(expectedAsteroid.Name, asteroidResult.Name);
        }

        [Test]
        public async Task DeleteAsync_Should_SendADeleteRequestAndReturnNull()
        {
            var expectedAsteroid = _dbFixture.Context.Asteroids.FirstOrDefault();
            var result = await _controller.DeleteAsync(expectedAsteroid.Id);
            var codeResult = (OkResult)result;
            var asteroid = await _repository.GetAsync<Asteroid>(expectedAsteroid.Id);

            Assert.AreEqual((int)HttpStatusCode.OK, codeResult.StatusCode);
            Assert.AreEqual(asteroid, null);
        }
    }
}
