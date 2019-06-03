using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Proyecto
{

    public enum TypeButton
    {
        LEFT_BUTTON, RIGHT_BUTTON
    }

    public class Input
    {
        //Teclado
        public static KeyboardState currentKeyState;
        public static KeyboardState lastKeyState;

        //Mouse
        public static MouseState currentMouseState;
        public static MouseState lastMouseState;
        public static Vector2 mousePos = new Vector2();

        public static void update()
        {
            //Teclado
            lastKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();

            //Mouse
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            mousePos.X = currentMouseState.X;
            mousePos.Y = currentMouseState.Y;
        }

        /** Mantener el botón del ratón pulsado */
        public static Boolean mouseClickDown(TypeButton button)
        {
            switch(button)
            {
                case TypeButton.LEFT_BUTTON:
                    return currentMouseState.LeftButton == ButtonState.Pressed;
                case TypeButton.RIGHT_BUTTON:
                    return currentMouseState.RightButton == ButtonState.Pressed;
                default:
                    return false;
            }
        }

        /** Pulsar y soltar el botón del ratón */
        public static Boolean mouseClickPressed(TypeButton button)
        {
            switch(button)
            {
                case TypeButton.LEFT_BUTTON:
                    return mouseClickDown(button) && lastMouseState.LeftButton == ButtonState.Released;
                case TypeButton.RIGHT_BUTTON:
                    return mouseClickDown(button) && lastMouseState.RightButton == ButtonState.Released;
                default:
                    return false;
            }
        }

        /** Mantener una tecla pulsada */
        public static Boolean keyDown(Keys key)
        {
            return currentKeyState.IsKeyDown(key);
        }

        /** Pulsar y soltar una tecla */
        public static Boolean keyPressed(Keys key)
        {
            return currentKeyState.IsKeyDown(key) && lastKeyState.IsKeyUp(key);
        }
    }
}
