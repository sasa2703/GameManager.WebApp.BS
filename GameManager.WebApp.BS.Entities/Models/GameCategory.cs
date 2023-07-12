using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManager.WebApp.BS.Entities.Models
{
    public class GameCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
