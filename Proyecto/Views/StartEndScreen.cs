using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto
{
    class StartEndScreen : BaseScreen
    {
        //Pausa la partida hasta que finalize la cuenta atrás
        public static bool countDown;

        //Texto que se mostrará por pantalla
        String text;
        SpriteFont font;

        //Posición del texto
        Vector2 textPos;

        //Contador
        bool countTimer;
        int firstTime;

        int timeToClose;

        public StartEndScreen(String text, bool countTimer)
        {
            Name = "StartEndScreen";
            State = ScreenState.Active;

            //Pausa la partida hasta que finalize la cuenta atrás
            countDown = false;

            //Texto
            this.text = text;

            //Contador            
            this.countTimer = countTimer;
            firstTime = 0;

            //Fuente
            font = Fonts.arial_60;

            timeToClose = 2000;
            if (countTimer) timeToClose = 4000;

        }

        public override void Update()
        {
            //Incrementa el valor del contador
            firstTime += (int)Globals.gameTime.ElapsedGameTime.TotalMilliseconds;

            //Muestra el contador
            if (countTimer)
            {
                text = (3 - firstTime / 1000).ToString();
                if (firstTime >= 3000) text = "GO";
            }

            //Pasados 4 segundos sale de la pantalla
            if (firstTime >= timeToClose)
            {
                InGameOptionsScreen.inGameActive = false;
                countDown = true;
                ScreenManager.UnloadScreen("StartEndScreen");

                if (!countTimer)
                {
                    ScreenManager.UnloadScreen("WeatherScreen");
                    ScreenManager.UnloadScreen("GameScreen");
                    ScreenManager.AddScreen(new StageScreen());
                }

            }
        }

        public override void Draw()
        {
            Globals.spriteBatch.Begin();

            //Posicionamiento del texto
            textPos = new Vector2(
                PaintToWinUtils.centerTextX(new Vector2(0, Globals.gameSize.X), font, text),
                PaintToWinUtils.centerTextY(new Vector2(0, Globals.gameSize.Y), font, text));

            //Dibujado del texto
            Globals.spriteBatch.DrawString(font, text, textPos, Color.White);

            Globals.spriteBatch.End();
        }
    }
}