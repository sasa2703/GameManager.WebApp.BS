using GameManager.WebApp.BS.Shared.DataTransferObjects.GameSubCollection;
using GameManager.WebApp.BS.Shared.DataTransferObjects.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManager.WebApp.BS.Shared.DataTransferObjects.GameCollection
{
    public class EditGameCollectionDto
    {
        public string DisplayName { get; set; }
        public string DisplayIndex { get; set; }
        public virtual ICollection<GameDto> Games { get; set; }
        public virtual ICollection<GameSubCollectionDto> GameSubCollections { get; set; }
    }
}
