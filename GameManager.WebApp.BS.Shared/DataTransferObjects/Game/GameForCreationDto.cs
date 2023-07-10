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
        public bool DisplayName { get; set; }

        [Required(ErrorMessage = "Display index is a required field.")]
        public bool DisplayIndex { get; set; }

        public DateTime ReleaseDateOfGame { get; set; }

        [Required(ErrorMessage = "Notification with displayd from utc is a required field.")]
        public DateTime DisplaydFromUtc { get; set; }

        public int CategoryId { get; set; }
        public byte[] Thumbnail { get; set; }

        public List<int> DeviceIds { get; set; }
    }
}
