namespace GameManager.WebApp.BS.Entities.Models
{
    public partial class Game
    {
        public Game()
        {
            Devices = new HashSet<Device>();
        }

        public int Id { get; set; }
        public string DisplayName { get; set; } = null!;
        public string DisplayIndex { get; set; } = null!;
        public DateTime ReleaseDateOfGame { get; set; } 
        public GameCategory Category { get; set; }
        public byte[] Thumbnail { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<GameCollection> GameCollections { get; set; }

    }
}
