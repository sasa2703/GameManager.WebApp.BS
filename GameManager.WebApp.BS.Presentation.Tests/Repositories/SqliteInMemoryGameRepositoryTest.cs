using AutoMapper;
using GameManager.WebApp.BS.Contracts;
using GameManager.WebApp.BS.Entities.Models;
using GameManager.WebApp.BS.Repository;
using GameManager.WebApp.BS.Service;
using GameManager.WebApp.BS.Service.Contracts;
using GameManager.WebApp.BS.Shared.RequestFeatures;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Data.Common;

namespace GameManager.WebApp.BS.Presentation.Tests.Repositories
{
    public class SqliteInMemoryGameRepositoryTest : IDisposable
    {
        private readonly DbConnection _connection;
        private readonly DbContextOptions<RepositoryContext> _contextOptions;

        #region ConstructorAndDispose
        public SqliteInMemoryGameRepositoryTest()
        {
            // Create and open a connection. This creates the SQLite in-memory database, which will persist until the connection is closed
            // at the end of the test (see Dispose below).
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            // These options will be used by the context instances in this test suite, including the connection opened above.
            _contextOptions = new DbContextOptionsBuilder<RepositoryContext>().UseSqlite(_connection).Options;


            // Create the schema and seed some data
            using var context = new RepositoryContext(_contextOptions);

            if (context.Database.EnsureCreated())
            {
                using var viewCommand = context.Database.GetDbConnection().CreateCommand();
                viewCommand.CommandText = @"
CREATE VIEW AllResources AS
SELECT Url
FROM Games;"
                ;
                viewCommand.ExecuteNonQuery();
            }
            var gameCategory = new GameCategory { Id = 1, Name = "PC" };

            context.Add(gameCategory);
            context.SaveChanges();



            context.AddRange(
                new Game { DisplayName = "DOOM", Id = 1, DisplayIndex = "a1", ReleaseDateOfGame = DateTime.Now.AddDays(-1), Category = gameCategory },
                new Game { DisplayName = "Tetris", Id = 2, DisplayIndex = "a2", ReleaseDateOfGame = DateTime.Now.AddDays(-1), Category = gameCategory });
            context.SaveChanges();
        }

        RepositoryContext CreateContext() => new RepositoryContext(_contextOptions);

        public void Dispose() => _connection.Dispose();
        #endregion


        [Fact]
        public async void GetGame_Return_OK()
        {
            // Arrange
            using var context = CreateContext();
            var gameRepository = new GameRepository(context);

            //Act
            var game = await gameRepository.GetGameAsync(1, false);

            //Assert
            Assert.Equal("DOOM", game.DisplayName);
        }

        [Fact]
        public async void GetAllGames_Return_OK()
        {
            // Arrange
            using var context = CreateContext();
            var gameRepository = new GameRepository(context);
            var gameParameters = new GameParameters() { OnlyAvailable = true, PageNumber = 0, PageSize = 10 };

            // Act
            var games = await gameRepository.GetAllGamesAsync(gameParameters, false);

            // Assert
            Assert.Collection(
           games,
           b => Assert.Equal("DOOM", b.DisplayName),
           b => Assert.Equal("Tetris", b.DisplayName));
        }

        [Fact]
        public void AddGame_Returns_OK()
        {
            // Arrange
            using var context = CreateContext();
            var gameRepository = new GameRepository(context);
            var gameCategory = new GameCategory { Id = 2, Name = "Mobile" };
            var game = new Game() { Id = 3, DisplayName = "Lara Croft", DisplayIndex = "b1", ReleaseDateOfGame = DateTime.Now.AddDays(-2), Category = gameCategory };

            //Act
            gameRepository.CreateGame(game);
            context.SaveChanges();

            //Assert
            game = context.Game.Single(b => b.DisplayIndex == "b1");
            Assert.Equal("Lara Croft", game.DisplayName);
        }

        [Fact]
        public async void UpdateGame_Return_OK()
        {
            // Arrange
            using var context = CreateContext();
            var gameRepository = new GameRepository(context);

            // Act
            var gameToUpdate = await gameRepository.GetGameAsync(1, false);
            gameToUpdate.DisplayName = "DOOM 2";
            gameRepository.UpdateGame(gameToUpdate);
            context.SaveChanges();


            //Assert
            var game = context.Game.Single(b => b.Id == 1);
            Assert.Equal("DOOM 2", game.DisplayName);
        }

        [Fact]
        public async void DeleteGame_Return_OK()
        {
            // Arrange
            using var context = CreateContext();
            var gameRepository = new GameRepository(context);

            //Act
            await gameRepository.DeleteGameAsync(1);
            context.SaveChanges();

            //Assert
            var game = context.Game.SingleOrDefault(b => b.Id == 1);
            Assert.Equal(null, game);

        }
    }
}
