public class Player
{
    public Position Position { get; set; }
    public int Score { get; set; }

    public Player(int x, int y)
    {
        Position = new Position(x, y);
        Score = 0;
    }

    public void Move(int dx, int dy)
    {
        Position.X += dx;
        Position.Y += dy;
    }

    public void AddScore()
    {
        Score += 10;
    }
}