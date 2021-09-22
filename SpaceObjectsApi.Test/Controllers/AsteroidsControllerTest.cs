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
    class AsteroidsControllerTest
    {
        private AsteroidsController _controller;
        private Mock<IRepository> _repository;
        private int _id;
        private Asteroid _asteroid;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new();
            _repository = new();
        }

        [Test]
        public async Task GetItemAsync_Asteroid_TimesOnce()
        {
            // Arrange    
            _id = _fixture.Create<int>();
            _controller = new(_repository.Object);

            //Act
            await _controller.GetAsync(_id);

            //Assert

            _repository.Verify(r => r.GetAsync<Asteroid>(_id), Times.Once);
        }

        [Test]
        public async Task GetListAsync_Asteroid_TimesOnce()
        {
            // Arrange    
            _controller = new(_repository.Object);

            //Act
            await _controller.GetAsync();

            //Assert
            _repository.Verify(r => r.GetAsync<Asteroid>(), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_Asteroid_TimesOnce()
        {
            // Arrange    
            var asteroid = new Asteroid();
            _repository.Setup(r => r.GetAsync<Asteroid>(asteroid.Id)).ReturnsAsync(asteroid);
            _controller = new(_repository.Object);

            //Act
            await _controller.DeleteAsync(asteroid.Id);

            //Assert
            _repository.Verify(r => r.RemoveAsync<Asteroid>(asteroid), Times.Once);
        }

        [Test]
        public async Task CreateAsync_Asteroid_TimesOnce()
        {
            // Arrange 
            _asteroid = _fixture.Create<Asteroid>();
            _controller = new(_repository.Object);

            //Act
            await _controller.CreateAsync(_asteroid);

            //Assert
            _repository.Verify(r => r.AddAsync(_asteroid), Times.Once);
        }

        [Test]
        public async Task PutAsync_Asteroid_TimesOnce()
        {
            // Arrange  
            var asteroid = _fixture.Create<Asteroid>();
            _controller = new(_repository.Object);

            //Act
            await _controller.UpdateAsync(asteroid);

            //Assert 
            _repository.Verify(r => r.UpdateAsync(asteroid), Times.Once);
        }
    }
}



