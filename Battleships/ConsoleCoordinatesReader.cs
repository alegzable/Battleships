using System;

namespace Battleships
{
    public class ConsoleCoordinatesReader : IFleetCommander
    {
        public Maybe<Coordinates> ProvideCoordinates()
        {
            var command = Console.ReadLine()?.Trim().ToUpper();
            
            if (command == null || command.Length != 2)
            {
                return new Maybe<Coordinates>();
            }

            var column = command[0];
            var rowParseSuccessful = int.TryParse(command[1].ToString(), out var row);

            return !rowParseSuccessful
                ? new Maybe<Coordinates>()
                : new Maybe<Coordinates>(new Coordinates(column, row));
        }
    }
}