using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManager.WebApp.BS.Entities.Models
{
    public class GameCollection
    {
        public GameCollection()
        {
            GameId = new HashSet<int>();
        }
        public int Id { get; set; }
        public string DispleyName { get; set; }
        public string DispleyIndex { get; set; }
        public virtual ICollection<int> GameId { get; set; }

    }
}
