using System.Linq;

namespace Battleships
{
    public class Game
    {
        private readonly IBoardGenerator _boardGenerator;
        private readonly IFleetCommander _fleetCommander;
        private readonly ICommentator _commentator;
        private string _playerName = "Loser";

        public Game(
            IBoardGenerator boardGenerator,
            IFleetCommander fleetCommander,
            ICommentator commentator)
        {
            _boardGenerator = boardGenerator;
            _fleetCommander = fleetCommander;
            _commentator = commentator;
        }

        public void Play()
        {
            _commentator.Greet();
            _playerName = _commentator.GetPlayerName(_playerName);
            _commentator.TellAStory(_playerName);

            var board = _boardGenerator.Generate();

            while (true)
            {
                var shotCoordinates = GetShotCoordinates(board);

                var shipShotResult = board.ReceiveShot(shotCoordinates);

                _commentator.PrintResult(shipShotResult, shotCoordinates, _playerName);

                if (shipShotResult.Result == ShotResult.Won)
                {
                    break;
                }
            }
        }

        private Coordinates GetShotCoordinates(IBoard board)
        {
            while (true)
            {
                _commentator.AskForShotCoordinates(
                    board.AllCoordinates.First().ToString(),
                    board.AllCoordinates.Last().ToString());

                var coordinates = _fleetCommander.ProvideCoordinates();

                if (!coordinates.HasValue)
                {
                    continue;
                }

                return coordinates.Value;
            }
        }
    }
}