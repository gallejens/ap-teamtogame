using System;

namespace TeamTOGame
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameManager())
            {
                game.Run();
            }
        }
    }
}
