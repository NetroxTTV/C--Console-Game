public class Maze
{
    private char[,] _grid;
    private List<Collectible> _items;
    private Position _exit;

    public int Width { get; private set; }
    public int Height { get; private set; }

    public Maze()
    {
        Width = 15;
        Height = 9;
        _grid = new char[Height, Width];
        _items = new List<Collectible>();
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
                _grid[y, x] = '█';
    }

    private void CreatePaths()
    {
        int[] pathsY = { 1, 3, 5, 7 };
        int[] pathsX = { 1, 4, 7, 10, 13 };

        foreach (int y in pathsY)
            for (int x = 1; x < Width - 1; x++)
                _grid[y, x] = ' ';

        foreach (int x in pathsX)
            for (int y = 1; y < Height - 1; y++)
                _grid[y, x] = ' ';

        _grid[2, 2] = '█';
        _grid[2, 3] = '█';
        _grid[4, 5] = '█';
        _grid[4, 6] = '█';
        _grid[6, 8] = '█';
        _grid[6, 9] = '█';
    }

    private void AddItems()
    {
        _items.Add(new Collectible(2, 1));
        _items.Add(new Collectible(8, 3));
        _items.Add(new Collectible(5, 5));
        _items.Add(new Collectible(11, 7));
        _items.Add(new Collectible(6, 1));
    }

    private void SetExit()
    {
        _exit = new Position(13, 7);
        _grid[_exit.Y, _exit.X] = ' ';
    }

    public bool CanMove(Position pos)
    {
        if (pos.X < 0 || pos.X >= Width || pos.Y < 0 || pos.Y >= Height)
            return false;
        return _grid[pos.Y, pos.X] == ' ';
    }

    public bool HasItem(Position pos)
    {
        return _items.Any(item => !item.Collected && item.Position.Equals(pos));
    }

    public void CollectItem(Position pos)
    {
        var item = _items.FirstOrDefault(i => !i.Collected && i.Position.Equals(pos));
        if (item != null) item.Collected = true;
    }

    public bool IsExit(Position pos)
    {
        return _exit.Equals(pos);
    }

    public bool AllItemsCollected()
    {
        return _items.All(item => item.Collected);
    }

    public int ItemsLeft()
    {
        return _items.Count(item => !item.Collected);
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
                    Console.Write(_grid[y, x]);
                }
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }
}