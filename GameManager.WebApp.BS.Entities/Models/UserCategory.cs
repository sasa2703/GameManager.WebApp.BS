using System;
using System.Collections.Generic;

namespace GameManager.WebApp.BS.Entities.Models
{
    public partial class UserCategory
    {
        public UserCategory()
        {
            Roles = new HashSet<Role>();
            Subscriptions = new HashSet<Subscription>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string UserCategoryName { get; set; } = null!;

        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
