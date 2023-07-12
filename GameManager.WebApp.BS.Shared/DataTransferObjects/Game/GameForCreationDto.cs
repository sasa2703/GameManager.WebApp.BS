using GameManager.WebApp.BS.Shared.DataTransferObjects.Device;
using GameManager.WebApp.BS.Shared.DataTransferObjects.GameCategory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManager.WebApp.BS.Shared.DataTransferObjects.Game
{
    public class GameForCreationDto
    {
        [Required(ErrorMessage = "Display name is a required field.")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Display index is a required field.")]
        public string DisplayIndex { get; set; }

        public DateTime ReleaseDateOfGame { get; set; }

        public GameCategoryDto Category{ get; set; }
        public byte[]? Thumbnail { get; set; }

        public ICollection<DeviceDto> Devices { get; set; }
    }
}
