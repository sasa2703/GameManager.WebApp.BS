using GameManager.WebApp.BS.Shared.DataTransferObjects.GameSubCollection;
using GameManager.WebApp.BS.Shared.DataTransferObjects.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManager.WebApp.BS.Shared.DataTransferObjects.GameCollection
{
    public class GameCollectionForCreationDto
    {
        [Required(ErrorMessage = "Display name is a required field.")]
        public string DisplayName { get; set; }
        [Required(ErrorMessage = "Display index is a required field.")]
        public string DisplayIndex { get; set; }
        public virtual List<int> GamesIds { get; set; }
        public virtual List<int> GameSubCollectionsIds { get; set; }

    }
}
