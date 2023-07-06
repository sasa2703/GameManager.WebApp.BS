using System;
using System.Collections.Generic;

namespace GameManager.WebApp.BS.Entities.Models
{
    public partial class ApiAccessToken
    {
        public int IApiAccessTokenId { get; set; }
        public string SSubscriptionId { get; set; } = null!;
        public string SSubscriptionName { get; set; } = null!;
        public string SLoginId { get; set; } = null!;
        public DateTime? DtCreated { get; set; }
        public DateTime? DtExpireDate { get; set; }
        public string SKeyVaultSecretId { get; set; } = null!;

        public virtual Subscription SSubscription { get; set; } = null!;
    }
}
