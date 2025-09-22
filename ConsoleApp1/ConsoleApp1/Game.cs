using System;
using NAudio.Wave;
using System.Globalization;

namespace Game
{
    public class Game
    {
        private Maze _maze;
        private Player _player;
        private bool _running;
        private string _music = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            @"GitHub\C--Console-Game\ConsoleApp1\ConsoleApp1\music\cat.mp3"
        );


        private static IWavePlayer? waveOut;
        private static AudioFileReader? audioFile;

        public Game()
        {
            _maze = new Maze();
            _player = new Player(1, 1);
            _running = true;
        }

        public void Start()
        {
            ShowInstructions();
            Play();
        }

        // FROM NAudio Audio
        private void PlaySound()
        {
            try
            {
                waveOut = new WaveOutEvent();
                audioFile = new AudioFileReader(_music);
                waveOut.Init(audioFile);
                waveOut.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing music: {ex.Message}");
            }
        }

        private void StopSound()
        {
            waveOut?.Stop();
            audioFile?.Dispose();
            waveOut?.Dispose();
            audioFile = null;
            waveOut = null;
        }
        // END AUDIO

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
            PlaySound();
            while (_running)
            {
                _maze.Draw(_player);

                if (CheckWin())
                {
                    ShowWin();
                    break;
                }

                HandleInput();
            }

            StopSound();
        }

        private void HandleInput()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            Position newPos = new Position(_player.Position.X, _player.Position.Y);

            switch (key.Key)
            {
                case ConsoleKey.Z: newPos.Y--; break;
                case ConsoleKey.S: newPos.Y++; break;
                case ConsoleKey.Q: newPos.X--; break;
                case ConsoleKey.D: newPos.X++; break;
                case ConsoleKey.Escape: _running = false; return;
            }

            if (_maze.CanMove(newPos))
            {
                _player.Position = newPos;

                if (_maze.HasItem(newPos))
                {
                    _maze.CollectItem(newPos);
                    _player.AddScore();
                }
            }
        }

        private bool CheckWin()
        {
            return _maze.IsExit(_player.Position) && _maze.AllItemsCollected();
        }

        private void ShowWin()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("VICTORY!");
            Console.ResetColor();
            Console.WriteLine($"Final Score: {_player.Score} points");
            Console.WriteLine("Press any key to quit...");
            Console.ReadKey();
        }
    }
}
