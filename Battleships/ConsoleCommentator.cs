using System;

namespace Battleships
{
    public class ConsoleCommentator : ICommentator
    {
        public void Greet()
        {
            Console.WriteLine("Welcome to the easiest version of the Battleships Game!");
        }

        public void TellAStory(string playerName)
        {
            Console.WriteLine("You've stumbled upon a defenceless enemy fleet!");
            Console.WriteLine($"Hurry up, {playerName}! Sink it before the reinforcements arrive!");
        }

        public string GetPlayerName(string initialPlayerName)
        {
            Console.WriteLine("What's your name, Captain?");
            var providedName = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(providedName))
            {
                return providedName;
            }

            Console.WriteLine($"Failing to provide a name is not a good sign. We'll call you '{initialPlayerName}' :(");
            
            return initialPlayerName;
        }
        
        public void PrintResult(ShipShotResult shipShotResult, Coordinates coordinates, string playerName)
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
                    Console.WriteLine($"You've sunk the last ship and won. Congratulations {playerName}!");
                    break;
                }
                default:
                {
                    throw new ArgumentOutOfRangeException(nameof(shipShotResult.Result));
                }
            }
        }

        public void AskForShotCoordinates(string firstCoordinates, string lastCoordinates)
        {
            Console.WriteLine(
                $@"Enter coordinates from {firstCoordinates} to {lastCoordinates}.");
        }
    }
}