using GameManager.WebApp.BS.Authorization.Interfaces;
using GameManager.WebApp.BS.Service.Contracts;
using GameManager.WebApp.BS.Shared.DataTransferObjects.ApiAccessToken;
using GameManager.WebApp.BS.Shared.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace GameManager.WebApp.BS.Presentation.Controllers
{
    [Route("api/api-access-tokens")]
    [ApiController]
    [Authorize]
    public class ApiAccessTokenController: ControllerBase
    {
        private readonly IApiAccessTokenService _service;
        private readonly IAccessRightsResolver _auth;


        public ApiAccessTokenController(IApiAccessTokenService apiAccessToken,IAccessRightsResolver accessRightsResolver)
        {
            _service = apiAccessToken;
            _auth = accessRightsResolver;
        }

        [HttpGet]
        [Authorize(Policy = "InternalOrEndUser")]
        public async Task<IActionResult> GetApiAccessTokenPaged([FromQuery] ApiAccessTokenParameters apiAccessTokenRequestParameters)
        {

            var pagedResult = await _service.GetApiAccessTokensAsync(apiAccessTokenRequestParameters, trackChanges: false, User);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            return Ok(pagedResult.apiAccessTokenDto);
        }


        [HttpPost]
        [Authorize(Policy = "InternalOrEndUser")]
        public async Task<IActionResult> CreateApiAccessToken([FromBody] ApiAccessTokenForCreationDto apiAccessTokenForCreation)
        {
            if (apiAccessTokenForCreation is null)
            {
                return BadRequest("ApiAccessTokenForCreationDto object is null");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }


            var createdApiAccessToken = await _service.CreateApiAccessTokenAsync(apiAccessTokenForCreation);

            return CreatedAtRoute("GetApiAccessToken", new
            {
                apiAccessTokenId = createdApiAccessToken.ApiAccessTokenId
            }, createdApiAccessToken);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "InternalUser")]
        public async Task<IActionResult> DeleteApiAccessToken(int id)
        {         
            await _service.DeleteApiAccessTokenAsync(id, trackChanges: false);

            return NoContent();
        }


        [HttpGet("{apiAccessTokenId}", Name = "GetApiAccessToken")]
        [Authorize(Policy = "InternalOrEndUser")]
        public async Task<IActionResult> GetApiAccessToken(int apiAccessTokenId, [FromQuery] ApiAccessTokenParameters billingAddressParameters)
        {
            var result = await _service.GetApiAccessTokenByIdAsync(apiAccessTokenId, trackChanges: false);

            return Ok(result);
        }

    }
}
