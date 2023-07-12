using AutoMapper;
using FiscalCloud.WebApp.BS.API.Mapping;
using GameManager.WebApp.BS.Authorization.Interfaces;
using GameManager.WebApp.BS.Contracts;
using GameManager.WebApp.BS.Entities.Models;
using GameManager.WebApp.BS.Service.Contracts;
using GameManager.WebApp.BS.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameManager.WebApp.BS.Mapping;

namespace GameManager.WebApp.BS.Presentation.Tests.Services
{
    public class GameCollectionServiceTests
    {
        [Fact]
        public async void GetGameCollection_Returns_OK()
        {
            // Arrange
            var repositoryMock = new Mock<IRepositoryManager>();
            var gameService = new Mock<IGameService>();
            var accessRightResolver = new Mock<IAccessRightsResolver>();
            var profile = new GameCollectionProfiles();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);

            repositoryMock
                .Setup(r => r.GameCollection.GetGameCollectionAsync(1, false))
                .Returns(Task.FromResult(new GameCollection { DisplayName = "Collection 1", DisplayIndex = "c1", Id = 1 }));

            var service = new GameCollectionService(repositoryMock.Object, mapper);
            // Act
            var gameCollection = await service.GetGameCollectionAsync(1, false);

            // Assert

            Assert.Equal("Collection 1", gameCollection.DisplayName);
        }
    }
}
