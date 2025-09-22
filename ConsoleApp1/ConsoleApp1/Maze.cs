public class Maze
{
    private char[,] grid;
    private List<Collectible> items;
    private Position exit;

    public int Width { get; private set; }
    public int Height { get; private set; }

    public Maze()
    {
        Width = 15;
        Height = 9;
        grid = new char[Height, Width];
        items = new List<Collectible>();
        BuildMaze();
    }

    private void BuildMaze()
    {
        FillWalls();
        CreatePaths();
        AddItems();
        SetExit();
    }

    private void FillWalls()
    {
        for (int y = 0; y < Height; y++)
            for (int x = 0; x < Width; x++)
                grid[y, x] = '█';
    }

    private void CreatePaths()
    {
        int[] pathsY = { 1, 3, 5, 7 };
        int[] pathsX = { 1, 4, 7, 10, 13 };

        foreach (int y in pathsY)
            for (int x = 1; x < Width - 1; x++)
                grid[y, x] = ' ';

        foreach (int x in pathsX)
            for (int y = 1; y < Height - 1; y++)
                grid[y, x] = ' ';

        grid[2, 2] = '█';
        grid[2, 3] = '█';
        grid[4, 5] = '█';
        grid[4, 6] = '█';
        grid[6, 8] = '█';
        grid[6, 9] = '█';
    }

    private void AddItems()
    {
        items.Add(new Collectible(2, 1));
        items.Add(new Collectible(8, 3));
        items.Add(new Collectible(5, 5));
        items.Add(new Collectible(11, 7));
        items.Add(new Collectible(6, 1));
    }

    private void SetExit()
    {
        exit = new Position(13, 7);
        grid[exit.Y, exit.X] = ' ';
    }

    public bool CanMove(Position pos)
    {
        if (pos.X < 0 || pos.X >= Width || pos.Y < 0 || pos.Y >= Height)
            return false;
        return grid[pos.Y, pos.X] == ' ';
    }

    public bool HasItem(Position pos)
    {
        return items.Any(item => !item.Collected && item.Position.Equals(pos));
    }

    public void CollectItem(Position pos)
    {
        var item = items.FirstOrDefault(i => !i.Collected && i.Position.Equals(pos));
        if (item != null) item.Collected = true;
    }

    public bool IsExit(Position pos)
    {
        return exit.Equals(pos);
    }

    public bool AllItemsCollected()
    {
        return items.All(item => item.Collected);
    }

    public int ItemsLeft()
    {
        return items.Count(item => !item.Collected);
    }

    public void Draw(Player player)
    {
        Console.Clear();
        Console.WriteLine($"Score: {player.Score} | Objects remaining: {ItemsLeft()}");

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Position pos = new Position(x, y);

                if (player.Position.Equals(pos))
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("P");
                }
                else if (HasItem(pos))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("o");
                }
                else if (IsExit(pos))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("E");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(grid[y, x]);
                }
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }
}