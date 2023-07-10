
using GameManager.WebApp.BS.Shared.DataTransferObjects.GameSubCollection;
using GameManager.WebApp.BS.Shared.DataTransferObjects.Product;

namespace GameManager.WebApp.BS.Shared.DataTransferObjects.GameCollection
{
    public class GameCollectionDto
    {
        public int GameCollectionId { get; set; }
        public string DisplayName { get; set; }
        public string DisplayIndex { get; set; }
        public virtual ICollection<GameDto> Games { get; set; }
        public virtual ICollection<GameSubCollectionDto> GameSubCollections { get; set; }
    }
}
