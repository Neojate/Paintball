using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Proyecto
{
    class GameScreen : BaseScreen
    {
        //Controla si el jugador ha ganado la partida
        public static bool playerWin;

        //instancia del mapa
        public static Map map;

        //instancia de la interfaz
        public GameInterface gameInterface;

        //PROVISIONAL tamaño del mapa
        private Vector2 mapSize;        

        public GameScreen()
        {
            Name = "GameScreen";
            State = ScreenState.Active;            

            //Controla si el jugador ha ganado la partida
            playerWin = false;

            //instancia al mapa
            mapSize = new Vector2(24, 32);
            map = new Map(mapSize, Vector2.Zero);

            //instancia a la camara
            //camera = new Camera();

            //instancia al player
            //player = new Player();

            //instancia a la interfaz
            gameInterface = new GameInterface(language);
        }

        public override void HandleInput()
        {
            //camera.handleInput();
            //player.handleInput();
            if (!InGameOptionsScreen.inGameActive) foreach (MapElement element in map.getElements()) element.handleInput();

            foreach (MapElement element in map.auxElements) map.getElements().Add(element);
            List<MapElement> deleteElement = new List<MapElement>();
            foreach (MapElement element in map.auxElements) deleteElement.Add(element);
            foreach (MapElement element in deleteElement) map.auxElements.Remove(element);
            foreach (MapElement e in map.deleteElements) map.getElements().Remove(e);
            map.deleteElements = new List<MapElement>();

            //Pausa la partida y muestra las opciones
            if (Input.keyPressed(Keys.Escape) && !InGameOptionsScreen.inGameActive) loadOptions();
        }

        public override void Update()
        {
            //update de la camara
            //camera.update();

            //update del personaje
            if (!InGameOptionsScreen.inGameActive) foreach (MapElement element in map.getElements()) element.update();
            //foreach (MapElement element in map.obstacles) element.update();

            //Pausa la partida hasta que finalize la cuenta atrás
            if (!StartEndScreen.countDown) InGameOptionsScreen.inGameActive = true;

            //Zona has perdido
            if (!map.getPlayer().isInvulnerable() && map.getPlayer().isColisioned() && StartEndScreen.countDown)
            {
                ScreenManager.AddScreen(new StartEndScreen(language.getMessage("startEnd_gameOver"), false));
            }

            //Zona has ganado
            if (map.numberEnemies <= 0 && StartEndScreen.countDown)
            {
                playerWin = true;
                ScreenManager.AddScreen(new StartEndScreen(language.getMessage("startEnd_win"), false));                
            }
        }

        public override void Draw()
        {
            Globals.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            //dibujar fondo negro
            Globals.spriteBatch.Draw(Textures.white, new Rectangle(0, 0, (int)Globals.gameSize.X, (int)Globals.gameSize.Y), Color.Black);

            //dibujar mapa
            map.draw();

            //dibujar elementos
            map.getElements().Sort();
            //foreach (MapElement element in map.obstacles) element.draw();
            foreach (MapElement element in map.getElements()) element.draw();
            

            //dibujar la interficie
            gameInterface.draw(map.getPlayer());

            Globals.spriteBatch.DrawString(Fonts.arial_12, map.getElements().Count + "", new Vector2(600, 10), Color.White);
            //Globals.spriteBatch.DrawString(Fonts.arial_12, map.deleteElements.Count + "", new Vector2(600, 40), Color.White);

            if (map.getPlayer().isInvulnerable())
            {
                Globals.spriteBatch.DrawString(Fonts.arial_12, language.getMessage("player_mode"), new Vector2(400, 10), Color.White);
            }
            
            Globals.spriteBatch.End();
        }        

        /** Carga la vista InGameOptions */
        private void loadOptions()
        {
            ScreenManager.AddScreen(new InGameOptionsScreen());
            InGameOptionsScreen.inGameActive = true;
        }
    }
}
