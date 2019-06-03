using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Proyecto
{
    class Panel : Component
    {
        public Panel(Rectangle rectangle, Texture2D texture) : base(rectangle, texture)
        {

        }

        public override void draw()
        {
            if (!visibility) return;
            Globals.spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
