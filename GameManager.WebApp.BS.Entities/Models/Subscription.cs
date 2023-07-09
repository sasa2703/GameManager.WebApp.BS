using System;
using System.Collections.Generic;

namespace GameManager.WebApp.BS.Entities.Models
{
    public partial class Subscription
    {
        public Subscription()
        {
            ApiAccessTokens = new HashSet<ApiAccessToken>();
            Users = new HashSet<User>();
        }

        public int ISubscriptionId { get; set; }
        public string SSubscriptionId { get; set; } = null!;
        public string SSubscriptionName { get; set; } = null!;
        public string? SPartnerSubsriptionId { get; set; }
        public string SProjectCode { get; set; } = null!;
        public DateTime? DtCreated { get; set; }
        public DateTime? DtLastUpdate { get; set; }
        public bool BDeleted { get; set; }
        public int IStatusId { get; set; }
        public int? IUserCategoryId { get; set; }
        public bool IsProduction { get; set; }

        public virtual UserCategory? IUserCategory { get; set; }
        public virtual ICollection<ApiAccessToken> ApiAccessTokens { get; set; }      
        public virtual ICollection<User> Users { get; set; }
    }
}
