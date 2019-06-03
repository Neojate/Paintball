using Microsoft.Xna.Framework.Graphics;

namespace Proyecto
{
    class Fonts
    {
        public static SpriteFont arial_12;
        public static SpriteFont arial_14;
        public static SpriteFont arial_24;
        public static SpriteFont arial_60;

        public static void Load()
        {
            arial_12 = Globals.content.Load<SpriteFont>("fonts/arial_12");
            arial_14 = Globals.content.Load<SpriteFont>("fonts/arial_14");
            arial_24 = Globals.content.Load<SpriteFont>("fonts/arial_24");
            arial_60 = Globals.content.Load<SpriteFont>("fonts/arial_60");
        }
    }
}

