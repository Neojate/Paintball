using Microsoft.Xna.Framework;
using System;

namespace Proyecto
{
    class Particle
    {
        public enum ParticleType { rain, snow}

        public abstract class BaseParticle
        {
            public ParticleType type;
            public Vector2 position;
            public Single speed;
            public Single direction;
            public Single rotation;
            public Single scaleX;
            public Single scaleY;
            public Single lifeTime;
            public Single lifeTick = 0;
            public static Random ran = new Random();
            public Color color;
        }

        //Lluvia
        public class Rain : BaseParticle
        {
            public Rain()
            {
                type = ParticleType.rain;
                color = Color.LightBlue;                
                position = new Vector2(ran.Next(-200, (int)Globals.gameSize.X), 0);
                direction = 290;
                scaleX = ran.Next(4, 15);
                scaleY = 1;
                lifeTime = ran.Next(10, 20) * scaleX;
                speed = scaleX;
                rotation = -MathHelper.ToRadians(direction);
            }
        }

        //Nieve
        public class Snow : BaseParticle
        {
            public Snow()
            {
                type = ParticleType.snow;
                color = Color.White;
                position = new Vector2(ran.Next(250, 300), ran.Next(100, 180));
                direction = ran.Next(-240, 300);
                scaleX = ran.Next(1, 4);
                scaleY = scaleX;
                lifeTime = ran.Next(1, 50) * (scaleX + scaleY);
                speed = 1;
                rotation = -MathHelper.ToRadians(direction);
            }
        }
    }
}
