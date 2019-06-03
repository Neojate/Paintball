using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Proyecto
{
    class StageMap
    {
        //idioma
        private Language language;

        //mapa
        private Vector2 mapSize;
        private Tile[,] scenario;

        //elementos del mapa
        private List<MapElement> elements;

        //camara y player
        private Camera camera;
        private Player player;

        //tamaño casilla
        int tileSize;

        //fisicas
        private bool triggerActive;
        private Trigger trigger;

        //mensaje de compra
        private int messagePos;
        private String message;

        public StageMap(Vector2 mapSize)
        {
            //idioma
            language = new Language();

            //tamaño mapa
            this.mapSize = mapSize;

            //tamaño casilla
            tileSize = Tile.TILE_SIZE;

            //camara y player
            camera = new Camera();
            player = new Player(camera, new Vector2(10, 5)); 
            player.loadPlayer();           

            //mapa
            loadScenario();
            loadElements();

            //Texto del trigger
            message = language.getMessage("trigger_talk");
            messagePos = PaintToWinUtils.centerTextX(new Vector2(0, Globals.gameSize.X), Fonts.arial_12, message);

            //Calcula cuanto dinero recibirá el jugador si gana la partida
            if (GameScreen.playerWin)
            {
                int k = 0;

                switch (Globals.difficult)
                {
                    case 0:
                        k = 400;
                        break;
                    case 1:
                        k = 300;
                        break;
                    case 2:
                        k = 200;
                        break;
                }

                player.setMoney(player.getMoney() + player.getLvlGame() * k);
                player.setLvlGame(player.getLvlGame() + 1);                
            }
        }

        public void handleInput()
        {
            if (triggerActive && Input.keyPressed(Controls.talk))
            {
                launchTriggerScript();
            }
            if (Input.keyPressed(Microsoft.Xna.Framework.Input.Keys.F6)) player.savePlayer();

            //Salir de StageScreen
            if (Input.keyPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                TitleScreen.showMenu = true;
                TitleScreen.titleActive = true;
                ScreenManager.UnloadScreen("StageScreen");
                if (screenActive("MarketScreen")) ScreenManager.UnloadScreen("MarketScreen");
                if (screenActive("SkillScreen")) ScreenManager.UnloadScreen("SkillScreen");
                if (screenActive("TutorialScreen")) ScreenManager.UnloadScreen("TutorialScreen");
                player.savePlayer();                
            }
        }

        public void update()
        {
            trigger = scenario[(int)player.getElementPos().X, (int)player.getElementPos().Y].getTrigger();
            if (trigger != null)
            {
                triggerActive = true;
            }
            else
            {
                triggerActive = false;
            }            
        }

        public void draw()
        {
            Vector2 cameraPos = player.getCamera().getCameraPos();
            Vector2 cameraOffset = player.getCamera().getCameraOffset();

            for (int drawX = -1; drawX < Globals.gameSize.X / tileSize; drawX++)
            {
                for (int drawY = -1; drawY < Globals.gameSize.Y / tileSize; drawY++)
                {
                    int x = (int)cameraPos.X + drawX;
                    int y = (int)cameraPos.Y + drawY;

                    if (x >= 0 && x <= Globals.gameSize.X && y >= 0 && y <= Globals.gameSize.Y && x < mapSize.X && y < mapSize.Y)
                    {
                        //dibujo de las celdas
                        Globals.spriteBatch.Draw(scenario[x, y].getTexture(), new Rectangle(drawX * tileSize + (int)cameraOffset.X, drawY * tileSize + (int)cameraOffset.Y, tileSize, tileSize), scenario[x, y].getSlice(), Color.White);

                        //dibujo de las coordenadas
                        //Globals.spriteBatch.DrawString(Fonts.arial_12, x + "\n" + y, new Vector2(drawX * tileSize + (int)cameraOffset.X, drawY * tileSize + (int)cameraOffset.Y), Color.Green);
                    }
                }
            }

            //texto del trigger
            if (triggerActive) Globals.spriteBatch.DrawString(Fonts.arial_12, trigger.getTriggermessage(), new Vector2(messagePos, Globals.gameSize.Y * 0.9f), Color.White);

            //sprite carro
            Globals.spriteBatch.Draw(Textures.icon, new Rectangle(20, (int)Globals.gameSize.Y - 100, 25, 25), new Rectangle(32, 0, 32, 32), Color.White);

            //sprite munición
            Globals.spriteBatch.Draw(Textures.icon, new Rectangle(20, (int)Globals.gameSize.Y - 50, 25, 25), new Rectangle(0, 0, 32, 32), Color.White);

            //sprite y texto ticket
            if (player.getTicket())
            {
                Globals.spriteBatch.Draw(Textures.icon, new Rectangle(15, (int)Globals.gameSize.Y - 150, 32, 32), new Rectangle(64, 0, 32, 32), Color.White);
                Globals.spriteBatch.DrawString(Fonts.arial_12, "Ticket", new Vector2(48, Globals.gameSize.Y - 142), Color.White);
            }

            //imprimir lvlGame
            Globals.spriteBatch.DrawString(Fonts.arial_12, language.getMessage("stageMap_lvl") + player.getLvlGame(), new Vector2(20, Globals.gameSize.Y - 180), Color.White);

            //imprimir dinero
            Globals.spriteBatch.DrawString(Fonts.arial_12, player.getMoney() + "", new Vector2(48, Globals.gameSize.Y - 93), Color.White);

            //imprimir numero pods
            Globals.spriteBatch.DrawString(Fonts.arial_12, player.getPods().Count + "", new Vector2(48, Globals.gameSize.Y - 43), Color.White);
        }

        #region METODOS PRIVADOS

        /** Carga el mapa */
        private void loadScenario()
        {
            scenario = new Tile[(int)mapSize.X, (int)mapSize.Y];
            for (int y = 0; y < mapSize.Y; y++)
            {
                for (int x = 0; x < mapSize.X; x++)
                {
                    scenario[x, y] = new Tile(TileType.FLOOR);
                }
            }
            for (int i = 1; i < 4; i++) scenario[i, 2].setTrigger(new Trigger(TriggerType.SHOP_EQUIP));
            for (int i = 5; i < 8; i++) scenario[i, 2].setTrigger(new Trigger(TriggerType.SHOP_SKILLS));
            for (int i = 9; i < 11; i++) scenario[i, 0].setTrigger(new Trigger(TriggerType.DOOR_ENTRANCE));
            for (int i = 17; i < 19; i++) scenario[i, 1].setTrigger(new Trigger(TriggerType.SCORE_BOARD));
            for (int i = 10; i < 12; i++) scenario[i, 1].setTrigger(new Trigger(TriggerType.TICKET_SELLER));
        }

        /** Carga los elementos del mapa */
        private void loadElements()
        {            
            elements = new List<MapElement>();

            //player
            elements.Add(player);

            //tienda 1
            elements.Add(new Obstacle(ObstacleType.SHOP_LEFT_TOP, camera, new Vector2(1,1), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.SHOP_MID_TOP, camera, new Vector2(2, 1), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.SHOP_RIGHT_TOP, camera, new Vector2(3, 1), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.SHOP_LEFT_BOT, camera, new Vector2(1, 2), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.SHOP_MID_BOT, camera, new Vector2(2, 2), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.SHOP_RIGHT_BOT, camera, new Vector2(3, 2), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.SHOOPKEEPER, camera, new Vector2(2, 1), new Vector2(0, 15)));

            //tienda 2
            elements.Add(new Obstacle(ObstacleType.SCHOOL_LEFT_TOP, camera, new Vector2(5, 1), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.SCHOOL_MID_TOP, camera, new Vector2(6, 1), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.SCHOOL_RIGHT_TOP, camera, new Vector2(7, 1), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.SCHOOL_LEFT_BOT, camera, new Vector2(5, 2), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.SCHOOL_MID_BOT, camera, new Vector2(6, 2), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.SCHOOL_RIGHT_BOT, camera, new Vector2(7, 2), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.SKILLKEEPER, camera, new Vector2(6, 1), new Vector2(0, 15)));

            //puerta
            elements.Add(new Obstacle(ObstacleType.DOOR_LEFT_TOP, camera, new Vector2(9, -1), new Vector2(0, 1)));
            elements.Add(new Obstacle(ObstacleType.DOOR_RIGHT_TOP, camera, new Vector2(10, -1), new Vector2(0, 1)));
            elements.Add(new Obstacle(ObstacleType.DOOR_LEFT_BOT, camera, new Vector2(9, 0), new Vector2(0, 1)));
            elements.Add(new Obstacle(ObstacleType.DOOR_RIGHT_BOT, camera, new Vector2(10, 0), new Vector2(0, 1)));

            //pared top
            for (int i = 1; i < mapSize.X - 1; i++)
            {
                elements.Add(new Obstacle(ObstacleType.STAGE_WALL_TOP, camera, new Vector2(i, -1), Vector2.Zero));
                elements.Add(new Obstacle(ObstacleType.STAGE_WALL_BOT, camera, new Vector2(i, 0), Vector2.Zero));
            }
            elements.Add(new Obstacle(ObstacleType.STAGE_CORNER_TOP_LEFT_TOP, camera, new Vector2(0, -1), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.STAGE_CORNER_TOP_LEFT_BOT, camera, new Vector2(0, 0), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.STAGE_CORNER_TOP_RIGHT_TOP, camera, new Vector2(mapSize.X - 1, -1), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.STAGE_CORNER_TOP_RIGHT_BOT, camera, new Vector2(mapSize.X - 1, 0), Vector2.Zero));
            for (int i = 1; i < mapSize.Y - 1; i++)
            {
                elements.Add(new Obstacle(ObstacleType.STAGE_WALL_LEFT, camera, new Vector2(0, i), Vector2.Zero));
                elements.Add(new Obstacle(ObstacleType.STAGE_WALL_RIGHT, camera, new Vector2(mapSize.X - 1, i), Vector2.Zero));
            }

            //pared bot
            elements.Add(new Obstacle(ObstacleType.STAGE_CORNER_BOT_LEFT, camera, new Vector2(0, 9), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.STAGE_CORNER_BOT_RIGHT, camera, new Vector2(21, 9), Vector2.Zero));
            for (int i = 1; i < mapSize.X - 1; i++)
            {
                elements.Add(new Obstacle(ObstacleType.STAGE_BOT_MID, camera, new Vector2(i, 9), Vector2.Zero));                
            }

            //pizarra
            elements.Add(new Obstacle(ObstacleType.BOARD_LEFT, camera, new Vector2(17, 0), new Vector2(0, 20)));
            elements.Add(new Obstacle(ObstacleType.BOARD_RIGHT, camera, new Vector2(18, 0), new Vector2(0, 20)));

            //planta
            elements.Add(new Obstacle(ObstacleType.PLANT, camera, new Vector2(20, 0), new Vector2(0, 10)));

            //vendedor entradas
            elements.Add(new Obstacle(ObstacleType.TICKET_SELLER, camera, new Vector2(11, 0), new Vector2(0, 16)));
        }

        /** Comprueba si la pantalla que pasamos por parámetro está activa */
        /** screen = pantalla que queremos comprobar si está activa */
        private static Boolean screenActive(String screen)
        {
            foreach (BaseScreen activeScreen in ScreenManager.activeScreens())
            {
                if (activeScreen.Name.Equals(screen))
                {
                    return true;
                }
            }
            return false;
        }
        
        /** Método que ejecuta el trigger activado */
        private void launchTriggerScript()
        {
            switch(trigger.getTriggerType())
            {
                case TriggerType.SHOP_EQUIP:
                    if(!screenActive("MarketScreen")) ScreenManager.AddScreen(new MarketScreen(player));
                    break;
                case TriggerType.SHOP_SKILLS:
                    if (!screenActive("SkillScreen")) ScreenManager.AddScreen(new SkillScreen(player));
                    break;
                case TriggerType.DOOR_ENTRANCE:
                    if (player.getTicket())
                    {
                        player.setTicket(false);
                        startGame();                                                
                        ScreenManager.AddScreen(new WeatherScreen()); //Añade lluvia a la partida
                    }
                    break;
                case TriggerType.TICKET_SELLER:
                    if (!screenActive("TicketScreen")) ScreenManager.AddScreen(new TicketScreen(player));
                    break;
                case TriggerType.SCORE_BOARD:
                    if (!screenActive("TutorialScreen")) ScreenManager.AddScreen(new TutorialScreen());
                    break;
            }
        }

        /** Iniciar partida */
        private void startGame()
        {
            StageScreen.isActive = false;
            player.savePlayer();
            ScreenManager.UnloadScreen("StageScreen");
            ScreenManager.AddScreen(new GameScreen());

            //Panel de información
            ScreenManager.AddScreen(new StartEndScreen("", true));
        }

        #endregion



        #region GETTERS Y SETTERS

        public List<MapElement> getElements() { return elements; }

        public Player getPlayer() { return player; }

        #endregion

    }
}
