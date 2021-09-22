using AutoFixture;
using Moq;
using NUnit.Framework;
using SpaceObjectsApi.Controllers;
using SpaceObjectsApi.Models;
using SpaceObjectsApi.Repository.Interface;
using System.Threading.Tasks;

namespace SpaceObjectsApi.Test.Controllers
{
    [TestFixture]
    class PlanetsControllerTest
    {
        private PlanetsController _controller;
        private Mock<IRepository> _repository;
        private int _id;
        private Planet _planet;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new();
            _repository = new();
        }

        [Test]
        public async Task GetItemAsync_Planet_TimesOnce()
        {
            // Arrange    
            _id = _fixture.Create<int>();
            _controller = new(_repository.Object);

            //Act
            await _controller.GetAsync(_id);

            //Assert
            _repository.Verify(r => r.GetAsync<Planet>(_id), Times.Once);
        }

        [Test]
        public async Task GetListAsync_Planet_TimesOnce()
        {
            // Arrange    
            _controller = new(_repository.Object);

            //Act
            await _controller.GetAsync();

            //Assert
            _repository.Verify(r => r.GetAsync<Planet>(), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_Planet_TimesOnce()
        {
            // Arrange
            var planet = new Planet();

            _repository.Setup(r => r.GetAsync<Planet>(planet.Id)).ReturnsAsync(planet);
            _controller = new(_repository.Object);

            //Act
            await _controller.DeleteAsync(planet.Id);

            //Assert
            _repository.Verify(r => r.RemoveAsync<Planet>(planet), Times.Once);
        }

        [Test]
        public async Task CreateAsync_Planet_TimesOnce()
        {
            // Arrange 
            _planet = _fixture.Create<Planet>();
            _controller = new(_repository.Object);

            //Act
            await _controller.CreateAsync(_planet);

            //Assert
            _repository.Verify(r => r.AddAsync(_planet), Times.Once);
        }

        [Test]
        public async Task PutAsync_Planet_TimesOnce()
        {
            // Arrange  
            var planet = _fixture.Create<Planet>();

            _repository.Setup(r => r.GetAsync<Planet>(planet.Id)).ReturnsAsync(_planet);

            _controller = new(_repository.Object);

            //Act
            await _controller.UpdateAsync(planet);

            //Assert 
            _repository.Verify(r => r.UpdateAsync(planet), Times.Once);
        }
    }
}



