namespace GameManager.WebApp.BS.Shared.Exceptions.Game
{
    public class GameNotFoundException : NotFoundException
    {
        public GameNotFoundException(int gameId) : base($"The game with id: {gameId} doesn't exist in the database.")
        {

        }

        public GameNotFoundException(string gameIndex) : base($"The game with index: {gameIndex} doesn't exist in the database.")
        {

        }
    }
}
