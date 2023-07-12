namespace GameManager.WebApp.BS.Contracts
{
    public interface IRepositoryManager
    {
        IUserRepository User { get; }
        IUserStatusRepository UserStatus { get; }
        IGameRepository Game { get; }
        IApiAccessTokenRepository ApiAccessTokenRepository { get; }

        IGameCollectionRepository GameCollection { get; }
        Task SaveAsync();
    }
}
