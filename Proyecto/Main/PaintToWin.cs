using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Proyecto
{
    public class PaintToWin : Game
    {
        private ScreenManager screenManager;
        private Options options;

        public PaintToWin()
        {
            Globals.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            //Carga los datos de un fichero
            options = new Options();
            options = options.load();

            //Mouse
            this.IsMouseVisible = true;

            //Aumentar/disminuir tamaño pantalla
            Window.AllowUserResizing = true;

            //Tamaño pantalla
            Globals.gameSize = new Vector2(options.resolution.X, options.resolution.Y);
            Globals.graphics.PreferredBackBufferWidth = (int)Globals.gameSize.X;
            Globals.graphics.PreferredBackBufferHeight = (int)Globals.gameSize.Y;

            //Comprueba la dificultad
            Globals.difficult = options.difficult;

            //Comprueba si tiene que arrancar en pantalla completa
            Globals.fullScreen = options.fullScreen;
            if (options.fullScreen) Globals.graphics.ToggleFullScreen();            

            Globals.graphics.ApplyChanges();

            //Idioma
            Globals.language = options.language;

            //Backbuffer
            Globals.backBuffer = new RenderTarget2D(Globals.graphics.GraphicsDevice, (int)Globals.gameSize.X, (int)Globals.gameSize.Y, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);

            /*//Código para desbloquear los FPS:
                Globals.graphics.SynchronizeWithVerticalRetrace = false;
                Globals.graphics.ApplyChanges();
                this.IsFixedTimeStep = false;
            */

            Mouse.WindowHandle = Window.Handle;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.content = this.Content;

            //Carga fuentes, texturas, videos y sonidos:
            Fonts.Load();
            Textures.Load();
            Videos.Load();

            //Pantalla por defecto
            screenManager = new ScreenManager();            
            
            //Pantalla inicial
            ScreenManager.AddScreen(new PreTitleScreen());
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Globals.windowFocused = this.IsActive;
            Globals.gameTime = gameTime;

            //Update screens
            screenManager.Update();

            //Input updates
            Input.update();
        }

        protected override void Draw(GameTime gameTime)
        {
            Globals.graphics.GraphicsDevice.SetRenderTarget(Globals.backBuffer);
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);

            //Dibuja contenido de screenManager
            screenManager.Draw();

            Globals.graphics.GraphicsDevice.SetRenderTarget(null);

            //Dibuja backbuffer en pantalla
            Globals.spriteBatch.Begin();

            Globals.spriteBatch.Draw(Globals.backBuffer, new Rectangle(0,0, Globals.graphics.GraphicsDevice.Viewport.Width, Globals.graphics.GraphicsDevice.Viewport.Height), Color.White);

            Globals.spriteBatch.End();
        }
    }
}
