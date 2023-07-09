using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using GameManager.WebApp.BS.Service.Contracts;
using GameManager.WebApp.BS.Shared.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using GameManager.WebApp.BS.Authorization.Interfaces;
using GameManager.WebApp.BS.Shared.DataTransferObjects.Product;

namespace GameManager.WebApp.BS.Presentation.Controllers
{
    [Authorize]
    [Route("api/games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _service;
        private readonly IAccessRightsResolver _auth;

        public GamesController(IGameService service, IAccessRightsResolver auth)
        {
            _service = service;
            _auth = auth;
        }

        [HttpGet("public")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPublicGames()
        {
            return Ok(await _service.GetPubliclyAvailableGames(false));
        }

        [HttpGet("{subscriptionId}/games")]
        public async Task<IActionResult> GetSubscriptionGames(string subscriptionId)
        {
            _auth.CheckPrincipalsRightsOnSubscription(User, subscriptionId);

            var subscriptionGames = await _service.GetAllGamesBySubscriptionAsync(subscriptionId, trackChanges: false);

            return Ok(subscriptionGames);
        }

        [HttpGet]
        public async Task<IActionResult> GetGames([FromQuery] GameParameters gameParameters)
        {
            var pagedResult = await _service.GetAllGamesAsync(gameParameters, trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            return Ok(pagedResult.games);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetGame(int gameId)
        {
            var game = await _service.GetGameAsync(gameId, false);

            return Ok(game);
        }


        [HttpDelete("{id:int}")]
        [Authorize(Policy = "InternalUser")]
        public async Task<IActionResult> DeleteGame(int gameId)
        {
            await _service.DeleteGameAsync(gameId);

            return NoContent();
        }


        [HttpPut("{gameId}")]
        [Authorize(Policy = "InternalUser")]
        public async Task<IActionResult> UpdateGame(int gameId, [FromBody] EditGameDto game)
        {
            GameDto editedGame = await _service.EditGamesAsync(gameId, game);
            return Ok(editedGame);
        }

    }
}
