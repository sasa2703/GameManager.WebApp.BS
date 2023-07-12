using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameManager.WebApp.BS.Entities.Models
{
    public partial class ApiAccessToken
    {
        [Key]
        public int Id { get; set; }
        public string SubscriptionId { get; set; } = null!;
        public string SubscriptionName { get; set; } = null!;
        public string LoginId { get; set; } = null!;
        public DateTime? DtCreated { get; set; }
        public DateTime? DtExpireDate { get; set; }
        public string KeyVaultSecretId { get; set; } = null!;

        public virtual Subscription Subscription { get; set; } = null!;
    }
}
