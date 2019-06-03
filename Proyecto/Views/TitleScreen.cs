using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;

namespace Proyecto
{
    class TitleScreen : BaseScreen
    {
        //Botón
        private static Vector2 buttonSize = new Vector2(150, 30);

        //Filas y columnas
        private float column = Globals.gameSize.X * 0.2f;
        private float[] rows = { Globals.gameSize.Y * 0.4f, Globals.gameSize.Y * 0.5f, Globals.gameSize.Y * 0.6f, Globals.gameSize.Y * 0.7f, Globals.gameSize.Y * 0.8f };

        //Video
        private static VideoPlayer videoPlayer;

        //Menú inicial
        public static Boolean showMenu;

        //Pantalla actual activa
        public static bool titleActive;

        //Alert
        private static bool showAlert;
        private static bool alertShowed;
        private static List<Component> auxComponents;
        private static Component auxComponent;

        public TitleScreen()
        {
            Name = "TitleScreen";
            State = ScreenState.Active;
            
            //Pantalla actual activa
            titleActive = true;

            //Alert
            showAlert = false;
            alertShowed = false;
            auxComponents = new List<Component>();

            //Creación de la interfaz
            createInterface();

            //Cargado del video
            videoPlayer = new VideoPlayer();

            if (videoPlayer.State == MediaState.Stopped)
            {
                videoPlayer = new VideoPlayer();

                videoPlayer.Play(Videos.background);
            }
        }

        public override void HandleInput()
        {
            
        }

        public override void Update()
        {
            //Mostrado interfaz
            foreach (Component c in components) c.setVisibility(showMenu);

            //Update video
            if (videoPlayer.State == MediaState.Stopped && titleActive) videoPlayer.Play(Videos.background);

            //Update Interfaz
            foreach (Component c in components) c.update();

            //Mensaje de alerta antes de iniciar una nueva partida
            if (showAlert)
            {
                foreach (Component c in components) c.setEnabled(false);
                Component alert = new Alert<TitleScreen>(new Rectangle(0, 0, 400, 200), Textures.background_menu, language.getMessage("menutitle_confirm_newgame"), "exitAlert", null);
                components.Add(alert);
                auxComponent = alert;
                showAlert = false;
                alertShowed = true;
            }
            foreach(Component c in auxComponents)
            {
                components.Remove(c);
                foreach (Component com in components) com.setEnabled(true);
            }
            auxComponents = new List<Component>();
        }

        public override void Draw()
        {
            base.Draw();            

            Globals.spriteBatch.Begin();

            //Dibujado del fondo
            Globals.spriteBatch.Draw(videoPlayer.GetTexture(), new Rectangle(0, 0, (int)Globals.gameSize.X, (int)Globals.gameSize.Y), Color.White);

            //Dibujado de la interfaz
            foreach (Component c in components) c.draw();

            //Dibujado del título
            Globals.spriteBatch.Draw(Textures.p2w_title, new Rectangle(50, 100, (int)(Globals.gameSize.X * 0.4f), (int)(Globals.gameSize.Y * 0.2f)), Color.White);

            Globals.spriteBatch.End();
        }

        public override void Unload()
        {
            base.Unload();
        }

        #region LISTENERS

        /** Confirmar nueva partida */
        public static void newGame()
        {
            if (alertShowed)
            {
                loadNewGame();
                alertShowed = false;
            }
            else
            {
                showAlert = true;
            }                        
        } 
        
        /** Salir del alert de nuevo juego */
        public static void exitAlert()
        {
            auxComponents.Add(auxComponent);
        }

        /** Continuar partida */
        public static void loadContinue()
        {
            if (!File.Exists(Player.SAVE_FILE)) return;
            GameScreen.playerWin = false;
            showMenu = false;
            titleActive = false;
            Player player = new Player(new Camera(), new Vector2(10, 10));
            ScreenManager.AddScreen(new StageScreen());
        }

        /** Carga la pantalla de opciones */
        public static void loadOptions()
        {
            showMenu = false;

            ScreenManager.AddScreen(new OptionsScreen());
        }

        /** Carga la pantalla de créditos */
        public static void loadCredits()
        {
            showMenu = false;
            titleActive = false;
            ScreenManager.AddScreen(new CreditsScreen());
        }

        /** Cierra el juego */
        public static void loadExit()
        {
            Program.p2w.Exit();
        }
        #endregion

        #region MÉTODOS PRIVADOS

        /** Empezar nueva partida */
        private static void loadNewGame()
        {
            GameScreen.playerWin = false;
            showMenu = false;
            titleActive = false;
            Player player = new Player(new Camera(), new Vector2(10, 10));
            player.savePlayer();
            ScreenManager.AddScreen(new StageScreen());
        }

        /** Creación de la interfaz **/
        private void createInterface()
        {
            showMenu = true;

            //Métodos a los que llama cada botón
            String[] actions = new string[] { "newGame", "loadContinue", "loadOptions", "loadCredits", "loadExit" };

            //Texto de los botones
            String[] menuCode = new string[] { "menutitle_option_newgame", "menutitle_option_continue", "menutitle_option_option", "menutitle_option_credits", "menutitle_option_exit" };

            //Panel
            components.Add(new Panel(new Rectangle((int)column - 10, (int)rows[0] - 10, (int)buttonSize.X + 20, (int)buttonSize.Y + 20 + (int)rows[0]), Textures.background_menu));

            //Botones
            for (int i = 0; i < actions.Length; i++)
            {
                components.Add(new Button<TitleScreen>(new Rectangle((int)column + 1, (int)rows[i] + 1, (int)buttonSize.X - 2, (int)buttonSize.Y - 2), Textures.background_menu, Textures.hoverButton, actions[i], null, menuCode[i], true));
            }
        }

        #endregion
    }
}

