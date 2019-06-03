using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Proyecto
{
    class Debug : BaseScreen
    {
        //Información pantalla
        public String screens = "";
        public String focusScreen = "";
        
        //Contador de fps
        private int fps;
        private int fpsCounter;
        private Double fpsTimer;
        private String fpsText = "";

        //Rectangulo background
        private Rectangle bGRect;

        private Boolean keyDown = false;

        public Debug()
        {
            Name = "Debug";
            State = ScreenState.Hidden;
            GrabFocus = false;

            State = ScreenState.Active;
        }

        public override void HandleInput()
        {                        
            if (Input.keyPressed(Keys.F1))
            {
                if (State == ScreenState.Active){
                    State = ScreenState.Hidden;
                }else if (State == ScreenState.Hidden)
                {
                    State = ScreenState.Active;
                }
            }
        }

        public override void Update()
        {
            base.Update();

            if(screens.Length > 0)
            {
                screens = screens.Substring(0, screens.Length - 2);
            }

            int txtWidth = 0;
            int txtHeight = 0;


            if(Fonts.arial_12.MeasureString(screens).X > txtWidth)
            {
                txtWidth = (int)Fonts.arial_12.MeasureString(screens).X;
            }

            if(Fonts.arial_12.MeasureString(focusScreen).X > txtWidth)
            {
                txtWidth = (int)Fonts.arial_12.MeasureString(focusScreen).X;
            }
            txtHeight = (int)Fonts.arial_12.MeasureString(fpsText).Y * 3;
            bGRect = new Rectangle(0, 0, txtWidth + 20, txtHeight + 20);
        }

        public override void Draw()
        {
            base.Draw();
            if(Globals.gameTime.TotalGameTime.TotalMilliseconds > fpsTimer)
            {
                fps = fpsCounter;
                fpsTimer = Globals.gameTime.TotalGameTime.TotalMilliseconds + 1000;
                fpsCounter = 1;
                fpsText = "FPS: " + fps;
            }
            else
            {
                fpsCounter += 1;
            }

            Globals.spriteBatch.Begin();
            Globals.spriteBatch.Draw(Textures.prova_imatge, bGRect, Color.Black * 0.6F);
            Globals.spriteBatch.DrawString(Fonts.arial_12, fpsText, new Vector2(10, 10), Color.White);
            Globals.spriteBatch.DrawString(Fonts.arial_12, screens, new Vector2(10, 22), Color.White);
            Globals.spriteBatch.DrawString(Fonts.arial_12, focusScreen, new Vector2(10, 34), Color.White);
            Globals.spriteBatch.End();
        }
    }
}
