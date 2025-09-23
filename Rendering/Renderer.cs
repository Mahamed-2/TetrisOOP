using System;

namespace TetrisOOP.Rendering
{
    public static class Renderer
    {
        private const char BlockChar = '█';

        public static void DrawBoard(Gameplay.Board board, Gameplay.Tetromino active, int score, int level)
        {
            Console.Clear();
            int width = board.Width;
            int height = board.Height;

            // --- Calculate horizontal centering ---
            int totalBoardWidth = width * 2 + 4; // 2 chars per block + borders
            int leftPadding = Math.Max(0, (Console.WindowWidth - totalBoardWidth) / 2);

            // --- Calculate vertical centering ---
            int totalBoardHeight = height + 6; // header + footer space
            int topPadding = Math.Max(0, (Console.WindowHeight - totalBoardHeight) / 2);

            // --- Print header (centered) ---
            Console.SetCursorPosition(leftPadding, topPadding);
            Console.WriteLine($"Score: {score}  Level: {level}");
            Console.SetCursorPosition(leftPadding, topPadding + 1);
            Console.WriteLine(new string('-', width * 2 + 6));

            // Copy board grid and overlay active piece
            var grid = board.GetGridCopy();
            if (active != null)
            {
                for (int r = 0; r < 4; r++)
                    for (int c = 0; c < 4; c++)
                        if (active.GetCell(r, c))
                        {
                            int x = active.X + c, y = active.Y + r;
                            if (y >= 0 && y < height && x >= 0 && x < width)
                                grid[y, x] = (int)active.Color;
                        }
            }

            // --- Print board rows (centered) ---
            for (int r = 0; r < height; r++)
            {
                Console.SetCursorPosition(leftPadding, topPadding + r + 2);
                Console.Write("| ");
                for (int c = 0; c < width; c++)
                {
                    if (grid[r, c] == -1) Console.Write("  ");
                    else
                    {
                        var orig = Console.ForegroundColor;
                        Console.ForegroundColor = (ConsoleColor)grid[r, c];
                        Console.Write(BlockChar); Console.Write(BlockChar);
                        Console.ForegroundColor = orig;
                    }
                }
                Console.WriteLine(" |");
            }

            // --- Footer ---
            Console.SetCursorPosition(leftPadding, topPadding + height + 2);
            Console.WriteLine(new string('-', width * 2 + 6));

            Console.SetCursorPosition(leftPadding, topPadding + height + 3);
            Console.WriteLine("← → move | ↓ drop | ↑/X rotate | Space hard drop | Q quit");
        }
    }
}
