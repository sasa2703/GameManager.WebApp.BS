using GameManager.WebApp.BS.Entities.Models;
using GameManager.WebApp.BS.Repository;
using GameManager.WebApp.BS.Shared.RequestFeatures;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace GameManager.WebApp.BS.Presentation.Tests.Repositories
{
    public class SqliteInMemoryGameColectionRepositoryTest : IDisposable
    {
        private readonly DbConnection _connection;
        private readonly DbContextOptions<RepositoryContext> _contextOptions;

        #region ConstructorAndDispose
        public SqliteInMemoryGameColectionRepositoryTest()
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
FROM GamesCollections;"
                ;
                viewCommand.ExecuteNonQuery();
            }

            var gameCategory = new GameCategory { Id = 1, Name = "PC" };

            context.Add(gameCategory);
            context.SaveChanges();

            var listOfGames = new List<Game>()
            {
 new Game { DisplayName = "DOOM", Id = 1, DisplayIndex = "a1", ReleaseDateOfGame = DateTime.Now.AddDays(-1), Category = gameCategory },
              new Game { DisplayName = "Tetris", Id = 2, DisplayIndex = "a2", ReleaseDateOfGame = DateTime.Now.AddDays(-1), Category = gameCategory }
        };

            context.AddRange(listOfGames);
            context.SaveChanges();

            context.AddRange(
                new GameCollection { DisplayName = "Collection 1", Id = 1, DisplayIndex = "c1", Games = listOfGames },
                new GameCollection { DisplayName = "Collection 2", Id = 2, DisplayIndex = "c2", Games = listOfGames });
            context.SaveChanges();
        }

        RepositoryContext CreateContext() => new RepositoryContext(_contextOptions);

        public void Dispose() => _connection.Dispose();
        #endregion

        [Fact]
        public async void GetGameCollection_Return_OK()
        {
            // Arrange
            using var context = CreateContext();
            var gameRepository = new GameCollectionRepository(context);

            //Act
            var game = await gameRepository.GetGameCollectionAsync(1, false);

            //Assert
            Assert.Equal("Collection 1", game.DisplayName);
        }

        [Fact]
        public async void GetAllGameCollections_Return_OK()
        {
            // Arrange
            using var context = CreateContext();
            var gameRepository = new GameCollectionRepository(context);
            var gameParameters = new GameParameters() { OnlyAvailable = true, PageNumber = 0, PageSize = 10 };

            // Act
            var games = await gameRepository.GetAllGamesCollectionAsync(gameParameters, false);

            // Assert
            Assert.Collection(
           games,
           b => Assert.Equal("Collection 1", b.DisplayName),
           b => Assert.Equal("Collection 2", b.DisplayName));
        }

        [Fact]
        public void AddGameCollection_Returns_OK()
        {
            // Arrange
            using var context = CreateContext();
            var gameRepository = new GameCollectionRepository(context);
            var gameCollection = new GameCollection { DisplayName = "Collection 3", Id = 3, DisplayIndex = "c3" };

            //Act
            gameRepository.CreateGameCollection(gameCollection);
            context.SaveChanges();

            //Assert
            gameCollection = context.GameCollection.Single(b => b.DisplayIndex == "c3");
            Assert.Equal("Collection 3", gameCollection.DisplayName);
        }

        [Fact]
        public async void UpdateGameCollection_Return_OK()
        {
            // Arrange
            using var context = CreateContext();
            var gameRepository = new GameCollectionRepository(context);

            // Act
            var gameToUpdate = await gameRepository.GetGameCollectionAsync(1, false);
            gameToUpdate.DisplayName = "New Collection 1";
            gameRepository.UpdateGameCollecton(gameToUpdate);
            context.SaveChanges();


            //Assert
            var game = context.GameCollection.Single(b => b.Id == 1);
            Assert.Equal("New Collection 1", game.DisplayName);
        }

        [Fact]
        public async void DeleteGameCollection_Return_OK()
        {
            // Arrange
            using var context = CreateContext();
            var gameRepository = new GameCollectionRepository(context);

            //Act
            await gameRepository.DeleteGameCollectionAsync(1);
            context.SaveChanges();

            //Assert
            var game = context.GameCollection.SingleOrDefault(b => b.Id == 1);
            Assert.Equal(null, game);

        }
    }
}
