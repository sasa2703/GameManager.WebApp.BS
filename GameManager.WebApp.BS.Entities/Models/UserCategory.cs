using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameManager.WebApp.BS.Entities.Models
{
    public partial class UserCategory
    {
        public UserCategory()
        {
            Subscriptions = new HashSet<Subscription>();
            Users = new HashSet<User>();
        }
        [Key]
        public int Id { get; set; }
        public string UserCategoryName { get; set; } = null!;

        public virtual ICollection<Subscription> Subscriptions { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
