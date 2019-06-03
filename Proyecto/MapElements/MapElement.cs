using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Proyecto
{
    public class MapElement : IComparable<MapElement>
    {
        //cámara del juego
        protected Camera camera;

        //posicionamiento en el mapa
        protected Vector2 elementPos;
        protected Vector2 elementOffset;

        //posicionamiento en la pantalla
        protected Vector2 elementScreenPos;

        //tamaño del elemento
        protected Vector2 elementSize;

        //texturas
        protected Texture2D texture;
        protected Rectangle slice;
        protected Color[] color1D;

        public MapElement(Camera camera)
        {
            this.camera = camera;
        }

        /** Metodo que calcula la posicion de dibujado en la pantalla respecto sus coordenadas de mapa y cámara */
        protected void calculateScreenPos()
        {
            elementScreenPos.X = (elementPos.X - camera.getCameraPos().X) * Tile.TILE_SIZE + camera.getCameraOffset().X;
            elementScreenPos.Y = (elementPos.Y - camera.getCameraPos().Y) * Tile.TILE_SIZE + camera.getCameraOffset().Y;
        }

        /** Método que devuelve la paleta de colores en un vector */
        /** texture = textura */
        /** slice = slice que utiliza la textura */
        protected Color[] getPalette1D(Texture2D texture, Rectangle slice)
        {
            Color[] color1D = new Color[texture.Width * texture.Height];
            texture.GetData(color1D);
            Color[,] color2D = new Color[texture.Width, texture.Height];
            Color[] finalColor1D = new Color[slice.Width * slice.Height];
            for (int y = 0; y < texture.Height; y++)
            {
                for (int x = 0; x < texture.Width; x++)
                {
                    color2D[x, y] = color1D[y * texture.Width + x];
                }
            }
            for (int y = 0; y < slice.Height; y++)
            {
                for (int x = 0; x < slice.Width; x++)
                {
                    finalColor1D[y * slice.Width + x] = color2D[x + slice.X, y + slice.Y];
                }
            }
            return finalColor1D;
        }

        public virtual void handleInput()
        {
            
        }

        public virtual void update()
        {

        }

        public virtual void draw()
        {

        }

        /** Método para saber si el elemento actual se tiene que dibujar antes o después del otro elemento */
        public int CompareTo(MapElement other)
        {
            return (int)((elementScreenPos.Y + elementOffset.Y + elementSize.Y) - (other.elementScreenPos.Y + other.elementOffset.Y + other.elementSize.Y));
        }

        #region GETTERS Y SETTERS

        public Vector2 getElementPos() { return elementPos; }
        public void setElementPos(Vector2 elementPos) { this.elementPos = elementPos; }

        public Vector2 getElementOffset() { return elementOffset; }
        public void setElementOffset(Vector2 elementOffset) { this.elementOffset = elementOffset; }
        
        public Vector2 getElementScreenPos() { return elementScreenPos; }
        public void setElementScreenPos(Vector2 elementScreenPos) { this.elementScreenPos = elementScreenPos; } 

        public Vector2 getElementSize() { return elementSize; }
        public void setElementSize(Vector2 elementSize) { this.elementSize = elementSize; }

        public Texture2D getTexture() { return texture; }
        public void setTexture(Texture2D texture) { this.texture = texture; }

        public Rectangle getSlice() { return slice; }
        public void setSlice(Rectangle slice) { this.slice = slice; }

        public Color[] getColor1D() { return color1D; }
        public void setColor1D(Color[] color1D) { this.color1D = color1D; }

        #endregion
    }
}
