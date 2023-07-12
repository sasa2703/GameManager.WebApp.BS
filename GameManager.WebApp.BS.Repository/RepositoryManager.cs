using GameManager.WebApp.BS.Contracts;
using GameManager.WebApp.BS.Entities.Models;

namespace GameManager.WebApp.BS.Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IGameRepository> _gameRepository;
        private readonly Lazy<IUserStatusRepository> _userStatusRepository;           
        private readonly Lazy<IApiAccessTokenRepository> _apiAccessTokenRepository;      
        private readonly Lazy<IGameCollectionRepository> _gameCollectionRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(repositoryContext));
            _gameRepository = new Lazy<IGameRepository>(() => new GameRepository(repositoryContext));
            _userStatusRepository = new Lazy<IUserStatusRepository>(() => new UserStatusRepository(repositoryContext));
            _apiAccessTokenRepository = new Lazy<IApiAccessTokenRepository>(() => new ApiAccessTokenRepository(repositoryContext));
            _gameCollectionRepository = new Lazy<IGameCollectionRepository>(()=> new GameCollectionRepository(repositoryContext));
        }

        public IUserRepository User => _userRepository.Value;
       
        public IGameRepository Game => _gameRepository.Value;
        public IUserStatusRepository UserStatus => _userStatusRepository.Value;
                 
        public IApiAccessTokenRepository ApiAccessTokenRepository => _apiAccessTokenRepository.Value;

        public IGameCollectionRepository GameCollection => _gameCollectionRepository.Value;
        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();


    }
}
