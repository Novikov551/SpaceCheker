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
    class BlackHolesControllerTest
    {
        private BlackHolesController _controller;
        private Mock<IRepository> _repository;
        private int _id;
        private BlackHole _blackHole;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new();
            _repository = new();
        }

        [Test]
        public async Task GetItemAsync_BlackHole_TimesOnce()
        {
            // Arrange    
            _id = _fixture.Create<int>();
            _controller = new(_repository.Object);

            //Act
            await _controller.GetAsync(_id);

            //Assert
            _repository.Verify(r => r.GetAsync<BlackHole>(_id), Times.Once);
        }

        [Test]
        public async Task GetListAsync_BlackHole_TimesOnce()
        {
            // Arrange    
            _controller = new(_repository.Object);

            //Act
            await _controller.GetAsync();

            //Assert
            _repository.Verify(r => r.GetAsync<BlackHole>(), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_BlackHole_TimesOnce()
        {
            // Arrange    
            var blackHole = new BlackHole();

            _repository.Setup(r => r.GetAsync<BlackHole>(blackHole.Id)).ReturnsAsync(blackHole);
            _controller = new(_repository.Object);

            //Act
            await _controller.DeleteAsync(blackHole.Id);

            //Assert
            _repository.Verify(r => r.RemoveAsync(blackHole), Times.Once);
        }

        [Test]
        public async Task CreateAsync_BlackHole_TimesOnce()
        {
            // Arrange 
            _blackHole = _fixture.Create<BlackHole>();
            _controller = new(_repository.Object);

            //Act
            await _controller.CreateAsync(_blackHole);

            //Assert
            _repository.Verify(r => r.AddAsync(_blackHole), Times.Once);
        }

        [Test]
        public async Task PutAsync_BlackHole_TimesOnce()
        {
            // Arrange  
            var blackHole = _fixture.Create<BlackHole>();

            _repository.Setup(r => r.GetAsync<BlackHole>(blackHole.Id)).ReturnsAsync(_blackHole);

            _controller = new(_repository.Object);

            //Act
            await _controller.UpdateAsync(blackHole);

            //Assert 
            _repository.Verify(r => r.UpdateAsync(blackHole), Times.Once);
        }
    }
}



