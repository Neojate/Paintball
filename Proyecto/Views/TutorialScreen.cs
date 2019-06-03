using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto
{
    class TutorialScreen : BaseScreen
    {
        //Fuentes
        private SpriteFont font12;
        private SpriteFont font14;

        //Botón
        private Vector2 buttonSize;

        //Teclado
        private Vector2 keyboardSize;

        //Filas y columnas
        private float[] columns;
        private float[] rows;

        public TutorialScreen()
        {
            Name = "TutorialScreen";
            State = ScreenState.Active;

            //Fuentes
            font12 = Fonts.arial_12;
            font14 = Fonts.arial_14;

            //Botón
            buttonSize = new Vector2(Globals.gameSize.X * 0.12f, Globals.gameSize.Y * 0.06f);

            //Teclado
            keyboardSize = new Vector2(Globals.gameSize.X * 0.28f, Globals.gameSize.Y * 0.13f);

            //Filas y columnas
            columns = new float[] { Globals.gameSize.X * 0.2f, Globals.gameSize.X * 0.5f, Globals.gameSize.X * 0.6f };
            rows = new float[] { Globals.gameSize.Y * 0.2f, Globals.gameSize.Y * 0.28f, Globals.gameSize.Y * 0.32f, Globals.gameSize.Y * 0.36f, Globals.gameSize.Y * 0.4f, Globals.gameSize.Y * 0.44f, Globals.gameSize.Y * 0.48f, Globals.gameSize.Y * 0.52f, Globals.gameSize.Y * 0.56f, Globals.gameSize.Y * 0.6f, Globals.gameSize.Y * 0.64f, Globals.gameSize.Y * 0.6f, Globals.gameSize.Y * 0.8f };

            //Creación de la interfaz
            createInterface();
        }

        public override void Update()
        {
            foreach (Component c in components) c.update();
        }

        public override void Draw()
        {
            Globals.spriteBatch.Begin();

            //Dibuja todos los componentes
            foreach (Component c in components) c.draw();

            //Titulo
            Globals.spriteBatch.DrawString(font14, language.getMessage("tutorial_controls"), new Vector2(centerText(language.getMessage("creditsScreen_name"), font14), rows[0] + 20), Color.White);

            //Recarga
            Globals.spriteBatch.DrawString(font12, "R: " + language.getMessage("tutorial_reload"), new Vector2( columns[0] + 20 ,rows[1]), Color.White);

            //Avanzar
            Globals.spriteBatch.DrawString(font12, "W: " + language.getMessage("tutorial_w_key"), new Vector2(columns[0] + 20, rows[2]), Color.White);

            //Retroceder
            Globals.spriteBatch.DrawString(font12, "S: " + language.getMessage("tutorial_s_key"), new Vector2(columns[0] + 20, rows[3]), Color.White);

            //Moverse a la derecha
            Globals.spriteBatch.DrawString(font12, "D: " + language.getMessage("tutorial_d_key"), new Vector2(columns[0] + 20, rows[4]), Color.White);

            //Moverse a la izquierda
            Globals.spriteBatch.DrawString(font12, "A: " + language.getMessage("tutorial_a_key"), new Vector2(columns[0] + 20, rows[5]), Color.White);

            //Correr
            Globals.spriteBatch.DrawString(font12, "SHIFT: " + language.getMessage("tutorial_shift_key"), new Vector2(columns[0] + 20, rows[6]), Color.White);

            //Hablar
            Globals.spriteBatch.DrawString(font12, "E: " + language.getMessage("tutorial_e_key"), new Vector2(columns[0] + 20, rows[7]), Color.White);

            //Centrar la cámara
            Globals.spriteBatch.DrawString(font12, "Q: " + language.getMessage("tutorial_q_key"), new Vector2(columns[0] + 20, rows[8]), Color.White);

            //Mostrar FPS
            Globals.spriteBatch.DrawString(font12, "F1: " + language.getMessage("tutorial_f1_key"), new Vector2(columns[0] + 20, rows[9]), Color.White);

            //Salir
            Globals.spriteBatch.DrawString(font12, "ESC: " + language.getMessage("tutorial_esc_key"), new Vector2(columns[0] + 20, rows[10]), Color.White);

            //Dibujado del teclado
            Globals.spriteBatch.Draw(Textures.keyboard, new Rectangle((int)columns[1] - 50, (int)(Globals.gameSize.Y * 0.5f), (int)keyboardSize.X, (int)keyboardSize.Y), Color.White);

            Globals.spriteBatch.End();
        }

        #region LISTENERS

        /** Salir de TutorialScreen */
        public static void exit()
        {
            ScreenManager.UnloadScreen("TutorialScreen");
        }

        #endregion

        #region METODOS PRIVADOS

        /** Creación de la interfaz */
        private void createInterface()
        {
            //Panel
            components.Add(new Panel(new Rectangle((int)columns[0], (int)rows[0], (int)columns[2], (int)rows[11]), Textures.white));
            components.Add(new Panel(new Rectangle((int)columns[0] + 1, (int)rows[0] + 1, (int)columns[2] - 2, (int)rows[11] - 2), Textures.background_menu));

            //Botón salir
            components.Add(new Button<TutorialScreen>(new Rectangle((int)(columns[1] - buttonSize.X / 2), (int)(rows[12] - buttonSize.Y - 10), (int)buttonSize.X, (int)buttonSize.Y), Textures.background_menu, Textures.hoverButton, "exit", null, "optionsscreen_exit", true));
        }

        /** Centrar texto en X */
        private int centerText(String text, SpriteFont font)
        {
            return PaintToWinUtils.centerTextX(new Vector2(0, Globals.gameSize.X), font, text);
        }

        #endregion
    }
}