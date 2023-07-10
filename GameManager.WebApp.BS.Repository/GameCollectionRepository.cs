using GameManager.WebApp.BS.Contracts;
using GameManager.WebApp.BS.Entities.Models;
using GameManager.WebApp.BS.Shared.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManager.WebApp.BS.Repository
{
    public class GameCollectionRepository : RepositoryBase<GameCollection>, IGameCollectionRepository
    {

        public GameCollectionRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }
        public async Task<PagedList<GameCollection>> GetAllGamesCollectionAsync(GameParameters gameParameters, bool trackChanges)
        {
            var gamesCollection = FindAll(trackChanges)
                                  .Include(x => x.Games)
                                  .Include(x => x.GameSubCollections);
                                  

            return await PagedList<GameCollection>
                .ToPageListAsync(gamesCollection, gameParameters.PageNumber, gameParameters.PageSize);
        }

        public async Task DeleteGameCollectionAsync(int gameCollectionId)
        {
            var game = await FindByCondition(p => p.Id == gameCollectionId, true).SingleOrDefaultAsync();
            if (game != null)
            {
                Delete(game);
            }
        }

        public void CreateGameCollection(GameCollection gameCollection) => Create(gameCollection);

        public void UpdateGameCollecton(GameCollection gameCollection) => Update(gameCollection);

       
        public async Task<GameCollection> GetGameCollectionAsync(int gameCollectionId, bool trackChanges)
        {
            var gameCollection = await FindByCondition(p => p.Id   == gameCollectionId, trackChanges).SingleOrDefaultAsync();

            return gameCollection;
        }
    }
    
}
