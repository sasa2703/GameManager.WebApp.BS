using GameManager.WebApp.BS.Authorization.Interfaces;
using GameManager.WebApp.BS.Service.Contracts;
using GameManager.WebApp.BS.Shared.DataTransferObjects.GameCollection;
using GameManager.WebApp.BS.Shared.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GameManager.WebApp.BS.Presentation.Controllers
{
    [Authorize]
    [Route("api/games-collection")]
    [ApiController]
    public class GameCollectionController:ControllerBase
    {
        private readonly IGameCollectionService _gamesControllerService;
        private readonly IAccessRightsResolver _auth;

        public GameCollectionController(IGameCollectionService gamesControllerService, IAccessRightsResolver auth)
        {
            _gamesControllerService = gamesControllerService;
            _auth = auth;
        }

        [HttpGet]
        [Authorize(Policy = "InternalOrEndUser")]
        public async Task<IActionResult> GetGamesCollections([FromQuery] RequestParameters requestParameters)
        {
            var pagedResult = await _gamesControllerService.GetAllGamesCollectionsAsync(requestParameters, trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            return Ok(pagedResult.games);
        }

        [HttpGet("{id:int}")]
        [Authorize(Policy = "InternalOrEndUser")]
        public async Task<IActionResult> GetGameCollection(int gameCollectionId)
        {
            var gameCollection = await _gamesControllerService.GetGameCollectionAsync(gameCollectionId, false);

            return Ok(gameCollection);
        }

        [HttpPost]
        [Authorize(Policy = "InternalUser")]
        public async Task<IActionResult> CreateGameCollection([FromBody] GameCollectionForCreationDto game)
        {
            if (game is null)
            {
                return BadRequest("GameCollectionForCreationDto object is null");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }


            var createdGame = await _gamesControllerService.CreateGameCollectionAsync(game);

            return CreatedAtRoute("GetGame", new
            {
                id = createdGame.GameCollectionId
            }, createdGame);
        }


        [HttpDelete("{id:int}")]
        [Authorize(Policy = "InternalUser")]
        public async Task<IActionResult> DeleteGameCollection(int gameCollectionId)
        {
            await _gamesControllerService.DeleteGameCollectionAsync(gameCollectionId);

            return NoContent();
        }


        [HttpPut("{gameCollectionId}")]
        [Authorize(Policy = "InternalUser")]
        public async Task<IActionResult> UpdateGameCollection(int gameCollectionId, [FromBody] EditGameCollectionDto gameCollection)
        {

            if (gameCollection is null)
            {
                return BadRequest("EditGameCollectionDto object is null");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            GameCollectionDto editedGameCollection = await _gamesControllerService.EditGameCollectionAsync(gameCollectionId, gameCollection);
            return Ok(editedGameCollection);
        }
    }
}
