using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManager.WebApp.BS.Entities.Models
{
    public class GameCollection
    {
        public GameCollection()
        {
            Games = new HashSet<Game>();
            GameSubCollections = new HashSet<GameSubCollection>();
        }
        [Key]
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string DisplayIndex { get; set; }
        public virtual ICollection<Game> Games { get; set; }
        public virtual ICollection<GameSubCollection> GameSubCollections { get; set; }

    }
}
