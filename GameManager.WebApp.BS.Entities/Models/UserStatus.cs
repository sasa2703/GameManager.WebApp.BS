using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameManager.WebApp.BS.Entities.Models
{
    public partial class UserStatus
    {
        public UserStatus()
        {
            Users = new HashSet<User>();
        }
        [Key]
        public int Id { get; set; }
        public string StatusName { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
