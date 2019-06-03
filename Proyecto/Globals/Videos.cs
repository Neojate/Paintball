using Microsoft.Xna.Framework.Media;

namespace Proyecto
{
    class Videos
    {
        public static Video intro;
        public static Video background;

        public static void Load()
        {
            intro = Globals.content.Load<Video>("videos/intro");
            background = Globals.content.Load<Video>("videos/background");
        }
    }
}
