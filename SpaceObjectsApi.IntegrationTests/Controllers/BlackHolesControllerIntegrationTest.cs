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
    class BlackHolesControllerIntegrationTest
    {
        private BlackHolesController _controller;
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
        public async Task GetAsync_Should_SendAGetRequestAndReturnBlackHole()
        {
            var expectedObject = _dbFixture.Context.BlackHoles.FirstOrDefault();
            var result = await _controller.GetAsync(expectedObject.Id);
            var objResult = (OkObjectResult)result.Result;
            var blackHole = objResult.Value;

            Assert.AreEqual(expectedObject, blackHole);
            Assert.AreEqual((int)HttpStatusCode.OK, objResult.StatusCode);

        }

        [Test]
        public async Task GetAsync_Should_SendAGetRequestAndReturnBlackHolesList()
        {
            var blackHolesListExpected = await _repository.GetAsync<BlackHole>();
            var result = await _controller.GetAsync();
            var objResult = (OkObjectResult)result.Result;
            var blackHolesListResult = objResult.Value;

            Assert.AreEqual(blackHolesListResult, blackHolesListExpected);
            Assert.AreEqual((int)HttpStatusCode.OK, objResult.StatusCode);
        }

        [Test]
        public async Task CreateAsync_Should_SendAPostRequestAndReturnCreatedBlackHoleId()
        {
            var name = "TestObject";
            var blackHole = new BlackHole() { Name = name, Weight = 20, Description = "" };
            var result = await _controller.CreateAsync(blackHole);
            var codeResult = (OkObjectResult)result;
            var expectedObject = _dbFixture.Context.BlackHoles.FirstOrDefault(s => s.Name == name);

            Assert.AreEqual((int)HttpStatusCode.OK, codeResult.StatusCode);
            Assert.AreEqual(expectedObject, blackHole);
        }

        [Test]
        public async Task UpdateAsync_Should_SendAPutRequestAndReturnUpdatedBlackHole()
        {
            var expectedObject = _dbFixture.Context.BlackHoles.FirstOrDefault();

            expectedObject.Name = "TestName";

            var result = await _controller.UpdateAsync(expectedObject);
            var CodeResult = (OkObjectResult)result;
            var objectResult = await _repository.GetAsync<BlackHole>(expectedObject.Id);

            Assert.AreEqual((int)HttpStatusCode.OK, CodeResult.StatusCode);
            Assert.AreEqual(expectedObject.Name, objectResult.Name);
        }

        [Test]
        public async Task DeleteAsync_Should_SendADeleteRequestAndReturnNull()
        {
            var expectedObject = _dbFixture.Context.BlackHoles.FirstOrDefault();
            var result = await _controller.DeleteAsync(expectedObject.Id);
            var codeResult = (OkResult)result;
            var blackHole = await _repository.GetAsync<BlackHole>(expectedObject.Id);

            Assert.AreEqual((int)HttpStatusCode.OK, codeResult.StatusCode);
            Assert.AreEqual(blackHole, null);
        }
    }
}
