using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto
{
    class WeatherScreen : BaseScreen
    {
        //Lluvia
        private bool rain;
        private int rainTransition;

        //Relámpago
        private bool flash;
        private int flashTick;
        private int lightningTimer;

        //Partículas
        private int particlesPerTick;
        private List<Particle.BaseParticle> particleList;
        
        //Random        
        private Random ran = new Random();

        public WeatherScreen()
        {
            Name = "WeatherScreen";
            State = ScreenState.Active;

            //Lluvia
            rain = true;
            rainTransition = 0;

            //Relámpago
            flash = false;
            flashTick = 0;
            lightningTimer = 0;

            //Partículas
            particlesPerTick = 10;
            particleList = new List<Particle.BaseParticle>();
        }

        public override void Update()
        {
            //Lluvia
            if (rain)
            {
                lightningTimer += 1;
                if (rainTransition < 20)
                {
                    rainTransition += 1;
                }
                else
                {
                    for (int x = 0; x < particlesPerTick; x++)
                    {
                        particleList.Add(new Particle.Rain());
                    }
                    if (flash)
                    {
                        flashTick += 1;
                        if (flashTick > 4)
                        {
                            flashTick = 0;
                            flash = false;

                            //Sonido relámpagos
                            if (ran.Next(0, 100) < 50)
                            {
                                //thunderSound.Play(ran.Next(10, 30) / 100, 0, 0);
                            }
                            else
                            {
                                //thunderSound2.Play(ran.Next(10, 30) / 100, 0, 0);
                            }
                        }
                    }
                    else if (ran.Next(5000) < lightningTimer / 60)
                    {
                        lightningTimer = 0;
                        flash = true;
                    }
                }
            }
            else
            {
                if (rainTransition > 0)
                {
                    rainTransition -= 1;
                }
                if (flash)
                {
                    flash = false;
                }
            }

            //Eliminar partículas
            List<Particle.BaseParticle> removeParticles = new List<Particle.BaseParticle>();

            foreach (Particle.BaseParticle p in particleList)
            {
                p.lifeTick += 1;

                if (p.lifeTick > p.lifeTime)
                {
                    removeParticles.Add(p);
                }

                p.position.X += (float)Math.Cos(MathHelper.ToRadians(p.direction)) * p.speed;
                p.position.Y += (float)-Math.Sin(MathHelper.ToRadians(p.direction)) * p.speed;
            }
            foreach (Particle.BaseParticle p in removeParticles)
            {
                particleList.Remove(p);
            }
        }

        public override void Draw()
        {
            Globals.spriteBatch.Begin();                        

            //Lluvia
            for (int x = particleList.Count - 1; x >= 0; x--)
            {
                Particle.BaseParticle p = particleList[x];
                Globals.spriteBatch.Draw(Textures.white, new Rectangle((int)p.position.X, (int)p.position.Y, (int)p.scaleX, (int)p.scaleY), new Rectangle(0, 0, Textures.white.Width, Textures.white.Height), p.color * MathHelper.Clamp((p.lifeTime / p.lifeTick - 1), 0, 1), p.rotation, new Vector2(MathHelper.ToRadians(p.direction), 0), SpriteEffects.None, 0);
            }

            //Relámpago
            if (flash)
            {
                Globals.spriteBatch.Draw(Textures.white, new Rectangle(0, 0, (int)Globals.gameSize.X, (int)Globals.gameSize.Y), Color.White * (float)0.5);
            }            

            Globals.spriteBatch.End();
        }
    }
}
