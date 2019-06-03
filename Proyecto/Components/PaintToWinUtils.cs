using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Proyecto
{
    class PaintToWinUtils
    {
        /** Método que calcula la posición central de un texto en el eje X */
        /** axisX = contenedor donde irá el texto */
        /** font = fuente del texto */
        /** text = texto que queremos centrar */
        public static int centerTextX(Vector2 axisX, SpriteFont font, String text) 
        {
            return (int)((axisX.Y - axisX.X) / 2 - (font.MeasureString(text).X / 2) + axisX.X);
        }

        /** Método que calcula la posición central de un texto en el eje Y */
        /** axisY = contenedor donde irá el texto */
        /** font = fuente del texto */
        /** text = texto que queremos centrar */
        public static int centerTextY(Vector2 axisY, SpriteFont font, String text)
        {
            return (int)((axisY.Y - axisY.X) / 2 - (font.MeasureString(text).Y / 2) + axisY.X);
        }

        /** Método que calcula la posición central del área */
        /** container = contenedor donde irá el objeto contenido */
        /** content = objecto a centrar dentro del contenedor */
        public static int centerArea(Vector2 container, Vector2 content)
        {
            return (int)(((container.Y - container.X) / 2 - (content.Y - content.X) / 2) + container.X);
        }

        /** Método que calcula la hypotenusa dados dos vectores */
        /** x = vector del eje X */
        /** y = vector del eje Y */
        public static double calcHypot(Vector2 x, Vector2 y)
        {
            float distanceX = x.Y - x.X;
            float distanceY = y.Y - y.X;
            return Math.Sqrt(Math.Pow(distanceX, 2) + Math.Pow(distanceY, 2));
        }

        /** Método que crea un número random */
        /** min = número random mínimo */
        /** max = número random máximo */
        public static int generateRandomNumber(int min, int max)
        {
            return new Random().Next(min, max);
        }

        
    }
}
