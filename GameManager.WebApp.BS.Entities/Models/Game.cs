using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameManager.WebApp.BS.Entities.Models
{
    public partial class Game
    {
        public Game()
        {
            Devices = new HashSet<Device>();
        }

        [Key]
        public int Id { get; set; }
        public string DisplayName { get; set; } = null!;
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string DisplayIndex { get; set; }
        public DateTime ReleaseDateOfGame { get; set; } 
        public GameCategory Category { get; set; }
        public byte[]? Thumbnail { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<GameCollection> GameCollections { get; set; }

    }
}
