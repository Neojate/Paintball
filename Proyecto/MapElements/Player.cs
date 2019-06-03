using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Proyecto
{    
    public class Player : HumanElement
    {
        //constantes
        public const String SAVE_FILE = "savedgame.pj";
        private const float RUN_COST = 1f;
        private const float STAMINA_INCREMENT = 5f;
        private const int NUMBER_FRAMES = 3;

        //trucos
        private bool invulnerability = false;

        //atributos
        private Vector2 playerStamina;      //stamina del jugador
        private int money;                  //saldo del jugador

        //acciones
        private Boolean isRunning;
        private Boolean isJumping;
        private int reloadState;

        //fisicas
        private int reloadValue;
        private float reloadSpeed;
        private float reloadNedle;
        private int reloadSkillArea;

        //animacion
        int playerAnimFrame;

        //lista armas
        private List<Marker> buyedMarkers;

        //lista habilidades
        private List<Skill> skills;

        //nivel de la partida
        private int lvlGame;

        //Entrada
        private bool ticket;

        public Player(Camera camera, Vector2 initialPos) : base(camera)
        {
            //posicion inicial en el mapa
            elementPos = initialPos;
            elementScreenPos = Vector2.Zero;
            elementSize = new Vector2(Tile.TILE_SIZE, Tile.TILE_SIZE);

            //instancia de la camara y centrado
            this.camera = camera;
            centerCamera();

            //atributos
            elementSpeed = 3f;
            playerStamina = new Vector2(300, 300);
            money = 500;

            //acciones
            isRunning = false;
            isJumping = false;
            reloadState = 0;            //0 --> estado normal
                                        //1 --> recargando
                                        //2 --> recargado

            //fisicas
            reloadSpeed = 2f;
            reloadSkillArea = 0;

            //animacion
            playerAnimFrame = 0;

            //Armas compradas
            buyedMarkers = new List<Marker>();
            buyedMarkers.Add(new Marker(MarkerModel.SPYDER_VICTOR));

            //Habilidades compradas
            loadSkills();

            //color del traje
            suitColor = Color.LightSkyBlue;

            //equipo
            marker = new Marker(MarkerModel.SPYDER_VICTOR);
            for (int i = 0; i < 4; i++) pods.Add(new Pod("", "", Textures.prova_imatge, "40", 100));

            //texturas
            texture = Textures.bodyAnim;
            slice = new Rectangle(0, 0, (int)elementSize.X, (int)elementSize.Y);
            color1D = getPalette1D(texture, slice);

            //Nivel partida
            lvlGame = 1;

            //Entrada
            ticket = false;
        }

        public override void handleInput()
        {
            //handleinput de la camara
            camera.handleInput();
            if (Input.keyPressed(Controls.centerCamera)) centerCamera();

            //movimiento del personaje
            if (Math.Abs(calculateTrigonometry().Z) < elementSize.X / 10) return;

            //correr
            runPlayer(Input.keyDown(Controls.run));

            //saltar (no implementado)
            //jumpPlayer(Input.keyPressed(Controls.jump));

            //movimiento W, A, S, D
            if (reloadState == 0)
            {
                if (Input.keyDown(Controls.up))
                {
                    if (canGhostStep(MoveDir.FOWARD)) movePlayer(MoveDir.FOWARD);
                }
                else if (Input.keyDown(Controls.down))
                {
                    if (canGhostStep(MoveDir.BACK)) movePlayer(MoveDir.BACK);
                }
                else if (Input.keyDown(Controls.left))
                {
                    if (canGhostStep(MoveDir.LEFT)) movePlayer(MoveDir.LEFT);
                }
                else if (Input.keyDown(Controls.right))
                {
                    if (canGhostStep(MoveDir.RIGHT)) movePlayer(MoveDir.RIGHT);
                }
            }

            //recarga del personaje
            if (!StageScreen.isActive)
            {
                if (reloadState == 0 && pods.Count > 0 && Input.keyPressed(Controls.reload)) reloadInit();
                else if (reloadState == 1 && Input.keyPressed(Controls.reload)) reloadState = 2;
            }            

            //disparos del personaje
            if (!StageScreen.isActive) marker.handleInput(camera, elementPos, elementOffset, elementSize, new Vector2(Input.mousePos.X, Input.mousePos.Y));

            //trucos
            if (Input.keyPressed(Controls.invulnerability)) invulnerability = !invulnerability;
            if (Input.keyPressed(Controls.moneyUP)) money += 100;
        }

        public override void update()
        {
            //update del posicionamiento de los objetos en pantalla
            calculateScreenPos();

            //update del reload
            reloadUpdate();

            //update del arma
            marker.update();
        }

        public override void draw()
        {
            int posX = (int)(elementScreenPos.X + elementOffset.X);
            int posY = (int)(elementScreenPos.Y + elementOffset.Y);

            if (Input.mousePos.Y <= elementScreenPos.Y + elementOffset.Y + elementSize.Y / 2)
            {
                //dibujado del arma
                Globals.spriteBatch.Draw(marker.getTexture(), new Rectangle(posX + (int)elementSize.X / 2, posY + (int)elementSize.Y / 2, (int)elementSize.X, (int)elementSize.Y), calculateSlice(), Color.White, calculateRotation(), new Vector2(elementSize.X / 2, elementSize.Y / 2), SpriteEffects.None, 0);

                //dibujado de la cabeza
                Globals.spriteBatch.Draw(Textures.headAnim, new Rectangle(posX, posY, (int)elementSize.X, (int)elementSize.Y), calculateSlice(), Color.White);

                //dibujado de la mascara
                Globals.spriteBatch.Draw(Textures.maskAnim, new Rectangle(posX, posY, (int)elementSize.X, (int)elementSize.Y), calculateSlice(), Color.White);

                //dibujado de cuerpo
                Globals.spriteBatch.Draw(Textures.bodyAnim, new Rectangle(posX, posY, (int)elementSize.X, (int)elementSize.Y), calculateSlice(), suitColor);
            }
            else 
            {
                //dibujado de la cabeza
                Globals.spriteBatch.Draw(Textures.headAnim, new Rectangle(posX, posY, (int)elementSize.X, (int)elementSize.Y), calculateSlice(), Color.White);

                //dibujado de la mascara
                Globals.spriteBatch.Draw(Textures.maskAnim, new Rectangle(posX, posY, (int)elementSize.X, (int)elementSize.Y), calculateSlice(), Color.White);

                //dibujado de cuerpo
                Globals.spriteBatch.Draw(Textures.bodyAnim, new Rectangle(posX, posY, (int)elementSize.X, (int)elementSize.Y), calculateSlice(), suitColor);

                //dibujado del arma
                Globals.spriteBatch.Draw(marker.getTexture(), new Rectangle(posX + (int)elementSize.X / 2, posY + (int)elementSize.Y / 2, (int)elementSize.X, (int)elementSize.Y), calculateSlice(), Color.White, calculateRotation(), new Vector2(elementSize.X / 2, elementSize.Y / 2), SpriteEffects.None, 0);
            }

            //dibujado del reload
            if (reloadState > 0)
            {
                int reloadPosX = (int)(posX + elementSize.X / 2 - (100 / 2));
                int reloadPosY = (int)(posY + elementSize.Y + 10);
                Globals.spriteBatch.Draw(Textures.white, new Rectangle(reloadPosX, reloadPosY, 100, 10), Color.White);
                Globals.spriteBatch.Draw(Textures.white, new Rectangle(reloadPosX + reloadValue, reloadPosY, 5 + reloadSkillArea, 10), Color.Red);
                Globals.spriteBatch.Draw(Textures.white, new Rectangle(reloadPosX + (int)reloadNedle, reloadPosY, 2, 10), Color.Black);
            }
            //Globals.spriteBatch.DrawString(Fonts.arial_12, colisioned + "", new Vector2(700, 10), Color.White);
        }

        /** Guardar partida */
        public void savePlayer()
        {
            using (StreamWriter writer = new StreamWriter(SAVE_FILE))
            {
                //estado de la partida
                writer.WriteLine("pantalla:" + lvlGame);

                //dinero del personaje
                writer.WriteLine("dinero:" + money);

                //armas compradas
                writer.WriteLine("armas compradas:" + buyedMarkers.Count);
                foreach (Marker m in buyedMarkers)
                {
                    writer.WriteLine("nombre arma:" + m.getName());
                }

                //arma equipada
                writer.WriteLine("arma equipada:" + marker.getName());

                //cargadores
                writer.WriteLine("numero pods:" + pods.Count);

                //habilidades
                foreach (Skill s in skills)
                {
                    writer.WriteLine("nivel:" + s.getLevel().X);
                }

                //Entrada
                writer.WriteLine("entrada:" + ticket);
            }
        }

        /** Cargar partida */
        public void loadPlayer()
        {
            using (StreamReader reader = new StreamReader(SAVE_FILE))
            {
                //estado de la partida
                lvlGame = int.Parse(reader.ReadLine().Split(':')[1]);

                //dinero del personaje:
                money = int.Parse(reader.ReadLine().Split(':')[1]);

                //armas compradas
                buyedMarkers = new List<Marker>();
                int markersCount = int.Parse(reader.ReadLine().Split(':')[1]);
                for (int i = 0; i < markersCount; i++) {
                    buyedMarkers.Add(searchWeapon(reader.ReadLine().Split(':')[1]));
                }

                //arma equipada
                marker = searchWeapon(reader.ReadLine().Split(':')[1]);

                //cargadores
                pods = new List<Pod>();
                int podsCount = int.Parse(reader.ReadLine().Split(':')[1]);
                for (int i = 0; i < podsCount; i++)
                {
                    pods.Add(new Pod("", "", Textures.prova_imatge, "40", 100));
                }

                //habilidades
                foreach (Skill s in skills)
                {
                    Vector2 v = new Vector2(int.Parse(reader.ReadLine().Split(':')[1]), s.getLevel().Y);
                    s.setLevel(v);
                }
                updatePlayerSkills();

                //Entrada
                ticket = reader.ReadLine().Split(':')[1].Equals("True") ? true : false;
            }
        }



        #region METODOS SOBREESCRITOS

        protected override Vector3 calculateTrigonometry()
        {
            float x = Input.mousePos.X - (elementScreenPos.X + elementOffset.X + elementSize.X / 2);
            float y = Input.mousePos.Y - (elementScreenPos.Y + elementOffset.Y + elementSize.Y / 2);
            float h = (float)Math.Sqrt(x * x + y * y);
            return new Vector3(x, y, h);
        }

        protected override float calculateRotation()
        {
            Vector3 triangle = calculateTrigonometry();
            return (float)Math.Atan2(triangle.Y, triangle.X);
        }

        #endregion



        #region METODOS PRIVADOS

        /** Metodo que centra la camara en el jugador sin tener en cuenta su offset */
        private void centerCamera()
        {
            Vector2 newPos = new Vector2(
                (int)(elementPos.X - Globals.gameSize.X / Tile.TILE_SIZE / 2),
                (int)(elementPos.Y - Globals.gameSize.Y / Tile.TILE_SIZE / 2));
            camera.setCameraPos(newPos);
        } 

        /** Metodo que calcula que porcion de textura debe cargarse */
        private Rectangle calculateSlice()
        {
            int look = 0;
            if (Input.mousePos.Y > elementScreenPos.Y + elementOffset.Y + elementSize.Y / 2) look = 1;
            if (Input.mousePos.X < elementScreenPos.X + elementOffset.X + elementSize.X / 2) look += 2;
            slice = new Rectangle(playerAnimFrame * (int)elementSize.X, look * (int)elementSize.Y, (int)elementSize.X, (int)elementSize.Y);
            return slice;
        }

        /** Metodo que mueve al personaje */
        /** dir = dirección hacia la que se está moviendo el personaje */
        public void movePlayer(MoveDir dir)
        {
            Vector3 triangle = calculateTrigonometry();

            float relativeSpeed = elementSpeed;
            if (!isRunning) relativeSpeed = elementSpeed / 2;

            float rotation = calculateRotation();

            switch(dir)
            {
                case MoveDir.FOWARD:
                    elementOffset.X += (triangle.X / triangle.Z) * relativeSpeed;
                    elementOffset.Y += (triangle.Y / triangle.Z) * relativeSpeed;
                    break;

                case MoveDir.BACK:
                    elementOffset.X -= (triangle.X / triangle.Z) * relativeSpeed / 2;
                    elementOffset.Y -= (triangle.Y / triangle.Z) * relativeSpeed / 2;
                    break;

                case MoveDir.LEFT:
                    elementOffset.X -= (float)Math.Cos(rotation + Math.PI / 2) * relativeSpeed;
                    elementOffset.Y -= (float)Math.Sin(rotation + Math.PI / 2) * relativeSpeed;
                    break;

                case MoveDir.RIGHT:
                    elementOffset.X += (float)Math.Cos(rotation + Math.PI / 2) * relativeSpeed;
                    elementOffset.Y += (float)Math.Sin(rotation + Math.PI / 2) * relativeSpeed;
                    break;

            }
            calculateOffsets();

            //TODO: rectificar la animacion
            if (elementOffset.X != 0)
            {
                playerAnimFrame = (int)Math.Floor(Math.Abs(elementOffset.X) / elementSize.X * NUMBER_FRAMES);
                if (playerAnimFrame > NUMBER_FRAMES - 1) playerAnimFrame = 0;
            }
        }

        /** Metodo que hace correr al personaje */
        /** fast == true (corre), fast == false (no corre) */
        private void runPlayer(bool fast)
        {
            isRunning = fast;
            if (playerStamina.X < RUN_COST) isRunning = false;

            if (isRunning)
            {
                playerStamina.X -= RUN_COST;
                if (playerStamina.X <= 0) playerStamina.X = 0;
            }
            else
            {
                playerStamina.X += RUN_COST / STAMINA_INCREMENT;
                if (playerStamina.X >= playerStamina.Y) playerStamina.X = playerStamina.Y;
            }            
        }

        /** No se utiliza */
        /** Metodo que hace saltar al personaje */
        private void jumpPlayer(bool jump)
        {
            isJumping = jump;
            if (!jump) return;
            for (int i = 0; i < 30; i++) movePlayer(MoveDir.FOWARD);
        }
    
        /** Inicializacion del reload */
        private void reloadInit()
        {
            reloadState = 1;
            reloadNedle = 0;
            reloadValue = PaintToWinUtils.generateRandomNumber(50, 100 - (5 + reloadSkillArea));
        }

        /** Update del metodo reload */
        private void reloadUpdate()
        {
            if (reloadState == 0) return;
            reloadNedle += reloadSpeed;
            if (reloadNedle >= 100) reloadState = 0;
            if (reloadState == 2)
            {
                Rectangle area = new Rectangle(reloadValue, 0, 5, 10);
                int loadCharge = pods.Last().getCapacity() - (int)(Math.Abs(area.X - reloadNedle) * 4f);
                if (loadCharge < 0) loadCharge = 0;
                if (area.Contains(new Point((int)reloadNedle, 0))) loadCharge = pods.Last().getCapacity();
                marker.loadMarker(loadCharge);
                reloadState = 0;
                pods.RemoveAt(pods.Count - 1);
                GameScreen.map.auxElements.Add(new EmptyPod(camera, elementPos, elementOffset));                                 
            }
        }

        /** Método que mueve dos pasos al personaje fantasma para comprobar colisiones con el entorno */
        /** dir = dirección hacia la que se está moviendo el personaje */
        private bool canGhostStep(MoveDir dir)
        {
            Vector3 triangle = calculateTrigonometry();
            Vector2 auxOffset = elementOffset;
            bool canMove = true;

            float relativeSpeed = elementSpeed;
            if (!isRunning) relativeSpeed = elementSpeed / 2;

            float rotation = calculateRotation();

            for (int i = 0; i < 2; i++)
            {
                switch (dir)
                {
                    case MoveDir.FOWARD:
                        elementOffset.X += (triangle.X / triangle.Z) * relativeSpeed;
                        elementOffset.Y += (triangle.Y / triangle.Z) * relativeSpeed;
                        break;

                    case MoveDir.BACK:
                        elementOffset.X -= (triangle.X / triangle.Z) * relativeSpeed / 2;
                        elementOffset.Y -= (triangle.Y / triangle.Z) * relativeSpeed / 2;
                        break;

                    case MoveDir.LEFT:
                        elementOffset.X -= (float)Math.Cos(rotation + Math.PI / 2) * relativeSpeed;
                        elementOffset.Y -= (float)Math.Sin(rotation + Math.PI / 2) * relativeSpeed;
                        break;

                    case MoveDir.RIGHT:
                        elementOffset.X += (float)Math.Cos(rotation + Math.PI / 2) * relativeSpeed;
                        elementOffset.Y += (float)Math.Sin(rotation + Math.PI / 2) * relativeSpeed;
                        break;
                }
            }

            List<MapElement> elements;
            if (StageScreen.isActive)
            {
                elements = StageScreen.stageMap.getElements();
            }
            else
            {
                elements = GameScreen.map.getElements();
            }
            foreach (MapElement e in elements)
            {
                if (e is Obstacle && perPixelCollision(e))
                {
                    canMove = false;
                    break;
                }
            }
            elementOffset = auxOffset;
            return canMove;
        }

        /** Método que devuelve el arma según el nombre */
        private Marker searchWeapon(String name)
        {
            switch(name)
            {
                case "Spyder Victor":
                    return new Marker(MarkerModel.SPYDER_VICTOR);
                case "Tippmann 98":
                    return new Marker(MarkerModel.TIPPMANN_98);
                case "Tippmann A5 AK Model":
                    return new Marker(MarkerModel.TIPPMANN_A5);
                case "Empire Axe 2.0":
                    return new Marker(MarkerModel.EMPIRE_AXE);
                case "Eclipse Geo CSR":
                    return new Marker(MarkerModel.ECLIPSE_CSR);
                case "Rap4 T68":
                    return new Marker(MarkerModel.RAP4_T68);
            }
            return null;
        }

        /** Método que carga todas las habilidades */
        private void loadSkills()
        {
            skills = new List<Skill>();
            skills.Add(new Skill(SkillType.GENERIC));
            skills.Add(new Skill(SkillType.CHARGER));
            skills.Add(new Skill(SkillType.RELOAD));
            skills.Add(new Skill(SkillType.AIM));
            skills.Add(new Skill(SkillType.STAMINA));
        }

        /** Actualiza el personaje en función de las habilidades */
        private void updatePlayerSkills()
        {
            //update [1] de los pods
            pods = new List<Pod>();
            pods.Add(new Pod("", "", Textures.prova_imatge, "40", 100));
            for (int i = 0; i < skills[1].getLevel().X; i++) pods.Add(new Pod("", "", Textures.prova_imatge, "40", 100));

            //update [2] del reload
            reloadSkillArea = 5 + 2 * (int)skills[2].getLevel().X;

            //update [3] de la precision
            float accuBase = new Marker(marker.getModel()).getAccuracy();
            marker.setAccuracy(accuBase - 2 * skills[3].getLevel().X);
            if (marker.getAccuracy() < 5) marker.setAccuracy(5);

            //update [4] de la estamina
            playerStamina.Y = 200 + 50 * skills[4].getLevel().X;
            playerStamina.X = playerStamina.Y;
        }

        #endregion



        #region GETTERS Y SETTERS

        public Camera getCamera() { return camera; }
        public void setCamera(Camera camera) { this.camera = camera; }

        public Vector2 getPlayerStamina() { return playerStamina; }
        public void setPlayerStamina(Vector2 playerStamina) { this.playerStamina = playerStamina; }
        
        public List<Marker> getBuyedMarkers() { return buyedMarkers; }

        public List<Skill> getSkills() { return skills; }        

        public int getMoney() { return money; }
        public void setMoney(int money) { this.money = money; }

        public int getLvlGame() { return lvlGame; }
        public void setLvlGame(int lvlGame) { this.lvlGame = lvlGame; }

        public bool getTicket() { return ticket; }
        public void setTicket(bool ticket) { this.ticket = ticket; }

        public bool isInvulnerable() { return invulnerability; }

        #endregion

    }
}
