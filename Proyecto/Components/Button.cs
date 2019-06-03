using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection;

namespace Proyecto
{
    public class Button<T> : Component
    {
        private bool hover;
        private MethodInfo content;
        private Object[] param;
        private String text;
        private Vector2 textPos;
        private SpriteFont font;
        private Boolean borderButton;
        private Texture2D hoverTexture;

        public Button(Rectangle rectangle, Texture2D texture, Texture2D hoverTexture, String method, Object[] param, String codeText, Boolean border) : base(rectangle, texture)
        {
            content = typeof(T).GetMethod(method);
            this.param = param;
            hover = false;
            font = Fonts.arial_12;
                        
            if (codeText != null)
            {
                this.text = new Language().getMessage(codeText);
                textPos = new Vector2(
                    PaintToWinUtils.centerTextX(new Vector2(rectangle.X, rectangle.X + rectangle.Width), font, text),
                    PaintToWinUtils.centerTextY(new Vector2(rectangle.Y, rectangle.Y + rectangle.Height), font, text));
            }

            borderButton = border;
            this.hoverTexture = hoverTexture;
        }

        public override void update()
        {
            if (!visibility || !enabled) return;
            hover = false;
            if (rectangle.Contains(new Point((int)Input.mousePos.X, (int)Input.mousePos.Y)))
            {
                hover = true;
                if (Input.mouseClickPressed(TypeButton.LEFT_BUTTON)) content.Invoke(null, param);
            }
        }

        public override void draw()
        {
            if (!visibility) return;

            //Dibujado del botón
            if(borderButton) Globals.spriteBatch.Draw(Textures.white, rectangle, Color.White);
            Globals.spriteBatch.Draw(texture, new Rectangle(rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 2, rectangle.Height - 2), Color.White);
           
            if (hover) Globals.spriteBatch.Draw(hoverTexture, new Rectangle(rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 2, rectangle.Height - 2), Color.White);            

            //Dibujado texto botón
            if (text != null) Globals.spriteBatch.DrawString(font, text, textPos, Color.White);

        }

        #region GETTERS Y SETTERS

        public bool isHover() { return hover; }

        #endregion
    }
}
