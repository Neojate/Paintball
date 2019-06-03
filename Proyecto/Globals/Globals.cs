using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Proyecto
{
    class Globals
    {
        public static ContentManager content;
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static GameTime gameTime;
        public static Boolean windowFocused;
        public static Vector2 gameSize;
        public static RenderTarget2D backBuffer;
        public static Boolean fullScreen;
        public static int language;
        public static int difficult;
    }
}
