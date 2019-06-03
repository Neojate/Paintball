using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Proyecto
{
    class CreditsScreen : BaseScreen
    {
        //Filas
        private float[] rows;

        //Fonts
        private SpriteFont font12;
        private SpriteFont font14;
        private SpriteFont font24;

        //Text
        private String albert;
        private String ruben;

        public CreditsScreen()
        {
            Name = "CreditsScreen";
            State = ScreenState.Active;

            //Filas
            rows = new float[] { Globals.gameSize.Y * 0.1f, Globals.gameSize.Y * 0.17f, Globals.gameSize.Y * 0.23f,
                Globals.gameSize.Y * 0.27f, Globals.gameSize.Y * 0.3f, Globals.gameSize.Y * 0.36f, Globals.gameSize.Y * 0.4f,
                Globals.gameSize.Y * 0.43f, Globals.gameSize.Y * 0.49f, Globals.gameSize.Y * 0.53f, Globals.gameSize.Y * 0.56f,
                Globals.gameSize.Y * 0.63f, Globals.gameSize.Y * 0.67f, Globals.gameSize.Y * 0.7f, Globals.gameSize.Y * 0.73f,
                Globals.gameSize.Y * 0.76f, Globals.gameSize.Y * 0.82f, Globals.gameSize.Y * 0.86f };

            //Fonts
            font12 = Fonts.arial_12;
            font14 = Fonts.arial_14;
            font24 = Fonts.arial_24;

            //Text
            albert = "Albert Alonso";
            ruben = "Rubén Gómez";
        }

        public override void HandleInput()
        {
            if (Input.keyPressed(Keys.Escape))
            {
                TitleScreen.showMenu = true;
                TitleScreen.titleActive = true;
                ScreenManager.UnloadScreen("CreditsScreen");
            }
        }

        public override void Update()
        {
            
        }

        public override void Draw()
        {
            Globals.spriteBatch.Begin();

            //Background
            Globals.spriteBatch.Draw(Textures.black, new Rectangle(0, 0, (int)Globals.gameSize.X, (int)Globals.gameSize.Y), Color.White);
            
            //Credits
            Globals.spriteBatch.DrawString(font24, language.getMessage("creditsScreen_name"), new Vector2(centerText(language.getMessage("creditsScreen_name"), font24), rows[0]), Color.White);

            //Development team
            Globals.spriteBatch.DrawString(font14, language.getMessage("creditsScreen_develop"), new Vector2(centerText(language.getMessage("creditsScreen_develop"), font14), rows[1]), Color.White);

            //Producer and project director
            Globals.spriteBatch.DrawString(font14, language.getMessage("creditsScreen_producer"), new Vector2(centerText(language.getMessage("creditsScreen_producer"), font14), rows[2]), Color.White);
            Globals.spriteBatch.DrawString(font12, albert, new Vector2(centerText(albert, font12), rows[3]), Color.White);
            Globals.spriteBatch.DrawString(font12, ruben, new Vector2(centerText(ruben, font12), rows[4]), Color.White);

            //Programmers
            Globals.spriteBatch.DrawString(font14, language.getMessage("creditsScreen_programmers"), new Vector2(centerText(language.getMessage("creditsScreen_programmers"), font14), rows[5]), Color.White);
            Globals.spriteBatch.DrawString(font12, albert, new Vector2(centerText(albert, font12), rows[6]), Color.White);
            Globals.spriteBatch.DrawString(font12, ruben, new Vector2(centerText(ruben, font12), rows[7]), Color.White);

            //Designers
            Globals.spriteBatch.DrawString(font14, language.getMessage("creditsScreen_designers"), new Vector2(centerText(language.getMessage("creditsScreen_designers"), font14), rows[8]), Color.White);
            Globals.spriteBatch.DrawString(font12, albert, new Vector2(centerText(albert, font12), rows[9]), Color.White);
            Globals.spriteBatch.DrawString(font12, ruben, new Vector2(centerText(ruben, font12), rows[10]), Color.White);

            //Testers
            Globals.spriteBatch.DrawString(font14, language.getMessage("creditsScreen_tester"), new Vector2(centerText(language.getMessage("creditsScreen_tester"), font14), rows[11]), Color.White);
            Globals.spriteBatch.DrawString(font12, "Eloy Nadal", new Vector2(centerText("Eloy Nadal", font12), rows[12]), Color.White);
            Globals.spriteBatch.DrawString(font12, "Rubén Sotillo", new Vector2(centerText("Rubén Sotillo", font12), rows[13]), Color.White);
            Globals.spriteBatch.DrawString(font12, "Samuel Román", new Vector2(centerText("Samuel Román", font12), rows[14]), Color.White);
            Globals.spriteBatch.DrawString(font12, "Manuel Bueno", new Vector2(centerText("Manuel Bueno", font12), rows[15]), Color.White);

            //Special Thanks
            Globals.spriteBatch.DrawString(font14, language.getMessage("creditsScreen_thanks"), new Vector2(centerText(language.getMessage("creditsScreen_thanks"), font14), rows[16]), Color.White);
            Globals.spriteBatch.DrawString(font12, "Carlos Faroles", new Vector2(centerText("Carlos Faroles", font12), rows[17]), Color.White);

            Globals.spriteBatch.End();
        }

        #region METODOS PRIVADOS

        /** Centrar texto en X */
        private int centerText(String text, SpriteFont font)
        {
            return PaintToWinUtils.centerTextX(new Vector2(0, Globals.gameSize.X), font, text);
        }

        #endregion
    }
}
