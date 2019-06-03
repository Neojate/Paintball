using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Proyecto
{
    class PreTitleScreen : BaseScreen
    {
        //Video intro
        VideoPlayer videoPlayer;        

        public PreTitleScreen()
        {
            Name = "PreTitleScreen";
            State = ScreenState.Active;

            //Instancia y reproducción del video
            videoPlayer = new VideoPlayer();
            videoPlayer.Play(Videos.intro);
        }

        public override void HandleInput()
        {
            if (Input.keyPressed(Keys.Escape) || 
                Input.keyPressed(Keys.Enter) || 
                Input.keyPressed(Keys.Space) ||
                Input.mouseClickPressed(TypeButton.LEFT_BUTTON)) loadScreen();
        }

        public override void Update()
        {
            
        }

        public override void Draw()
        {
            Globals.spriteBatch.Begin();
                        
            Globals.spriteBatch.Draw(videoPlayer.GetTexture(), new Rectangle(0, 0, (int)Globals.gameSize.X, (int)Globals.gameSize.Y), Color.White);                                                
            
            Globals.spriteBatch.End();
        }

        /** Metodo que carga la pantalla principal */
        private void loadScreen()
        {
            videoPlayer.Stop();
            ScreenManager.AddScreen(new TitleScreen());
            ScreenManager.UnloadScreen(this.Name);
        }        
    }
}
