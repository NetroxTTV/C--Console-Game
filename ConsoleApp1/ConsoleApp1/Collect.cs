public class Collectible
{
    public Position Position { get; set; }
    public bool Collected { get; set; }

    public Collectible(int x, int y)
    {
        Position = new Position(x, y);
        Collected = false;
    }
}