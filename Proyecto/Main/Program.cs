using System;

namespace Proyecto
{
#if WINDOWS || LINUX

    public static class Program
    {
        public static PaintToWin p2w;

        [STAThread]
        static void Main()
        {
            using (var game = new PaintToWin())
            {
                p2w = game;
                p2w.Run();
            }                
        }
    }
#endif
}
