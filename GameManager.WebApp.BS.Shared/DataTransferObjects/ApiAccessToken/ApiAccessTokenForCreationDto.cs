using System.ComponentModel.DataAnnotations;


namespace GameManager.WebApp.BS.Shared.DataTransferObjects.ApiAccessToken
{
    public class ApiAccessTokenForCreationDto
    {

        [Required(ErrorMessage = "Api Access Token with LoginId is a required field.")]      
        public string? LoginId { get; set; }


        [Required(ErrorMessage = "ExpireDate is a required field.")]      
        public DateTime? ExpireDate { get; set; }
    }
}
