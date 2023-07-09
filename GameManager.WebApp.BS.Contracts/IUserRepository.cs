using GameManager.WebApp.BS.Entities.Models;
using GameManager.WebApp.BS.Shared.RequestFeatures;

namespace GameManager.WebApp.BS.Contracts
{
    public interface IUserRepository
    {
        Task<PagedList<User>> GetAllUsersAsync(UserParameters userParameters, bool trackChanges);
        Task<PagedList<User>> GetAllUsersForSubscriptionAsync(UserParameters userParameters, bool trackChanges);
        Task<User> GetUserAsync(int userId, bool trackChanges);
        Task<User> GetUserByUsernameAsync(string username, bool trackChanges);
        Task<User> GetUserEnableDisableAsync(int userId, bool trackChanges);
        Task AddUserAsync(User user);
        Task DeleteUserAsync(string username);
        Task ReactivateDeleteUserAsync(string username);
        Task<int> GetUsersNumberBySubscriptionIdAsync(string subscriptionId, bool trackChanges);
    }
}
