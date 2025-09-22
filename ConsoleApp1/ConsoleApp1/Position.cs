using System;
public class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool Equals(Position other)
    {
        return X == other.X && Y == other.Y;
    }
}