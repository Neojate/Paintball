using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto
{
    class Npc : HumanElement
    {
        //constantes
        private const int ACCU_RANGE = 500;        

        //randoms
        private static Random random;

        //tipo
        IAType iaType;

        private float accuracy;

        //Tipo de NPC
        private TeamType team;

        //Instancia de Player
        private Player player;

        //fisicas de disparos
        private double shootTime;
        private bool canShoot;

        //estado de la IA
        private IAMoral iaMoral;
        private Vector3 triangleMove;

        //disparos mejorados
        private Vector3 triangleShoot;
        private Vector2 randomDestiny;

        public Npc(Camera camera, Vector2 initialPos, Vector2 initialOffset, TeamType team, IAType iaType, Player player) : base(camera)
        {
            //camera
            this.camera = camera;

            //posicion inicial
            elementPos = initialPos;
            elementOffset = initialOffset;

            //tamaño
            elementSize = new Vector2(32, 32);

            //equipo (escuadra) y player
            this.team = team;
            this.player = player;

            //conducta de la IA
            iaMoral = new IAMoral(iaType, initialPos);
            elementSpeed = iaMoral.getElementSpeed();

            //texturas
            texture = Textures.bodyAnim;
            slice = calculateSlice();
            color1D = getPalette1D(texture, slice);


            random = new Random();

            //equipo
            switch(Globals.difficult)
            {
                case 0:
                    marker = new Marker(MarkerModel.TIPPMANN_98);
                    break;
                case 1:
                    marker = new Marker(MarkerModel.TIPPMANN_A5);
                    break;
                case 2:
                    marker = new Marker(MarkerModel.EMPIRE_AXE);
                    break;
            }

            randomDestiny = new Vector2(0, 0);

            iaMoral.setDestinyNode(new Node(elementPos));
        }

        public override void handleInput()
        {
            
            //marker.handleInput(camera, elementPos, elementOffset, elementSize, destiny);
        }

        public override void update()
        {
            conductMoveOn();
            conductShootOn();

            calculateScreenPos();

            if (colisioned)
            {
                GameScreen.map.numberEnemies--;
                GameScreen.map.deleteElements.Add(this);
            }                      
        }

        public override void draw()
        {
            int posX = (int)(elementScreenPos.X + elementOffset.X);
            int posY = (int)(elementScreenPos.Y + elementOffset.Y);

            if (player.getElementScreenPos().Y + player.getElementOffset().Y + player.getElementSize().Y / 2 < elementScreenPos.Y + elementOffset.Y + elementSize.Y / 2)
            {
                //dibujar arma
                Globals.spriteBatch.Draw(marker.getTexture(), new Rectangle(posX + (int)elementSize.X / 2, posY + (int)elementSize.Y / 2, (int)elementSize.X, (int)elementSize.Y), calculateSlice(), Color.White, calculateRotation(), new Vector2(elementSize.X / 2, elementSize.Y / 2), SpriteEffects.None, 0);

                //dibujar cabeza
                Globals.spriteBatch.Draw(Textures.headAnim, new Rectangle(posX, posY, (int)elementSize.X, (int)elementSize.Y), calculateSlice(), Color.White);

                //dibujar mascara
                Globals.spriteBatch.Draw(Textures.maskAnim, new Rectangle(posX, posY, (int)elementSize.X, (int)elementSize.Y), calculateSlice(), Color.White);

                //dibujar cuerpo
                Globals.spriteBatch.Draw(Textures.bodyAnim, new Rectangle(posX, posY, (int)elementSize.X, (int)elementSize.Y), calculateSlice(), Color.White);
            }
            else
            {
                //dibujar cabeza
                Globals.spriteBatch.Draw(Textures.headAnim, new Rectangle(posX, posY, (int)elementSize.X, (int)elementSize.Y), calculateSlice(), Color.White);

                //dibujar mascara
                Globals.spriteBatch.Draw(Textures.maskAnim, new Rectangle(posX, posY, (int)elementSize.X, (int)elementSize.Y), calculateSlice(), Color.White);

                //dibujar cuerpo
                Globals.spriteBatch.Draw(Textures.bodyAnim, new Rectangle(posX, posY, (int)elementSize.X, (int)elementSize.Y), calculateSlice(), Color.White);

                //dibujar arma
                Globals.spriteBatch.Draw(marker.getTexture(), new Rectangle(posX + (int)elementSize.X / 2, posY + (int)elementSize.Y / 2, (int)elementSize.X, (int)elementSize.Y), calculateSlice(), Color.White, calculateRotation(), new Vector2(elementSize.X / 2, elementSize.Y / 2), SpriteEffects.None, 0);
            }

            Globals.spriteBatch.DrawString(Fonts.arial_12, iaMoral.getDestinyNode().getPosition().X + " / " + iaMoral.getDestinyNode().getPosition().Y, new Vector2(700, 10), Color.White);

        }



        #region MÉTODOS SOBREESCRITOS

        protected override Vector3 calculateTrigonometry()
        {
            Player player = GameScreen.map.getPlayer();
            float x = player.getElementScreenPos().X + player.getElementOffset().X - (elementScreenPos.X + elementOffset.X + elementSize.X / 2);
            float y = player.getElementScreenPos().Y + player.getElementOffset().Y - (elementScreenPos.Y + elementOffset.Y + elementSize.Y / 2);
            float h = (float)Math.Sqrt(x * x + y * y);
            return new Vector3(x, y, h);
        }

        #endregion

        public static int getRandom(int min, int max)
        {
            return random.Next(min, max);
        }

        #region MÉTODOS PRIVADOS

        /** Calcula la porción de textura que tiene que utilizar el enemigo */
        private Rectangle calculateSlice()
        {
            int look = 0;
            if (player.getElementScreenPos().Y + player.getElementOffset().Y > elementScreenPos.Y + elementOffset.Y) look = 1;
            if (player.getElementScreenPos().X + player.getElementOffset().X < elementScreenPos.X + elementOffset.Y) look += 2;
            return new Rectangle(0, look * (int)elementSize.Y, (int)elementSize.X, (int)elementSize.Y);
        }

        /** Activa el movimiento del personaje enemigo */
        private void conductMoveOn()
        {
            if (iaMoral.checkSite(elementPos))
            {
                iaMoral.calculateNextNode(player);
                triangleMove = calculateMovementTrigonometry();
            }
            moveNpc(triangleMove);
        }

        /** Calcula el movimiento del personaje enemigo en dirección recta */
        private Vector3 calculateMovementTrigonometry()
        {
            int tileSize = Tile.TILE_SIZE;
            float x = (iaMoral.getNextPosition().X * tileSize + tileSize / 2) - (elementPos.X * tileSize + elementOffset.X);
            float y = (iaMoral.getNextPosition().Y * tileSize + tileSize / 2) - (elementPos.Y * tileSize + elementOffset.Y);
            float h = (float)Math.Sqrt(x * x + y * y);
            return new Vector3(x, y, h);
        }

        /** Mueve el enemigo a través del mapa */
        private void moveNpc(Vector3 triangle)
        {
            elementOffset.X += (triangle.X / triangle.Z) * elementSpeed;
            elementOffset.Y += (triangle.Y / triangle.Z) * elementSpeed;
            calculateOffsets();
        }

        /** Activa los disparos del enemigo */
        private void conductShootOn()
        {
            shootTime += Globals.gameTime.ElapsedGameTime.TotalMilliseconds;
            if (shootTime > 1000 / marker.getFireRate())
            {
                canShoot = true;
                shootTime = 0;
            }
            if (canShoot)
            {
                shoot();
                canShoot = false;
            }
        }

        /** Hace disparar al enemigo */
        private void shoot()
        {
            triangleShoot = calculateTriangle(randomDestiny);
            Vector2 dispersion = calculateDispersion();

            Vector2 destiny = new Vector2(
                player.getElementScreenPos().X + player.getElementOffset().X + player.getElementSize().X / 2 + dispersion.X,
                player.getElementScreenPos().Y + player.getElementOffset().Y + player.getElementSize().Y / 2 + dispersion.Y);
            GameScreen.map.auxElements.Add(new Shoot(camera, elementPos, elementOffset, elementSize, accuracy, ShootFrom.ENEMY, destiny));
        }

        /** Calcula el disparo aleatorio del enemigo */
        /** randomDestiny = dispersión del disparo enemigo */
        private Vector3 calculateTriangle(Vector2 randomDestiny)
        {
            float x = (player.getElementScreenPos().X + player.getElementOffset().X + randomDestiny.X + player.getElementSize().X / 2) - (elementScreenPos.X + elementOffset.X + elementSize.X / 2);
            float y = (player.getElementScreenPos().Y + player.getElementOffset().Y + randomDestiny.Y + player.getElementSize().Y / 2) - (elementScreenPos.Y + elementOffset.Y + elementSize.Y / 2);
            float h = (float)Math.Sqrt(x * x + y * y);
            return new Vector3(x, y, h);
        }

        /** Calcula la dispersión del disparo enemigo */
        private Vector2 calculateDispersion()
        {
            Random rnd = new Random();
            int rndReason = (int)(triangleShoot.Z * marker.getAccuracy()) / Shoot.PERFECT_SHOOT;
            return new Vector2(
                rnd.Next(rndReason * -1, rndReason),
                rnd.Next(rndReason * -1, rndReason));
            
        }

        /** Calcula la razón trigonométrica del disparo del enemigo */
        private Vector2 calculateTrinometryReason()
        {
            float h = (float)Math.Sqrt(ACCU_RANGE * ACCU_RANGE + accuracy * accuracy);
            return new Vector2(accuracy / h, ACCU_RANGE / h);
        }

        /** Calcula la rotación del enemigo */
        protected override float calculateRotation()
        {
            Vector3 triangle = calculateTrigonometry();
            return (float)Math.Atan2(triangle.Y, triangle.X);
        }

        #endregion
    }
}
