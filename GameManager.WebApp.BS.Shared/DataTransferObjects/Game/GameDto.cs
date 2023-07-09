using GameManager.WebApp.BS.Entities.Models;
using GameManager.WebApp.BS.Shared.DataTransferObjects.Device;
using GameManager.WebApp.BS.Shared.DataTransferObjects.GameCategory;

namespace GameManager.WebApp.BS.Shared.DataTransferObjects.Product
{
    public class GameDto
    {
        public int GameId { get; set; }
        public string DisplayName { get; set; } = null!;
        public string DisplayIndex { get; set; } = null!;
        public DateTime ReleaseDateOfGame { get; set; }
        public GameCategoryDto Category { get; set; }
        public byte[] Thumbnail { get; set; }
        public virtual ICollection<DeviceDto> Devices { get; set; }
    }
}
