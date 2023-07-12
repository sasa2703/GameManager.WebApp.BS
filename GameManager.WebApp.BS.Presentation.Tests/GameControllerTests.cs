using GameManager.WebApp.BS.Authorization.Interfaces;
using GameManager.WebApp.BS.Contracts;
using GameManager.WebApp.BS.Entities.Models;
using GameManager.WebApp.BS.Presentation.Controllers;
using GameManager.WebApp.BS.Service.Contracts;
using GameManager.WebApp.BS.Shared.DataTransferObjects.Product;
using GameManager.WebApp.BS.Shared.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Reflection.Metadata;

namespace GameManager.WebApp.BS.Presentation.Tests
{
    public class GameControllerTests
    {
        [Fact]
        public async void GetGame_Returns_OK()
        {
            // Arrange
            var repositoryMock = new Mock<IGameRepository>();
            var gameService = new Mock<IGameService>();
            var accessRightResolver = new Mock<IAccessRightsResolver>();

            gameService
                .Setup(r => r.GetGameAsync(1,false))             
                .Returns(Task.FromResult( new GameDto{ DisplayName = "DOOM", DisplayIndex = "a1" }));

            var controller = new GamesController(gameService.Object, accessRightResolver.Object);

            // Act
            var game =  await controller.GetGame(1);
            var result = game as OkObjectResult;
          
            // Assert
            gameService.Verify(r => r.GetGameAsync(1,false));
            Assert.Equal("DOOM", ((GameDto)result.Value).DisplayName);
        }
    }
}