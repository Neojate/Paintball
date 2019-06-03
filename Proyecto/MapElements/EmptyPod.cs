using Microsoft.Xna.Framework;

namespace Proyecto
{
    class EmptyPod : MapElement
    {
        public EmptyPod(Camera camera, Vector2 originPos, Vector2 originOffset) : base(camera)
        {
            elementPos = originPos;
            elementOffset = originOffset;
            elementSize = new Vector2(10, 7);
        }

        public override void update()
        {
            calculateScreenPos();
        }

        public override void draw()
        {
            //Posición de emptyPod
            int posX = (int)(elementScreenPos.X + elementOffset.X);
            int posY = (int)(elementScreenPos.Y + elementOffset.Y);

            //Dibujado de emptyPod
            Globals.spriteBatch.Draw(Textures.podMap, new Rectangle(posX, posY, (int)elementSize.X, (int)elementSize.Y), Color.White);
        }
    }
}
