using Microsoft.Xna.Framework;
using System;

namespace Proyecto
{
    public enum ShootFrom
    {
        ENEMY, ALLIED
    }

    public class Shoot : AnimatedElement
    {
        //constantes
        public const int PERFECT_SHOOT = 1000;
        public const int DELETE_TIME = 3000;

        //coordenadas de destino
        private Vector2 destinyScreen;

        //razones trigonometricas
        private Vector3 triangle;
        private Vector2 trigonometryReason;

        //atributos
        private float accuracy;
        private Vector2 randomDestiny;

        //procedencia
        private ShootFrom from;

        //color
        private Color color;

        //fisicas
        private double timeToDelete;

        public Shoot(Camera camera, Vector2 originPos, Vector2 originOffset, Vector2 originSize, float accuracy, ShootFrom from, Vector2 destiny) : base(camera)
        {
            elementPos = originPos;
            elementOffset = originOffset;
            destinyScreen = destiny;
            this.trigonometryReason = trigonometryReason;
            this.accuracy = accuracy;

            //Centra los disparos iniciales
            calculateInitialPosition();

            //tamaño del disparo
            elementSize = new Vector2(3, 3);

            //velocidad de disparo
            elementSpeed = 10f;

            //razones trigonometricas
            calculateScreenPos();
            triangle = trianglePlayerMouse();

            //actualizar las razones trigonometricas con el accuracy
            calculateRandomDestiny();

            //texturas
            texture = Textures.prova_imatge;
            slice = new Rectangle(0, 0, 3, 3);
            color1D = getPalette1D(texture, slice);

            //procedencia
            this.from = from;

            color = Color.Blue;
            if (from == ShootFrom.ENEMY) color = Color.Black;

        }

        public override void update()
        {
            if (!colisioned) moveShoot();
            calculateScreenPos();
            if (!colisioned)
            {
                foreach (MapElement e in GameScreen.map.getElements())
                {
                    if (e is Obstacle && perPixelCollision(e))
                    {
                        colisioned = true;
                        break;
                    }
                    if (from == ShootFrom.ALLIED)
                    {
                        if (e is Npc && perPixelCollision(e))
                        {
                            colisioned = true;
                            ((Npc)e).setColisioned(true);
                            break;
                        }
                    } 
                    else
                    {
                        if (e is Player && perPixelCollision(e))
                        {
                            colisioned = true;
                            ((Player)e).setColisioned(true);
                            break;
                        }
                    }
                    
                }
            }

            if (colisioned)
            {
                timeToDelete += Globals.gameTime.ElapsedGameTime.TotalMilliseconds;
                if (timeToDelete > DELETE_TIME) GameScreen.map.deleteElement(this);
            }

            //coordenadas a piñon
            if (elementPos.X < -10 || elementPos.X > 50) colisioned = true;
            if (elementPos.Y < -10 || elementPos.Y > 50) colisioned = true;
        }

        public override void draw()
        {
            int posX = (int)(elementScreenPos.X + elementOffset.X);
            int posY = (int)(elementScreenPos.Y + elementOffset.Y);
            
            //dibujado del disparo
            Globals.spriteBatch.Draw(Textures.shoot, new Rectangle(posX, posY, (int)elementSize.X, (int)elementSize.Y), color);
        }

        #region METODOS PRIVADOS

        /** Mueve la bala a través del mapa */
        private void moveShoot()
        {
            elementOffset.X += (triangle.X / triangle.Z) * elementSpeed;
            elementOffset.Y += (triangle.Y / triangle.Z) * elementSpeed;
            calculateOffsets();
        }

        /** Calcula el triángulo entre personaje y ratón */
        private Vector3 trianglePlayerMouse()
        {
            float x = destinyScreen.X - (elementScreenPos.X + elementOffset.X + elementSize.X / 2);
            float y = destinyScreen.Y - (elementScreenPos.Y + elementOffset.Y + elementSize.Y / 2);
            float h = (float)Math.Sqrt(x * x + y * y);
            return new Vector3(x, y, h);
        }

        /** Calcula el triángulo una vez se ha hecho el random acccuracy */
        /** randomDestiny = cálculo del accuracy en función de la distáncia de disparo */
        private Vector3 trianglePlayerMouse(Vector2 randomDestiny)
        {
            float x = destinyScreen.X  + randomDestiny.X - (elementScreenPos.X + elementOffset.X + elementSize.X / 2);
            float y = destinyScreen.Y + randomDestiny.Y - (elementScreenPos.Y + elementOffset.Y + elementSize.Y / 2);
            float h = (float)Math.Sqrt(x * x + y * y);
            return new Vector3(x, y, h);
        }

        /** Traduce la posición de la pantalla a la posicion en el mapa */
        private Vector2 screenToMap()
        {
            return new Vector2(
                (int)((Input.mousePos.X - camera.getCameraOffset().X) / 32) + camera.getCameraPos().X,
                (int)((Input.mousePos.Y - camera.getCameraOffset().Y) / 32) + camera.getCameraPos().Y);
        }

        /** Método que desvía el disparo según la accuracy del arma */
        private void calculateRandomDestiny()
        {
            Random rnd = new Random();
            int rndReason = (int)(triangle.Z * accuracy) / PERFECT_SHOOT;
            randomDestiny = new Vector2(
                rnd.Next(rndReason * -1, rndReason),
                rnd.Next(rndReason * -1, rndReason));
            triangle = trianglePlayerMouse(randomDestiny); 
        }

        /** Metodo que centra los disparos iniciales */
        private void calculateInitialPosition()
        {
            elementOffset.X += 16;
            elementOffset.Y += 16;
            calculateOffsets();
        }

        #endregion

    }
}
