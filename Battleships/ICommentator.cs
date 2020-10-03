namespace Battleships
{
    public interface ICommentator
    {
        void Greet();
        void TellAStory(string playerName);
        string GetPlayerName(string initialPlayerName);
        void PrintResult(ShipShotResult shipShotResult, Coordinates coordinates, string playerName);
        void AskForShotCoordinates(string firstCoordinates, string lastCoordinates);
    }
}