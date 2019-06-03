using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Proyecto
{
    class Alert<T> : Component
    {
        //Alert
        private Vector2 alertPos;

        //Texto
        private String text;
        private Vector2 textPos;

        //Botón
        private Vector2 buttonSize;
        private Button<T> button;        

        //Fuente
        private SpriteFont font;        

        public Alert(Rectangle rectangle, Texture2D texture, String text, String method, Object[] param) : base(rectangle, texture)
        {
            //fuente
            font = Fonts.arial_12;            

            //posicionamiento del alert.
            alertPos = new Vector2(
                PaintToWinUtils.centerArea(new Vector2(0, Globals.gameSize.X), new Vector2(0, rectangle.Width)),
                PaintToWinUtils.centerArea(new Vector2(0, Globals.gameSize.Y), new Vector2(0, rectangle.Height)));

            //texto y posicionamiento del texto del alert.
            this.text = text;
            textPos = new Vector2(
                PaintToWinUtils.centerTextX(new Vector2(alertPos.X, alertPos.X + rectangle.Width), Fonts.arial_12, text),
                PaintToWinUtils.centerTextY(new Vector2(alertPos.Y, alertPos.Y + rectangle.Height), Fonts.arial_12, text));

            //tamaño, posicionamiento y creacion del boton.
            buttonSize = new Vector2(150, 30);
            Vector2 buttonPos = new Vector2(
                PaintToWinUtils.centerArea(new Vector2(alertPos.X, alertPos.X + rectangle.Width), new Vector2(0, buttonSize.X)),
                rectangle.Height * 0.75f + alertPos.Y);
            button = new Button<T>(new Rectangle((int)buttonPos.X, (int)buttonPos.Y, (int)buttonSize.X, (int)buttonSize.Y), texture, Textures.hoverButton, method, param, "optionsscreen_accept", true);
        }

        public override void update()
        {
            button.update();
        }

        public override void draw()
        {
            //dibujado del alert
            Globals.spriteBatch.Draw(Textures.alert_background, new Rectangle((int)alertPos.X, (int)alertPos.Y, rectangle.Width, rectangle.Height), 
                Color.White);

            //dibujado del boton
            button.draw();

            //dibujado del texto del alert
            Globals.spriteBatch.DrawString(font, text, textPos, Color.Black);
        }
    }
}
