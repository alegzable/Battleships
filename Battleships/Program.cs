namespace Battleships
{
    class Program
    {
        static void Main(string[] args)
        {
            new Game(new BoardGenerator(), new ConsoleCoordinatesReader(), new ConsoleCommentator()).Play();
        }
    }
}