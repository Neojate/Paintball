using Microsoft.Xna.Framework;

namespace Proyecto
{
    public class Camera
    {
        //posicionamiento
        private Vector2 cameraPos;
        private Vector2 cameraOffset;

        //atributos
        private float mouseLimit;
        private float cameraSpeed;

        #region CONSTRUCTORES

        public Camera()
        {
            //posicion inicial de la camara
            cameraPos = Vector2.Zero;

            //offset inicial de la camara
            cameraOffset = Vector2.Zero;

            //borde del movimiento del mouse
            mouseLimit = 0.05f;

            //velocidad de desplazamiento de la camara
            cameraSpeed = 10f;
        }

        #endregion

        #region METODOS PUBLICOS

        public void handleInput()
        {
            //mirar izquierda
            if (Input.mousePos.X < Globals.gameSize.X * mouseLimit)
            {
                cameraOffset.X += cameraSpeed;
                if (cameraOffset.X >= Tile.TILE_SIZE)
                {
                    cameraOffset.X = 0;
                    cameraPos.X -= 1;
                }
            }

            //mirar derecha
            if (Input.mousePos.X > Globals.gameSize.X - Globals.gameSize.X * mouseLimit)
            {
                cameraOffset.X -= cameraSpeed;
                if (cameraOffset.X < 0)
                {
                    cameraOffset.X = Tile.TILE_SIZE;
                    cameraPos.X += 1;
                }
            }

            //mirar arriba
            if (Input.mousePos.Y < Globals.gameSize.Y * mouseLimit)
            {
                cameraOffset.Y += cameraSpeed;
                if (cameraOffset.Y >= Tile.TILE_SIZE)
                {
                    cameraOffset.Y = 0;
                    cameraPos.Y -= 1;
                }
            }

            //mirar abajo
            if (Input.mousePos.Y > Globals.gameSize.Y - Globals.gameSize.Y * mouseLimit)
            {
                cameraOffset.Y -= cameraSpeed;
                if (cameraOffset.Y < 0)
                {
                    cameraOffset.Y = Tile.TILE_SIZE;
                    cameraPos.Y += 1;
                }
            }
        }

        #endregion

        #region GETTERS Y SETTERS

        public Vector2 getCameraPos() { return cameraPos; }
        public void setCameraPos(Vector2 cameraPos) { this.cameraPos = cameraPos; }
        
        public Vector2 getCameraOffset() { return cameraOffset; }
        public void setCameraOffset(Vector2 cameraOffset) { this.cameraOffset = cameraOffset; }
        
        #endregion
    }
}
