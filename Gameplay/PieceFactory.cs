using System;

namespace TetrisOOP.Gameplay
{
    public static class PieceFactory
    {
        private static readonly Random rnd = new Random();

        public static Tetromino RandomPiece()
        {
            int idx = rnd.Next(0, 7);
            return idx switch
            {
                0 => CreateI(),
                1 => CreateO(),
                2 => CreateT(),
                3 => CreateS(),
                4 => CreateZ(),
                5 => CreateJ(),
                _ => CreateL()
            };
        }

        private static Tetromino CreateI()
        {
            bool[,] s = new bool[4, 4];
            s[1, 0] = s[1, 1] = s[1, 2] = s[1, 3] = true;
            return new Tetromino("I", s, ConsoleColor.Cyan);
        }

        private static Tetromino CreateO()
        {
            bool[,] s = new bool[4, 4];
            s[1, 1] = s[1, 2] = s[2, 1] = s[2, 2] = true;
            return new Tetromino("O", s, ConsoleColor.Yellow);
        }

        private static Tetromino CreateT()
        {
            bool[,] s = new bool[4, 4];
            s[1, 0] = s[1, 1] = s[1, 2] = s[2, 1] = true;
            return new Tetromino("T", s, ConsoleColor.Magenta);
        }

        private static Tetromino CreateS()
        {
            bool[,] s = new bool[4, 4];
            s[1, 1] = s[1, 2] = s[2, 0] = s[2, 1] = true;
            return new Tetromino("S", s, ConsoleColor.Green);
        }

        private static Tetromino CreateZ()
        {
            bool[,] s = new bool[4, 4];
            s[1, 0] = s[1, 1] = s[2, 1] = s[2, 2] = true;
            return new Tetromino("Z", s, ConsoleColor.Red);
        }

        private static Tetromino CreateJ()
        {
            bool[,] s = new bool[4, 4];
            s[1, 0] = s[2, 0] = s[2, 1] = s[2, 2] = true;
            return new Tetromino("J", s, ConsoleColor.Blue);
        }

        private static Tetromino CreateL()
        {
            bool[,] s = new bool[4, 4];
            s[1, 2] = s[2, 0] = s[2, 1] = s[2, 2] = true;
            return new Tetromino("L", s, ConsoleColor.DarkYellow);
        }
    }
}
