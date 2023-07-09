namespace GameManager.WebApp.BS.Contracts
{
    public interface IRepositoryManager
    {
        IUserRepository User { get; }
        IUserStatusRepository UserStatus { get; }
        IRoleRepository Role { get; }
        IGameRepository Game { get; }
        IApiAccessTokenRepository ApiAccessTokenRepository { get; }
        Task SaveAsync();
    }
}
