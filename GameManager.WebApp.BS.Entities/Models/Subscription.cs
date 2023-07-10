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

        public int Id { get; set; }
        public string SubscriptionName { get; set; } = null!;
        public string ProjectCode { get; set; } = null!;
        public DateTime? DtCreated { get; set; }
        public DateTime? DtLastUpdate { get; set; }

        public virtual UserCategory? IUserCategory { get; set; }
        public virtual ICollection<ApiAccessToken> ApiAccessTokens { get; set; }      
        public virtual ICollection<User> Users { get; set; }
    }
}
