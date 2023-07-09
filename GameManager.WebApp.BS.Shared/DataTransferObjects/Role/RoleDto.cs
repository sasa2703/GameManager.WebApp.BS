
namespace GameManager.WebApp.BS.Shared.DataTransferObjects.Role
{
    public class RoleDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public string RoleDescription { get; set; } = null!;
        public DateTime LastUpdate { get; set; }
        public string UserCategoryName { get; set; } = null!;
    }
}
