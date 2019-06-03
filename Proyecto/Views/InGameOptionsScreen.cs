using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Proyecto
{
    class InGameOptionsScreen : BaseScreen
    {
        //Botón
        private static Vector2 buttonSize = new Vector2(150, 30);

        //Filas y columnas
        private float column = Globals.gameSize.X * 0.2f;
        private float[] rows = { Globals.gameSize.Y * 0.4f, Globals.gameSize.Y * 0.45f, Globals.gameSize.Y * 0.115f };

        //Controla si InGameOptions está activa
        public static Boolean inGameActive;

        public InGameOptionsScreen()
        {
            Name = "InGameOptionsScreen";
            State = ScreenState.Active;

            //Creación de la interfaz
            createInterface();
        }

        public override void HandleInput()
        {
            //Salimos de InGameOptions
            if (Input.keyPressed(Keys.Escape)) continuePlaying();
        }

        public override void Update()
        {
            foreach (Component c in components) c.update();
        }

        public override void Draw()
        {
            Globals.spriteBatch.Begin();

            //dibujado de la interfaz
            foreach (Component c in components) c.draw();

            Globals.spriteBatch.End();
        }

        #region LISTENERS
        /** Cierra la vista InGameOptions para poder seguir jugando */
        public static void continuePlaying()
        {
            ScreenManager.UnloadScreen("InGameOptionsScreen");
            inGameActive = false;
        }

        /** Sale a la pantalla del título */
        public static void exit()
        {
            TitleScreen.titleActive = true;
            ScreenManager.UnloadScreen("InGameOptionsScreen");
            ScreenManager.UnloadScreen("GameScreen");
            ScreenManager.UnloadScreen("WeatherScreen");
            ScreenManager.AddScreen(new StageScreen());
            inGameActive = false;
        }
        #endregion

        #region METODOS PRIVADOS
        /** Creación de la interfaz */
        private void createInterface()
        {
            //Acciones de los botones
            String[] actions = new string[] { "continuePlaying", "exit" };

            //Texto de los botones
            String[] menuCode = new string[] { "menuInGame_continue", "menuInGame_surrender" };

            int panelHeight = Globals.gameSize.X == 800 ? (int)(Globals.gameSize.Y * 0.132f) : (int)(Globals.gameSize.Y * 0.115f);

            //Creación del panel
            components.Add(new Panel(new Rectangle((int)column - 10, (int)rows[0] - 10, (int)buttonSize.X + 20, panelHeight), Textures.background_menu));

            //Creación de los botones
            for (int i = 0; i < actions.Length; i++)
            {
                components.Add(new Button<InGameOptionsScreen>(new Rectangle((int)column + 1, (int)rows[i] + 1, (int)buttonSize.X - 2, (int)buttonSize.Y - 2), Textures.background_menu, Textures.hoverButton, actions[i], null, menuCode[i], true));
            }
        }
        #endregion
    }
}

