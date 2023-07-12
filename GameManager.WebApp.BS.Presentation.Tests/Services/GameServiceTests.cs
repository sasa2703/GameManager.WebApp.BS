using AutoMapper;
using FiscalCloud.WebApp.BS.API.Mapping;
using GameManager.WebApp.BS.Authorization.Interfaces;
using GameManager.WebApp.BS.Contracts;
using GameManager.WebApp.BS.Entities.Models;
using GameManager.WebApp.BS.Presentation.Controllers;
using GameManager.WebApp.BS.Service;
using GameManager.WebApp.BS.Service.Contracts;
using GameManager.WebApp.BS.Shared.DataTransferObjects.Game;
using GameManager.WebApp.BS.Shared.DataTransferObjects.Product;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManager.WebApp.BS.Presentation.Tests.Services
{
    public class GameServiceTests
    {
        [Fact]
        public async void GetGame_Returns_OK()
        {
            // Arrange
            var repositoryMock = new Mock<IRepositoryManager>();
            var gameService = new Mock<IGameService>();
            var accessRightResolver = new Mock<IAccessRightsResolver>();
            var profile = new GameProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);

            repositoryMock
                .Setup(r => r.Game.GetGameAsync(1, false))
                .Returns(Task.FromResult(new Game { DisplayName = "DOOM", DisplayIndex = "a1", Id = 1 }));

            var service = new GameService(repositoryMock.Object, mapper);
            // Act
            var game = await service.GetGameAsync(1, false);

            // Assert
    
            Assert.Equal("DOOM", game.DisplayName);
        }

        [Fact]
        public async void CreateGame_Returns_OK()
        {
            // Arrange
            var repositoryMock = new Mock<IRepositoryManager>();
            var gameService = new Mock<IGameService>();
            var accessRightResolver = new Mock<IAccessRightsResolver>();
            var profile = new GameProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);
            var newGame = new GameForCreationDto(){DisplayIndex = "a2", DisplayName = "NFS", ReleaseDateOfGame = DateTime.UtcNow.AddDays(-3) };

            repositoryMock
                .Setup(r => r.Game.CreateGame(new Game { DisplayName = "NFS", DisplayIndex = "a2", Id = 2, ReleaseDateOfGame = DateTime.UtcNow.AddDays(-3) }));

            var service = new GameService(repositoryMock.Object, mapper);
            // Act
            var game = await service.CreateGameAsync(newGame);

            // Assert

            Assert.Equal("NFS", game.DisplayName);
        }

        [Fact]
        public async void GetAllAvailableGames_Returns_OK()
        {
            // Arrange
            var repositoryMock = new Mock<IRepositoryManager>();
            var gameService = new Mock<IGameService>();
            var accessRightResolver = new Mock<IAccessRightsResolver>();
            var profile = new GameProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);
            var newGame = new GameForCreationDto() { DisplayIndex = "a2", DisplayName = "NFS", ReleaseDateOfGame = DateTime.UtcNow.AddDays(-3) };

            repositoryMock
                .Setup(r => r.Game.GetAllAvailableGames(false))
                .Returns(Task.FromResult(new List<Game> { new Game { DisplayName = "DOOM", DisplayIndex = "a1" } }));

            var service = new GameService(repositoryMock.Object, mapper);
            // Act
            var game = await service.GetPubliclyAvailableGames(false);

            // Assert
            Assert.NotEmpty(game);
            Assert.Equal("DOOM", game[0].DisplayName);
        }
    }
}
