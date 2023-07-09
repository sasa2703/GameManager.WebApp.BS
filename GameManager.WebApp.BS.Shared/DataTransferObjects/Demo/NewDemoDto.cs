
using System.ComponentModel.DataAnnotations;

namespace GameManager.WebApp.BS.Shared.DataTransferObjects.Demo
{
    public class NewDemoDto
    {
        [Required]
        public string UserEmail { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;

    }
}
