using Microsoft.EntityFrameworkCore;
using GameManager.WebApp.BS.Contracts;
using GameManager.WebApp.BS.Entities.Models;
using GameManager.WebApp.BS.Shared.RequestFeatures;

namespace GameManager.WebApp.BS.Repository
{
    internal sealed class GameRepository : RepositoryBase<Game>, IGameRepository
    {
        public GameRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task<PagedList<Game>> GetAllGamesAsync(GameParameters gameParameters, bool trackChanges)
        {
            var games = FindAll(trackChanges);
           

            if (gameParameters.OnlyAvailable.HasValue && gameParameters.OnlyAvailable.Value)
            {
                games = games.Where(p => p.ReleaseDateOfGame > DateTime.Now);
            }

            games = games.OrderBy(p => p.DisplayIndex);

            return await PagedList<Game>
                .ToPageListAsync(games, gameParameters.PageNumber, gameParameters.PageSize);
        }

        public async Task<Game> GetGameAsync(int gameId, bool trackChanges)
        {
            var game = await FindByCondition(p => p.Id == gameId, trackChanges).SingleOrDefaultAsync();

            return game;
        }

        public async Task<Game> GetGameAsync(string gameIndex, bool trackChanges)
        {
            var game = await FindByCondition(p => p.DisplayIndex == gameIndex, trackChanges).SingleOrDefaultAsync();

            return game;
        }

        public async Task DeleteGameAsync(int gameId)
        {
            var game = await FindByCondition(p => p.Id == gameId, true).SingleOrDefaultAsync();
            if (game != null)
            {
                Delete(game);
            }
        }


        public async Task<IEnumerable<Game>> GetAllGamesBySubscriptionIdAsync(string subscriptionId, bool trackChanges)
        {
            return await FindAll(trackChanges).ToListAsync();
        }

        public async Task<List<Game>> GetAllAvailableGames(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderByDescending(x => x.ReleaseDateOfGame).ToListAsync();
        }

    }
}
