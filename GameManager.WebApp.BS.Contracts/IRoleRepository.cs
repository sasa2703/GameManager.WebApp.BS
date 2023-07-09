using GameManager.WebApp.BS.Entities.Models;


namespace GameManager.WebApp.BS.Contracts
{
    public interface IRoleRepository : IRepositoryBase<Role>
    {
        Task<List<Role>> GetAllRoles();
        Task<List<Role>> GetRolesByCategory(int userCategoryId);
        Task<Role> GetRoleByName(string roleName);
    }
}
