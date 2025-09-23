using System;

namespace TetrisOOP.Characters
{
    // Base class for all blocks (abstraction)
    public class Character
    {
        public static readonly int GridWidth = 10;
        public static readonly int GridHeight = 20;
        public static readonly int BlocksPerPiece = 4;

        public string Name { get; set; }
        public string Form { get; set; }
        public ConsoleColor Color { get; set; }

        public Character(string name, string form, ConsoleColor color)
        {
            Name = name;
            Form = form;
            Color = color;
        }

        public virtual void PrintInfo()
        {
            Console.WriteLine($"Name: {Name}, Form: {Form}, Color: {Color}");
        }

        public static void Info()
        {
            Console.WriteLine($"Grid: {GridWidth}x{GridHeight}, Blocks/Piece: {BlocksPerPiece}");
        }
    }
}
