namespace Battleships
{
    public interface IFleetCommander
    {
        Maybe<Coordinates> ProvideCoordinates();
    }
}