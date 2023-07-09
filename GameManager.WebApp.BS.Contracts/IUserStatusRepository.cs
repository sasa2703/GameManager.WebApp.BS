using GameManager.WebApp.BS.Entities.Models;


namespace GameManager.WebApp.BS.Contracts
{
    public interface IUserStatusRepository
    {
        Task<UserStatus> GetUserStatusByName(string name);
    }
}
