using System.Linq;

namespace Battleships
{
    class Program
    {
        static void Main(string[] args)
        {
            var validColumns = new[] {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K'};
            var validRows = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            
            var game = new Game(
                    new BoardGenerator(validColumns, validRows),
                    new ConsoleCoordinatesReader(),
                    new ConsoleCommentator());
            
            var firstCoordinates = validColumns.First().ToString() + validRows.First();
            var lastCoordinates = validColumns.Last().ToString() + validRows.Last();
            
            game.Play(firstCoordinates, lastCoordinates);
        }
    }
}