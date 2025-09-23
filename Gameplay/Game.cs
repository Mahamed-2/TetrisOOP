using System;
using System.Diagnostics;
using System.Threading;
using TetrisOOP.Rendering;

namespace TetrisOOP.Gameplay
{
    public class Game
    {
        private readonly Board board;
        private Tetromino current;
        private Tetromino next;
        private bool gameOver = false;
        private int score = 0;
        private int level = 1;
        private int lines = 0;
        private int baseIntervalMs = 700;

        public Game()
        {
            board = new Board(10, 20);
            current = PieceFactory.RandomPiece();
            next = PieceFactory.RandomPiece();
            PlaceNewCurrent();
        }

        private void PlaceNewCurrent()
        {
            current = next;
            current.X = (board.Width - 4) / 2;
            current.Y = 0;
            next = PieceFactory.RandomPiece();
            if (!board.CanPlace(current, current.X, current.Y)) gameOver = true;
        }

        public void Run()
        {
            Console.CursorVisible = false;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            long lastTick = sw.ElapsedMilliseconds;

            while (!gameOver)
            {
                if (Console.KeyAvailable) HandleKey(Console.ReadKey(true).Key);
                long now = sw.ElapsedMilliseconds;
                int interval = Math.Max(100, baseIntervalMs - (level - 1) * 50);

                if (now - lastTick >= interval)
                {
                    if (!TryMove(0, 1))
                    {
                        board.Place(current, current.X, current.Y);
                        int cleared = board.ClearFullLines();
                        if (cleared > 0)
                        {
                            lines += cleared;
                            score += cleared * 100 * level;
                            level = 1 + lines / 10;
                        }
                        PlaceNewCurrent();
                    }
                    lastTick = now;
                }

                Renderer.DrawBoard(board, current, score, level);
                Thread.Sleep(15);
            }

            Console.Clear();
            Console.WriteLine($"GAME OVER\nScore: {score}  Lines: {lines}");
            SoundManager.StopMusic(); // stop background music

        }

        private void HandleKey(ConsoleKey k)
        {
            switch (k)
            {
                case ConsoleKey.LeftArrow: TryMove(-1, 0); break;
                case ConsoleKey.RightArrow: TryMove(1, 0); break;
                case ConsoleKey.DownArrow: TryMove(0, 1); break;
                case ConsoleKey.UpArrow:
                case ConsoleKey.X: TryRotate(true); break;
                case ConsoleKey.Z: TryRotate(false); break;
                case ConsoleKey.Spacebar: while (TryMove(0, 1)) { } break;
                case ConsoleKey.Q: gameOver = true; break;
            }
        }

        private bool TryMove(int dx, int dy)
        {
            int newX = current.X + dx, newY = current.Y + dy;
            if (board.CanPlace(current, newX, newY))
            {
                current.X = newX; current.Y = newY; return true;
            }
            return false;
        }

        private void TryRotate(bool cw)
        {
            if (cw) current.RotateCW(); else current.RotateCCW();
            if (!board.CanPlace(current, current.X, current.Y))
            {
                if (board.CanPlace(current, current.X - 1, current.Y)) current.X -= 1;
                else if (board.CanPlace(current, current.X + 1, current.Y)) current.X += 1;
                else { if (cw) current.RotateCCW(); else current.RotateCW(); }
            }
        }
    }
}
