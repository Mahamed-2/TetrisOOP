using TetrisOOP.Gameplay;

namespace TetrisOOP
{
    class Program
    {
        static void Main()
        {
            Console.Title = "OOP Tetris (with Music)";
            
            // Play music in background
            SoundManager.PlayTetrisTheme();

            Game game = new Game();
            game.Run();
            
        }
    }
} 
