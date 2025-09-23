using System;
using TetrisOOP.Characters;

namespace TetrisOOP.Gameplay
{
    // Tetromino inherits from Character (Inheritance)
    public class Tetromino : Character
    {
        private bool[,] shape = new bool[4, 4];
        public int X { get; set; }
        public int Y { get; set; }

        public Tetromino(string name, bool[,] initialShape, ConsoleColor color)
            : base(name, "Tetromino", color) // calls Character constructor
        {
            CopyShape(initialShape);
            X = 3;
            Y = 0;
        }

        private void CopyShape(bool[,] src)
        {
            for (int r = 0; r < 4; r++)
                for (int c = 0; c < 4; c++)
                    shape[r, c] = src[r, c];
        }

        public bool GetCell(int r, int c) => shape[r, c];

        public void RotateCW() => shape = RotateMatrixCW(shape);
        public void RotateCCW() => shape = RotateMatrixCCW(shape);

        private static bool[,] RotateMatrixCW(bool[,] src)
        {
            var dst = new bool[4, 4];
            for (int r = 0; r < 4; r++)
                for (int c = 0; c < 4; c++)
                    dst[c, 3 - r] = src[r, c];
            return dst;
        }

        private static bool[,] RotateMatrixCCW(bool[,] src)
        {
            var dst = new bool[4, 4];
            for (int r = 0; r < 4; r++)
                for (int c = 0; c < 4; c++)
                    dst[3 - c, r] = src[r, c];
            return dst;
        }

        // Polymorphism → override PrintInfo
        public override void PrintInfo()
        {
            Console.WriteLine($"Tetromino {Name} ({Form}), Color: {Color}");
        }
    }
}
