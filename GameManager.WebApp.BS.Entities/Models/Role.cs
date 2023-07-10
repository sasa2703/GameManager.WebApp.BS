using System;
using System.Collections.Generic;

namespace GameManager.WebApp.BS.Entities.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public int UserCategoryId { get; set; }
        public string RoleName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? DtLastUpdate { get; set; }

        public virtual UserCategory UserCategory { get; set; } = null!;
        public virtual ICollection<User> Users { get; set; }
    }
}
