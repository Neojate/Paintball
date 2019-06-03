using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Proyecto
{
    class OptionsScreen : BaseScreen
    {
        //Resolución
        private static Vector2[] resolutions = { new Vector2(800, 600), new Vector2(1024, 768), new Vector2(1200, 800), new Vector2(1360, 768) };
        private static int resPos;

        //Modo visualización
        private static String[] modes;
        private static int modePos;

        //Dificultad
        private static String[] difficults;
        private static int diffPos;

        //Idiomas
        private static String[] languages;
        private static int langPos;

        private String[] menu;

        //Filas y columnas
        private float[] columns;
        private float[] rows;
        private int middleColumn;

        //Botones
        private static Vector2 buttonSizeLarge;
        private int buttonSize;

        //Fuentes
        private SpriteFont font;

        //Alerta
        private static Boolean showAlert = false;

        public OptionsScreen()
        {
            Name = "OptionsScreen";
            State = ScreenState.Active;

            //Fuentes
            font = Fonts.arial_12;

            //Tamaño botón
            buttonSize = (int)font.MeasureString(language.getMessage("optionsscreen_resolution")).Y;
            buttonSizeLarge = new Vector2(150, 30);

            //Filas y columnas
            columns = new float[] { Globals.gameSize.X * 0.2f, Globals.gameSize.X * 0.5f, Globals.gameSize.X * 0.6f, Globals.gameSize.X * 0.8f };
            rows = new float[] { Globals.gameSize.Y * 0.2f, Globals.gameSize.Y * 0.4f, Globals.gameSize.Y * 0.5f, Globals.gameSize.Y * 0.6f, Globals.gameSize.Y * 0.7f, Globals.gameSize.Y * 0.85f };

            //Panel
            components.Add(new Panel(new Rectangle((int)columns[0] - 30, (int)rows[1] - 30, (int)columns[3] - (int)columns[0] + buttonSize + 60, (int)rows[4] - (int)rows[1] + buttonSize + 60), Textures.background_menu));

            //Botones cambiar resolución
            components.Add(new Button<OptionsScreen>(new Rectangle((int)columns[1], (int)rows[1], buttonSize, buttonSize), Textures.leftArrow, Textures.leftArrowHover, "moveResPos", new Object[] { -1 }, null, false));
            components.Add(new Button<OptionsScreen>(new Rectangle((int)columns[3], (int)rows[1], buttonSize, buttonSize), Textures.rightArrow, Textures.rightArrowHover, "moveResPos", new Object[] { 1 }, null, false));

            //Botones cambiar modo de visualización
            components.Add(new Button<OptionsScreen>(new Rectangle((int)columns[1], (int)rows[2], buttonSize, buttonSize), Textures.leftArrow, Textures.leftArrowHover, "moveModePos", null, null, false));
            components.Add(new Button<OptionsScreen>(new Rectangle((int)columns[3], (int)rows[2], buttonSize, buttonSize), Textures.rightArrow, Textures.rightArrowHover, "moveModePos", null, null, false));

            //Botones cambiar dificultad
            components.Add(new Button<OptionsScreen>(new Rectangle((int)columns[1], (int)rows[3], buttonSize, buttonSize), Textures.leftArrow, Textures.leftArrowHover, "moveDiffPos", new Object[] { -1 }, null, false));
            components.Add(new Button<OptionsScreen>(new Rectangle((int)columns[3], (int)rows[3], buttonSize, buttonSize), Textures.rightArrow, Textures.rightArrowHover, "moveDiffPos", new Object[] { 1 }, null, false));

            //Botones cambiar idioma
            components.Add(new Button<OptionsScreen>(new Rectangle((int)columns[1], (int)rows[4], buttonSize, buttonSize), Textures.leftArrow, Textures.leftArrowHover, "moveLangPos", new Object[] { -1 }, null, false));
            components.Add(new Button<OptionsScreen>(new Rectangle((int)columns[3], (int)rows[4], buttonSize, buttonSize), Textures.rightArrow, Textures.rightArrowHover, "moveLangPos", new Object[] { 1 }, null, false));

            //Botones aceptar y salir
            components.Add(new Button<OptionsScreen>(new Rectangle((int)columns[0], (int)rows[5], (int)buttonSizeLarge.X, (int)buttonSizeLarge.Y), Textures.background_menu, Textures.hoverButton, "accept", null, "optionsscreen_accept", true));
            components.Add(new Button<OptionsScreen>(new Rectangle((int)columns[1], (int)rows[5], (int)buttonSizeLarge.X, (int)buttonSizeLarge.Y), Textures.background_menu, Textures.hoverButton, "exit", null, "optionsscreen_exit", true));

            //Texto modo visualización
            resPos = getResolutionPos();
            modes = new String[] { language.getMessage("optionsscreen_mode_fs"), language.getMessage("optionsscreen_mode_w") };
            modePos = (Globals.fullScreen) ? 0 : 1;

            //Texto dificultad
            difficults = new string[] { language.getMessage("optionsscreen_difficult_easy"), language.getMessage("optionsscreen_difficult_medium"), language.getMessage("optionsscreen_difficult_hard") };
            diffPos = Globals.difficult;

            //Texto idioma
            languages = new string[] { language.getMessage("optionsscreen_language_spn"), language.getMessage("optionsscreen_language_cat"), language.getMessage("optionsscreen_language_eng") };
            langPos = Globals.language;
        }

        public override void Update()
        {
            foreach (Component c in components) c.update();

            //Mensaje de alerta para reiniciar la aplicación al cambiar las opciones
            if (showAlert)
            {
                foreach (Component c in components) c.setEnabled(false);
                components.Add(new Alert<OptionsScreen>(new Rectangle(0, 0, 400, 200), Textures.background_menu, language.getMessage("optionsscreen_alert"), "exit", null));
                showAlert = false;
            }
        }

        public override void Draw()
        {
            String text = "";
            int posTextX;

            Globals.spriteBatch.Begin();

            foreach (Component c in components) c.draw();

            //Resolución
            Globals.spriteBatch.DrawString(font, language.getMessage("optionsscreen_resolution"), new Vector2(columns[0], rows[1]), Color.White);
            text = resolutions[resPos].X + "x" + resolutions[resPos].Y;
            posTextX = PaintToWinUtils.centerTextX(new Vector2(columns[1] + buttonSize, columns[3]), font, text);
            Globals.spriteBatch.DrawString(font, text, new Vector2(posTextX, rows[1]), Color.White);

            //Modo de visualización
            Globals.spriteBatch.DrawString(font, language.getMessage("optionsscreen_mode"), new Vector2(columns[0], rows[2]), Color.White);
            text = modes[modePos];
            posTextX = PaintToWinUtils.centerTextX(new Vector2(columns[1] + buttonSize, columns[3]), font, text);
            Globals.spriteBatch.DrawString(font, text, new Vector2(posTextX, rows[2]), Color.White);

            //Dificultad
            Globals.spriteBatch.DrawString(font, language.getMessage("optionsscreen_dificult"), new Vector2(columns[0], rows[3]), Color.White);
            text = difficults[diffPos];
            posTextX = PaintToWinUtils.centerTextX(new Vector2(columns[1] + buttonSize, columns[3]), font, text);
            Globals.spriteBatch.DrawString(font, text, new Vector2(posTextX, rows[3]), Color.White);

            //Idioma
            Globals.spriteBatch.DrawString(font, language.getMessage("optionsscreen_language"), new Vector2(columns[0], rows[4]), Color.White);
            text = languages[langPos];
            posTextX = PaintToWinUtils.centerTextX(new Vector2(columns[1] + buttonSize, columns[3]), font, text);
            Globals.spriteBatch.DrawString(font, text, new Vector2(posTextX, rows[4]), Color.White);

            foreach (Component c in components) if(c is Alert<OptionsScreen>) c.draw();

            Globals.spriteBatch.End();
        }

        #region LISTENERS

        /** Cambiar resolución */
        /** type = dirección a la que se mueve el puntero en el vector (izquierda = -1, derecha = 1) */
        public static void moveResPos(int type)
        {
            resPos += 1 * type;
            if (resPos < 0) resPos = resolutions.Length - 1;
            if (resPos >= resolutions.Length) resPos = 0;
        }

        /** Cambiar modo de visualización */
        public static void moveModePos()
        {
            modePos++;
            if (modePos < 0) modePos = modes.Length - 1;
            if (modePos >= modes.Length) modePos = 0;
        }

        /** Cambiar dificultad */
        /** type = dirección a la que se mueve el puntero en el vector (izquierda = -1, derecha = 1) */
        public static void moveDiffPos(int type)
        {
            diffPos += 1 * type;
            if (diffPos < 0) diffPos = difficults.Length - 1;
            if (diffPos >= difficults.Length) diffPos = 0;
        }

        /** Cambiar idioma */
        /** type = dirección a la que se mueve el puntero en el vector (izquierda = -1, derecha = 1) */
        public static void moveLangPos(int type)
        {
            langPos += 1 * type;
            if (langPos < 0) langPos = languages.Length - 1;
            if (langPos >= languages.Length) langPos = 0;
        }

        /** Guarda las opciones seleccionadas para aplicar-las al reiniciar la aplicación */
        public static void accept()
        {
            Options options = new Options();
            options.resolution = resolutions[resPos];
            options.fullScreen = modePos == 0 ? true : false;
            options.difficult = diffPos;
            options.language = langPos;
            options.save();
            showAlert = true;
        }

        /** Salir de OptionsScreen */
        public static void exit()
        {
            TitleScreen.showMenu = true;
            ScreenManager.UnloadScreen("OptionsScreen");
        }

        #endregion

        #region METODOS PRIVADOS

        /** Cambiar la resolución */
        private int getResolutionPos()
        {
            for (int i = 0; i < resolutions.Length; i++)
            {
                if (Globals.gameSize.X == resolutions[i].X && Globals.gameSize.Y == resolutions[i].Y) return i;
            }
            return 0;
        }

        #endregion
    }
}

