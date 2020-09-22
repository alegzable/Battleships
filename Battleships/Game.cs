using System;
using System.Linq;
using System.Text;

namespace Battleships
{
    public class Game
    {
        private string _playerName = "Loser";

        public void Play()
        {
            Greet();
            GetPlayerName();
            TellAStory();

            var board = new BoardGenerator().Generate();

            while (true)
            {
                var shotCoordinates = GetShotCoordinates(board);

                var shipShotResult = board.ReceiveShot(shotCoordinates);

                PrintResult(shipShotResult, shotCoordinates);

                if (shipShotResult.Result == ShotResult.Won)
                {
                    break;
                }
            }

            Console.Read();
        }

        private void TellAStory()
        {
            Console.WriteLine("You've stumbled upon a defenceless enemy fleet!");
            Console.WriteLine($"Hurry up, {_playerName}! Sink it before the reinforcements arrive!");
        }

        private static void Greet()
        {
            Console.WriteLine("Welcome to the easiest version of the Battleships Game!");
        }

        private void GetPlayerName()
        {
            Console.WriteLine("What's your name, Captain?");
            var providedName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(providedName))
            {
                Console.WriteLine($"Failing to provide a name is not a good sign. We'll call you '{_playerName}' :(");
            }
            else
            {
                _playerName = providedName;
            }
        }

        private void PrintResult(ShipShotResult shipShotResult, Coordinates coordinates)
        {
            switch (shipShotResult.Result)
            {
                case ShotResult.Missed:
                {
                    Console.WriteLine("You've missed.");
                    break;
                }
                case ShotResult.Hit:
                {
                    Console.WriteLine(
                        $"You've hit {shipShotResult.MaybeShip.Value.Name} at {coordinates}!");
                    break;
                }
                case ShotResult.Sunk:
                {
                    Console.WriteLine($"You've sunk {shipShotResult.MaybeShip.Value.Name}!");
                    break;
                }
                case ShotResult.Won:
                {
                    Console.WriteLine($"You've sunk the last ship and won. Congratulations {_playerName}!");
                    break;
                }
                default:
                {
                    throw new ArgumentOutOfRangeException(nameof(shipShotResult.Result));
                }
            }
        }

        private static Coordinates GetShotCoordinates(Board board)
        {
            while (true)
            {
                Console.WriteLine(
                    $@"Enter coordinates from {board.AllCoordinates.First()} to {board.AllCoordinates.Last()}.");

                var command = Console.ReadLine()?.Trim().ToUpper();

                if (command == null || command.Length != 2)
                {
                    continue;
                }

                var column = command[0];
                var rowParseSuccessful = int.TryParse(command[1].ToString(), out var row);

                if (!rowParseSuccessful)
                {
                    continue;
                }

                var coordinates = new Coordinates(column, row);
                var coordinatesFoundOnBoard = board.AllCoordinates.Contains(coordinates);

                if (coordinatesFoundOnBoard)
                {
                    return coordinates;
                }
            }
        }
    }
}