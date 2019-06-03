using Microsoft.Xna.Framework;
using System;

namespace Proyecto
{
    public class GameInterface
    {
        //Barra estamina
        private const int STAMINA_BAR_HEIGHT = 14;
        private Vector2 staminaPos;

        //Idioma
        Language language;

        //Munición
        private Vector2 ammoPos;
        private Vector2 ammoSize;

        public GameInterface(Language language)
        {
            //instancia al lenguaje
            this.language = language;

            //posición de la barra de estamina
            staminaPos = new Vector2(Globals.gameSize.X * 0.025f, Globals.gameSize.Y * 0.05f);

            //posición de la municion
            ammoPos = new Vector2(Globals.gameSize.X * 0.85f, Globals.gameSize.Y * 0.85f);
            ammoSize = new Vector2(Fonts.arial_12.MeasureString("200 / 200").X, Fonts.arial_12.MeasureString("200 / 200").X /2);
        }

        public void draw(Player player)
        {
            //dibujar la estamina
            Globals.spriteBatch.DrawString(Fonts.arial_12, language.getMessage("gamescreen_stamina"), new Vector2(staminaPos.X, staminaPos.Y - Fonts.arial_12.MeasureString("A").Y - 5), Color.White);
            Globals.spriteBatch.Draw(Textures.prova_imatge, new Rectangle((int)staminaPos.X, (int)staminaPos.Y, (int)player.getPlayerStamina().Y + 4, STAMINA_BAR_HEIGHT), Color.White);
            Globals.spriteBatch.Draw(Textures.prova_imatge, new Rectangle((int)staminaPos.X + 2, (int)staminaPos.Y + 2, (int)player.getPlayerStamina().X, STAMINA_BAR_HEIGHT - 4), Color.Red);

            //dibujar la munición
            Globals.spriteBatch.Draw(Textures.pod, new Rectangle((int)ammoPos.X, (int)ammoPos.Y, (int)ammoSize.X, (int)ammoSize.Y), Color.White);
            String text = player.getMarker().getLoader().X + " / " + player.getMarker().getLoader().Y;
            Globals.spriteBatch.DrawString(Fonts.arial_12, text, ammoPos, Color.White);
        }
    }
}
