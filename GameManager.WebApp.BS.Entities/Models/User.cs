namespace GameManager.WebApp.BS.Entities.Models
{
    public partial class User
    {

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string? SubscriptionId { get; set; }
        public int UserCategory { get; set; }
        public int? RoleId { get; set; }
        public string? Name { get; set; }
        public string Title { get; set; } = null!;
        public string? CountryCode { get; set; }
        public string? Phone { get; set; }
        public string? TimeZone { get; set; }
        public bool Deleted { get; set; }
        public int StatusId { get; set; }
        public DateTime? DtCreated { get; set; }
        public DateTime? DtLastUpdate { get; set; }

        public virtual Role? Role { get; set; }
        public virtual UserStatus Status { get; set; } = null!;
        public virtual UserCategory UserCategoryNavigation { get; set; } = null!;
        public virtual Subscription? Subscription { get; set; }
    }
}
