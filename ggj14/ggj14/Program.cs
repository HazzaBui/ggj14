using System;

namespace ggj14
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application
        /// </summary>
        static void Main(string[] args)
        {
            using (levelManager game = new levelManager())
            {
                game.Run();
                int i = 0;
            }
        }
    }
#endif
}

