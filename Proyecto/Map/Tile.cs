using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Proyecto
{
    public enum TileType
    {
        //todo: meter los distintos tipos de suelo
        GRASS,
        DUST,
        FLOOR
    }

    public class Tile
    {
        public const int TILE_SIZE = 32;

        private TileType terrainType;
        private Texture2D texture;
        private Vector2 position;
        private int strategic;
        private Boolean blocked;
        private Trigger trigger;
        private Rectangle slice;

        public Tile(TileType terrainType)
        {
            this.terrainType = terrainType;
            texture = Textures.grassMap;
            slice = calculateSlice();
        }

        public Tile(TileType terrainType, Texture2D texture, Vector2 position, int strategic, Boolean blocked)
        {
            this.terrainType = terrainType;
            this.texture = texture;
            this.position = position;
            this.strategic = strategic;
            this.blocked = blocked;
        }

        public Rectangle calculateSlice()
        {
            //TODO: que aqui se consigan las texturas del terreno
            Rectangle rectangle = new Rectangle(0, 0, 32, 32);
            switch(terrainType)
            {
                case TileType.GRASS:
                    rectangle = new Rectangle(0, 0, TILE_SIZE, TILE_SIZE);
                    break;
                case TileType.DUST:
                    rectangle = new Rectangle(0, 0, TILE_SIZE, TILE_SIZE);
                    break;
                case TileType.FLOOR:
                    rectangle = new Rectangle(TILE_SIZE, 0, TILE_SIZE, TILE_SIZE);
                    break;
            }
            return rectangle;
        }

        #region GETTERS Y SETTERS

        public TileType getTerrainType() { return terrainType; }
        public void setTerrainType(TileType terrainType) { this.terrainType = terrainType; }

        public Texture2D getTexture() { return texture; }
        public void setTexture(Texture2D texture) { this.texture = texture; }

        public Vector2 getPosition() { return position; }
        public void setPosition(Vector2 position) { this.position = position; }

        public int getStrategic() { return strategic; }
        public void setStrategic(int strategic) { this.strategic = strategic; }

        public Boolean getBlocked() { return blocked; }
        public void setBlocked(Boolean blocked) { this.blocked = blocked; }

        public Trigger getTrigger() { return trigger; }
        public void setTrigger(Trigger trigger) { this.trigger = trigger; }

        public Rectangle getSlice() { return slice; }

        #endregion

    }
}
