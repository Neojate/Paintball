using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Proyecto
{
    class StageScreen : BaseScreen
    {
        //Instacia de stageMap
        public static StageMap stageMap;

        //colisiones
        public static bool isActive; 

        public StageScreen()
        {
            Name = "StageScreen";
            State = ScreenState.Active;

            stageMap = new StageMap(new Vector2(22, 10));
            
            isActive = true;            
        }

        public override void HandleInput()
        {
            stageMap.handleInput();

            foreach (MapElement e in stageMap.getElements()) e.handleInput();
        }

        public override void Update()
        {
            stageMap.update();

            //Ordena los elementos de stageMap
            stageMap.getElements().Sort();

            foreach (MapElement e in stageMap.getElements()) e.update();
        }

        public override void Draw()
        {
            Globals.spriteBatch.Begin();

            //dibujar fondo negro
            Globals.spriteBatch.Draw(Textures.white, new Rectangle(0, 0, (int)Globals.gameSize.X, (int)Globals.gameSize.Y), Color.Black);

            //dibujar mapa
            stageMap.draw();

            //dibujar elementos
            foreach (MapElement e in stageMap.getElements()) e.draw();

            Globals.spriteBatch.End();
        }        
    }
}
