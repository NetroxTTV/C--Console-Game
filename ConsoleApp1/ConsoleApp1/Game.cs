namespace Game
{
    public class Game
    {
        private Maze maze;
        private Player player;
        private bool running;

        public Game()
        {
            maze = new Maze();
            player = new Player(1, 1);
            running = true;
        }

        public void Start()
        {
            ShowInstructions();
            Play();
        }

        private void ShowInstructions()
        {
            Console.WriteLine("QUICK MAZE GAME");
            Console.WriteLine("Use ZQSD to move");
            Console.WriteLine("Collect all objects and reach the exit");
            Console.WriteLine("Press any key to start...");
            Console.ReadKey();
        }

        private void Play()
        {
            while (running)
            {
                maze.Draw(player);

                if (CheckWin())
                {
                    ShowWin();
                    break;
                }

                HandleInput();
            }
        }

        private void HandleInput()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            Position newPos = new Position(player.Position.X, player.Position.Y);

            switch (key.Key)
            {
                case ConsoleKey.Z: newPos.Y--; break;
                case ConsoleKey.S: newPos.Y++; break;
                case ConsoleKey.Q: newPos.X--; break;
                case ConsoleKey.D: newPos.X++; break;
                case ConsoleKey.Escape: running = false; return;
            }

            if (maze.CanMove(newPos))
            {
                player.Position = newPos;

                if (maze.HasItem(newPos))
                {
                    maze.CollectItem(newPos);
                    player.AddScore();
                }
            }
        }

        private bool CheckWin()
        {
            return maze.IsExit(player.Position) && maze.AllItemsCollected();
        }

        private void ShowWin()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("VICTORY!");
            Console.ResetColor();
            Console.WriteLine($"Final Score: {player.Score} points");
            Console.WriteLine("Press any key to quit...");
            Console.ReadKey();
        }
    }
}