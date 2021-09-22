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
    class StarsControllerTest
    {
        private StarsController _controller;
        private Mock<IRepository> _repository;
        private int _id;
        private Star _star;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new();
            _repository = new();
        }

        [Test]
        public async Task GetItemAsync_Star_TimesOnce()
        {
            // Arrange    
            _id = _fixture.Create<int>();
            _controller = new(_repository.Object);

            //Act
            await _controller.GetAsync(_id);

            //Assert
            _repository.Verify(r => r.GetAsync<Star>(_id), Times.Once);
        }

        [Test]
        public async Task GetListAsync_Star_TimesOnce()
        {
            // Arrange    
            _controller = new(_repository.Object);

            //Act
            await _controller.GetAsync();

            //Assert
            _repository.Verify(r => r.GetAsync<Star>(), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_Star_TimesOnce()
        {
            // Arrange    
            var star = new Star();
            _repository.Setup(r => r.GetAsync<Star>(star.Id)).ReturnsAsync(star);
            _controller = new(_repository.Object);

            //Act
            await _controller.DeleteAsync(_id);

            //Assert
            _repository.Verify(r => r.RemoveAsync<Star>(star), Times.Once);
        }

        [Test]
        public async Task CreateAsync_Star_TimesOnce()
        {
            // Arrange 
            _star = _fixture.Create<Star>();
            _controller = new(_repository.Object);

            //Act
            await _controller.CreateAsync(_star);

            //Assert
            _repository.Verify(r => r.AddAsync(_star), Times.Once);
        }

        [Test]
        public async Task PutAsync_Star_TimesOnce()
        {
            // Arrange  
            var star = _fixture.Create<Star>();

            _repository.Setup(r => r.GetAsync<Star>(star.Id)).ReturnsAsync(_star);

            _controller = new(_repository.Object);


            //Act
            await _controller.UpdateAsync(star);


            //Assert 
            _repository.Verify(r => r.UpdateAsync(star), Times.Once);
        }
    }
}



