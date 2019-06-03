using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Proyecto
{
    public class Map
    {
        //mapa
        private Tile[,] scenario;
        private Vector2 mapSize;

        //tamaño casilla
        private int tileSize = Tile.TILE_SIZE;

        //elementos del mapa
        private List<MapElement> elements;
        public List<MapElement> auxElements;
        public List<MapElement> deleteElements;  
        
        public int numberEnemies;      

        //player
        private Player player;

        //cámara del juego
        private Camera camera;

        public Map(Vector2 size, Vector2 startCoords)
        {
            this.mapSize = size;
            scenario = new Tile[(int)size.X, (int)size.Y];

            //mapa temporal, luego se cargará desde la base de datos
            loadTemporalMap();

            //elementos del mapa
            elements = new List<MapElement>();
            auxElements = new List<MapElement>();
            deleteElements = new List<MapElement>();

            //camara del juego
            camera = new Camera();

            //añadido el personaje
            player = new Player(camera, new Vector2(11, 29));
            player.loadPlayer();
            elements.Add(player);

            //obstáculos
            //obstacles = new List<MapElement>();
            insertTemporalObstacles();
            
            foreach (MapElement e in elements) if (e is Npc) numberEnemies++;
                        
        }

        public void load()
        {
            //todo: implementar tile bitmasking?
        }

        public void save()
        {

        }

        public void draw()
        {
            Vector2 cameraPos = player.getCamera().getCameraPos();
            Vector2 cameraOffset = player.getCamera().getCameraOffset();

            for (int drawX = -1; drawX < Globals.gameSize.X / tileSize; drawX++)
            {
                for (int drawY = -1; drawY < Globals.gameSize.Y / tileSize; drawY++)
                {
                    int x = (int)cameraPos.X + drawX;
                    int y = (int)cameraPos.Y + drawY;

                    if (x >= 0 && x <= Globals.gameSize.X && y >= 0 && y <= Globals.gameSize.Y && x < mapSize.X && y < mapSize.Y)
                    {
                        //dibujo de las celdas
                        Globals.spriteBatch.Draw(scenario[x, y].getTexture(), new Rectangle(drawX * tileSize + (int)cameraOffset.X, drawY * tileSize + (int)cameraOffset.Y, tileSize, tileSize), scenario[x,y].calculateSlice(), Color.White);

                        //dibujo de las coordenadas
                        //Globals.spriteBatch.DrawString(Fonts.arial_12, x + "\n" + y, new Vector2(drawX * tileSize + (int)cameraOffset.X, drawY * tileSize + (int)cameraOffset.Y), Color.Green);
                    }
                }
            }
        }

        /** Metodo para añadir elementos al mapa */
        /** element = elemento que queremos añadir */
        public void addElement(MapElement element)
        {
            auxElements.Add(element);
        }

        /** Metodo para eliminar elementos del mapa */
        /** element = elemento que queremos eliminar */
        public void deleteElement(MapElement element)
        {
            deleteElements.Add(element); 
        }

        /** Carga el mapa */
        private void loadTemporalMap()
        {
            for (int i = 0; i < mapSize.X; i++)
            {
                for (int j = 0; j < mapSize.Y; j++)
                {
                    scenario[i, j] = new Tile(TileType.GRASS);
                }
            }
        }

        /** Carga los obstáculos */
        private void insertTemporalObstacles()
        {
            //Verjas
            for (int i = 0; i < 24; i++)
            {
                if (i % 2 == 0)
                {
                    elements.Add(new Obstacle(ObstacleType.WALL_TOP_LEFT, camera, new Vector2(i, 0), Vector2.Zero));
                    elements.Add(new Obstacle(ObstacleType.WALL_TOP_LEFT, camera, new Vector2(i, 31), Vector2.Zero));
                }
                else
                {
                    elements.Add(new Obstacle(ObstacleType.WALL_TOP_RIGHT, camera, new Vector2(i, 0), Vector2.Zero));
                    elements.Add(new Obstacle(ObstacleType.WALL_TOP_RIGHT, camera, new Vector2(i, 31), Vector2.Zero));
                }
            }

            for (int i = 1; i < 32; i++)
            {
                if (i % 2 == 0)
                {
                    elements.Add(new Obstacle(ObstacleType.WALL_LEFT_TOP, camera, new Vector2(0, i), Vector2.Zero));
                    elements.Add(new Obstacle(ObstacleType.WALL_RIGHT_TOP, camera, new Vector2(23, i), Vector2.Zero));
                }
                else
                {
                    elements.Add(new Obstacle(ObstacleType.WALL_LEFT_BOT, camera, new Vector2(0, i), Vector2.Zero));
                    elements.Add(new Obstacle(ObstacleType.WALL_RIGHT_BOT, camera, new Vector2(23, i), Vector2.Zero));
                }
            }

            //campo B
            elements.Add(new Obstacle(ObstacleType.CILINDRE, camera, new Vector2(7, 26), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.CILINDRE, camera, new Vector2(15, 26), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.CILINDRE, camera, new Vector2(7, 21), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.CILINDRE, camera, new Vector2(15, 21), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.PYRAMID, camera, new Vector2(11, 17), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.PYRAMID, camera, new Vector2(3, 17), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.PYRAMID, camera, new Vector2(3, 23), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.CILINDRE, camera, new Vector2(11, 23), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.PYRAMID, camera, new Vector2(19, 23), Vector2.Zero));

            elements.Add(new Obstacle(ObstacleType.CILINDRE, camera, new Vector2(8, 14), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.CILINDRE, camera, new Vector2(14, 14), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.CILINDRE, camera, new Vector2(20, 15), Vector2.Zero));

            //campo A
            elements.Add(new Obstacle(ObstacleType.CILINDRE, camera, new Vector2(7, 4), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.CILINDRE, camera, new Vector2(15, 4), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.CILINDRE, camera, new Vector2(7, 9), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.CILINDRE, camera, new Vector2(15, 9), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.PYRAMID, camera, new Vector2(11, 12), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.PYRAMID, camera, new Vector2(3, 12), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.PYRAMID, camera, new Vector2(3, 6), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.CILINDRE, camera, new Vector2(11, 6), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.PYRAMID, camera, new Vector2(19, 6), Vector2.Zero));


            //gusano
            elements.Add(new Obstacle(ObstacleType.GUSANO_TOP, camera, new Vector2(18, 12), Vector2.Zero));
            for (int i = 13; i < 18; i++) elements.Add(new Obstacle(ObstacleType.GUSANO_MID, camera, new Vector2(18, i), Vector2.Zero));
            elements.Add(new Obstacle(ObstacleType.GUSANO_BOT, camera, new Vector2(18, 18), Vector2.Zero));

            //enemigos
            elements.Add(new Npc(camera, new Vector2(12, 1), Vector2.Zero, TeamType.ENEMY, IAType.SCOUT_LEFT, player));
            //elements.Add(new Npc(camera, new Vector2(13, 1), Vector2.Zero, TeamType.ENEMY, IAType.COVER, player));
            //elements.Add(new Npc(camera, new Vector2(14, 1), Vector2.Zero, TeamType.ENEMY, IAType.SCOUT_RIGHT, player));
            //elements.Add(new Npc(camera, new Vector2(20, 25), Vector2.Zero, TeamType.ENEMY, player));
            //elements.Add(new Npc(camera, new Vector2(10, 20), Vector2.Zero, TeamType.ENEMY, player));
        }

        #region GETTERS Y SETTERS

        public List<MapElement> getElements() { return elements; }
        public void setElements(List<MapElement> elements) { this.elements = elements; }

        public Player getPlayer() { return player; }
        public void setPlayer(Player player) { this.player = player; }

        #endregion
    }
}
