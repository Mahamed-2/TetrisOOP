using System;

namespace TetrisOOP.Gameplay
{
    public class Board
    {
        public readonly int Width;
        public readonly int Height;
        private int[,] grid;

        public Board(int width = 1000, int height = 2000)
        {
            Width = width;
            Height = height;
            grid = new int[height, width];
            Clear();
        }

        public void Clear()
        {
            for (int r = 0; r < Height; r++)
                for (int c = 0; c < Width; c++)
                    grid[r, c] = -1;
        }

        public bool CanPlace(Tetromino t, int x, int y)
        {
            for (int r = 0; r < 4; r++)
                for (int c = 0; c < 4; c++)
                {
                    if (!t.GetCell(r, c)) continue;
                    int bx = x + c, by = y + r;
                    if (bx < 0 || bx >= Width || by < 0 || by >= Height) return false;
                    if (grid[by, bx] != -1) return false;
                }
            return true;
        }

        public void Place(Tetromino t, int x, int y)
        {
            int colorIndex = (int)t.Color;
            for (int r = 0; r < 4; r++)
                for (int c = 0; c < 4; c++)
                {
                    if (!t.GetCell(r, c)) continue;
                    int bx = x + c, by = y + r;
                    if (by >= 0 && by < Height && bx >= 0 && bx < Width)
                        grid[by, bx] = colorIndex;
                }
        }

        public int ClearFullLines()
        {
            int cleared = 0;
            for (int r = Height - 1; r >= 0; r--)
            {
                bool full = true;
                for (int c = 0; c < Width; c++)
                    if (grid[r, c] == -1) { full = false; break; }
                if (full)
                {
                    cleared++;
                    for (int rr = r; rr > 0; rr--)
                        for (int cc = 0; cc < Width; cc++)
                            grid[rr, cc] = grid[rr - 1, cc];
                    for (int cc = 0; cc < Width; cc++)
                        grid[0, cc] = -1;
                    r++;
                }
            }
            return cleared;
        }

        public int[,] GetGridCopy()
        {
            var copy = new int[Height, Width];
            for (int r = 0; r < Height; r++)
                for (int c = 0; c < Width; c++)
                    copy[r, c] = grid[r, c];
            return copy;
        }
    }
}
