using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Proyecto
{
    class buyAlert<T> : Component
    {
        //posición del alert
        private Vector2 alertPos;

        //Equipo
        BaseEquip equip;

        //Botón
        private Vector2 buttonSize;
        private Button<T> acceptButton;
        private Button<T> cancelButton;

        //Fuente
        private SpriteFont font;

        //Language
        Language language;

        //Filas y columnas
        private float[] columns;
        private float[] rows;

        public buyAlert(Rectangle rectangle, Texture2D texture, BaseEquip equip, String method, Object[] param) : base(rectangle, texture)
        {
            //fuente
            font = Fonts.arial_12;

            //Language
            language = new Language();

            //Equipo
            this.equip = equip;

            //Filas y columnas
            columns = new float[] { Globals.gameSize.X * 0.31f, Globals.gameSize.X * 0.62f };
            rows = new float[] { Globals.gameSize.Y * 0.35f, Globals.gameSize.Y * 0.4f, Globals.gameSize.Y * 0.7f };

            //posicionamiento del alert.
            alertPos = new Vector2(
                PaintToWinUtils.centerArea(new Vector2(rectangle.X, Globals.gameSize.X), new Vector2(0, rectangle.Width)),
                PaintToWinUtils.centerArea(new Vector2(rectangle.Y, Globals.gameSize.Y), new Vector2(0, rectangle.Height)));

            //tamaño, posicionamiento y creación del boton.
            buttonSize = new Vector2(150, 30);
            Vector2 buttonPos = new Vector2(
                PaintToWinUtils.centerArea(new Vector2(alertPos.X, alertPos.X + rectangle.Width), new Vector2(0, buttonSize.X)),
                rectangle.Height * 0.75f + alertPos.Y);
            acceptButton = new Button<T>(new Rectangle((int)(buttonPos.X - Globals.gameSize.X * 0.1f), (int)buttonPos.Y, (int)buttonSize.X, (int)buttonSize.Y), texture, Textures.hoverButton, method, param, "gameMenuMarket_buy", true);
            cancelButton = new Button<T>(new Rectangle((int)(buttonPos.X + Globals.gameSize.X * 0.1f), (int)buttonPos.Y, (int)buttonSize.X, (int)buttonSize.Y), texture, Textures.hoverButton, method, param, "optionsscreen_exit", true);
        }

        public override void update()
        {
            acceptButton.update();
            cancelButton.update();
        }

        public override void draw()
        {
            //dibujado del alert
            Globals.spriteBatch.Draw(Textures.white, new Rectangle((int)alertPos.X, (int)alertPos.Y, rectangle.Width, rectangle.Height),
                Color.White);

            //dibujado del boton
            acceptButton.draw();
            cancelButton.draw();

            //dibujado del nombre
            Globals.spriteBatch.DrawString(font, language.getMessage("gameMenuMarket_model") + equip.getName(), new Vector2(columns[0], rows[0]), Color.Black);

            //dibujado del precio
            Globals.spriteBatch.DrawString(font, equip.getDescription(), new Vector2(columns[0], rows[1]), Color.Black);

            //dibujado de la descripción
            Globals.spriteBatch.DrawString(font, language.getMessage("gameMenuMarket_price") + equip.getPrice(), new Vector2(columns[1], rows[0]), Color.Black);
        }
    }
}

