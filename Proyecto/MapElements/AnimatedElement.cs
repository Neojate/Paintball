using Microsoft.Xna.Framework;
using System;

namespace Proyecto
{
    public enum MoveDir
    {
        //Tipos de movimiento
        FOWARD, BACK, LEFT, RIGHT
    }

    public class AnimatedElement : MapElement
    {
        //Velocidad de movimiento
        protected float elementSpeed;
        protected bool colisioned;

        public AnimatedElement(Camera camera) : base(camera)
        {
            colisioned = false;
        }

        /** Metodo que calcula los offsets y actualiza la posicion del personaje */
        protected void calculateOffsets()
        {
            if (elementOffset.X <= 0)
            {
                elementOffset.X = Tile.TILE_SIZE;
                elementPos.X -= 1;
            }
            else if (elementOffset.X >= Tile.TILE_SIZE)
            {
                elementOffset.X = 0;
                elementPos.X += 1;
            }

            if (elementOffset.Y <= 0)
            {
                elementOffset.Y = Tile.TILE_SIZE;
                elementPos.Y -= 1;
            }
            else if (elementOffset.Y >= Tile.TILE_SIZE)
            {
                elementOffset.Y = 0;
                elementPos.Y += 1;
            }
        }

        /** Método que calcula la colision de AnimatedElement contra un MapElement */
        /** e = MapElement contra el que calcula la colisión */
        protected bool calculateColision(MapElement e)
        {
            Rectangle obstacle = new Rectangle((int)e.getElementScreenPos().X, (int)e.getElementScreenPos().Y, (int)e.getElementSize().X, (int)e.getElementSize().Y);
            Point p = new Point((int)elementScreenPos.X, (int)elementScreenPos.Y);
            if (obstacle.Contains(p)) return true;
            p.X = p.X + (int)elementSize.X;
            p.Y = p.Y + (int)elementSize.Y;
            if (obstacle.Contains(p)) return true;
            return false;
        }

        /** Método que calcula la colisión por pixel de AnimatedElement contra un MapElement */
        /** e = MapElement contra el que calcula la colisión */
        protected bool perPixelCollision(MapElement e)
        {
            int posAX = (int)(elementScreenPos.X + elementOffset.X);
            int posAY = (int)(elementScreenPos.Y + elementOffset.Y);
            int posBX = (int)(e.getElementScreenPos().X + e.getElementOffset().X);
            int posBY = (int)(e.getElementScreenPos().Y + e.getElementOffset().Y);

            float dx = posAX + (elementSize.X / 2) - (posBX + (e.getElementSize().X / 2));
            float dy = posAY + (elementSize.Y / 2) - (posBY + (e.getElementSize().Y / 2));
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

            float diameterA = elementSize.X;
            float diameterB = e.getElementSize().X;
            //if (diameterA < 32) diameterA = 16;
            //if (diameterB < 32) diameterB = 32;
            if (this is Shoot) diameterA = 8;
            if (distance > diameterA / 2 + diameterB / 2 ) return false;

            /*if (Math.Abs(posAX - posBX) >= 32 ||
                Math.Abs(posAY - posBY) >= 32) return false;*/

            Color[] cA = color1D;
            Color[] cB = e.getColor1D();

            int x1 = Math.Max(posAX, posBX);
            int x2 = (int)Math.Min(posAX + elementSize.X, posBX + e.getElementSize().X);
            int y1 = Math.Max(posAY, posBY);
            int y2 = (int)Math.Min(posAY + elementSize.Y, posBY + e.getElementSize().Y);

            for (int y = y1; y < y2; y++) { 
                for (int x = x1; x < x2; x++)
                {
                    Color a = cA[(x - posAX) + (y - posAY) * slice.Width];
                    Color b = cB[(x - posBX) + (y - posBY) * e.getSlice().Width];
                    if (a.A != 0 && b.A != 0) return true;
                }
            }
            return false;
        }

        public bool isColisioned() { return colisioned; }
        public void setColisioned(bool colisioned) { this.colisioned = colisioned; }
    }
}
