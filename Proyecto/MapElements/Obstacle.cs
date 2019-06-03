using Microsoft.Xna.Framework;

namespace Proyecto
{ 
    public enum ObstacleType
    {
        //obstáculos de juego
        WALL_TOP_LEFT, WALL_TOP_RIGHT,
        CILINDRE,
        WALL_RIGHT_TOP, WALL_RIGHT_BOT,
        WALL_LEFT_TOP, WALL_LEFT_BOT,
        GUSANO_TOP, GUSANO_MID, GUSANO_BOT,
        PYRAMID,

        //obstáculos de stage
        SHOP_LEFT_TOP, SHOP_MID_TOP, SHOP_RIGHT_TOP,
        SHOP_LEFT_BOT, SHOP_MID_BOT, SHOP_RIGHT_BOT,
        SCHOOL_LEFT_TOP, SCHOOL_MID_TOP, SCHOOL_RIGHT_TOP,
        SCHOOL_LEFT_BOT, SCHOOL_MID_BOT, SCHOOL_RIGHT_BOT,
        STAGE_WALL_TOP, STAGE_WALL_BOT,
        STAGE_CORNER_TOP_LEFT_TOP, STAGE_CORNER_TOP_LEFT_BOT, STAGE_WALL_LEFT,
        STAGE_CORNER_TOP_RIGHT_TOP, STAGE_CORNER_TOP_RIGHT_BOT, STAGE_WALL_RIGHT,
        STAGE_CORNER_BOT_RIGHT, STAGE_CORNER_BOT_LEFT, STAGE_BOT_MID,
        SHOOPKEEPER, SKILLKEEPER,
        DOOR_LEFT_TOP, DOOR_RIGHT_TOP, DOOR_LEFT_BOT, DOOR_RIGHT_BOT,
        BOARD_LEFT, BOARD_RIGHT,
        PLANT,
        TICKET_SELLER
    }

    class Obstacle : MapElement
    {
        //Tipo de obstáculo
        private ObstacleType obstacleType;

        public Obstacle(ObstacleType obstacleType, Camera camera, Vector2 originPos, Vector2 originOffset) : base(camera)
        {
            this.obstacleType = obstacleType;
            elementPos = originPos;
            elementOffset = originOffset;
            calcElementSize();

            //texturas
            texture = Textures.obstacle;
            slice = calculateSlice();
            color1D = getPalette1D(texture, slice);
        }

        public override void update()
        {
            //Update del obstáculo
            calculateScreenPos();
        }

        public override void draw()
        {
            int posX = (int)(elementScreenPos.X + elementOffset.X);
            int posY = (int)(elementScreenPos.Y + elementOffset.Y);

            //Dibujo del obstáculo
            Globals.spriteBatch.Draw(Textures.obstacle, new Rectangle(posX, posY, (int)elementSize.X, (int)elementSize.Y), slice, Color.White);
        }

        #region METODOS PRIVADOS

        /** Tamaño del obstáculo */
        private void calcElementSize()
        {
            switch (obstacleType)
            {
                case ObstacleType.WALL_TOP_LEFT:
                    elementSize = new Vector2(32, 32);
                    break;
                case ObstacleType.WALL_TOP_RIGHT:
                    elementSize = new Vector2(32, 32);
                    break;
                case ObstacleType.CILINDRE:
                    elementSize = new Vector2(32, 32);
                    break;
                case ObstacleType.WALL_RIGHT_TOP:
                    elementSize = new Vector2(32, 32);
                    break;                
            }
            elementSize = new Vector2(32, 32);
        }

        /** Posición del sprite dentro del sprite general */
        private Rectangle calculateSlice()
        {
            switch (obstacleType)
            {
                case ObstacleType.WALL_TOP_LEFT:
                    slice = new Rectangle(0, 0, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.WALL_TOP_RIGHT:
                    slice = new Rectangle(32, 0, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.CILINDRE:
                    slice = new Rectangle(0, 32, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.WALL_RIGHT_TOP:
                    slice = new Rectangle(0, 64, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.WALL_RIGHT_BOT:
                    slice = new Rectangle(0, 96, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.WALL_LEFT_TOP:
                    slice = new Rectangle(32, 64, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.WALL_LEFT_BOT:
                    slice = new Rectangle(32, 96, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.GUSANO_TOP:
                    slice = new Rectangle(96, 32, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.GUSANO_MID:
                    slice = new Rectangle(64, 32, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.GUSANO_BOT:
                    slice = new Rectangle(32, 32, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.PYRAMID:
                    slice = new Rectangle(64, 0, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.SHOP_LEFT_TOP:
                    slice = new Rectangle(64, 64, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.SHOP_MID_TOP:
                    slice = new Rectangle(96, 64, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.SHOP_RIGHT_TOP:
                    slice = new Rectangle(128, 64, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.SHOP_LEFT_BOT:
                    slice = new Rectangle(64, 96, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.SHOP_MID_BOT:
                    slice = new Rectangle(96, 96, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.SHOP_RIGHT_BOT:
                    slice = new Rectangle(128, 96, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.STAGE_WALL_TOP:
                    slice = new Rectangle(128, 0, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.STAGE_WALL_BOT:
                    slice = new Rectangle(128, 32, (int)elementSize.X, (int)elementSize.Y);
                    return slice;

                case ObstacleType.SHOOPKEEPER:
                    slice = new Rectangle(96, 0, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.SKILLKEEPER:
                    slice = new Rectangle(64, 160, (int)elementSize.X, (int)elementSize.Y);
                    return slice;

                case ObstacleType.DOOR_LEFT_TOP:
                    slice = new Rectangle(0, 128, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.DOOR_RIGHT_TOP:
                    slice = new Rectangle(32, 128, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.DOOR_LEFT_BOT:
                    slice = new Rectangle(0, 160, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.DOOR_RIGHT_BOT:
                    slice = new Rectangle(32, 160, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.BOARD_LEFT:
                    slice = new Rectangle(96, 128, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.BOARD_RIGHT:
                    slice = new Rectangle(128, 128, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.PLANT:
                    slice = new Rectangle(64, 128, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.TICKET_SELLER:
                    slice = new Rectangle(160, 160, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.STAGE_CORNER_TOP_LEFT_TOP:
                    slice = new Rectangle(160, 0, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.STAGE_CORNER_TOP_LEFT_BOT:
                    slice = new Rectangle(160, 32, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.STAGE_WALL_LEFT:
                    slice = new Rectangle(192, 0, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.STAGE_CORNER_TOP_RIGHT_TOP:
                    slice = new Rectangle(224, 0, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.STAGE_CORNER_TOP_RIGHT_BOT:
                    slice = new Rectangle(224, 32, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.STAGE_WALL_RIGHT:
                    slice = new Rectangle(192, 32, (int)elementSize.X, (int)elementSize.Y);
                    return slice;

                case ObstacleType.SCHOOL_LEFT_TOP:
                    slice = new Rectangle(160, 64, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.SCHOOL_MID_TOP:
                    slice = new Rectangle(192, 64, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.SCHOOL_RIGHT_TOP:
                    slice = new Rectangle(224, 64, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.SCHOOL_LEFT_BOT:
                    slice = new Rectangle(160, 96, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.SCHOOL_MID_BOT:
                    slice = new Rectangle(192, 96, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.SCHOOL_RIGHT_BOT:
                    slice = new Rectangle(224, 96, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.STAGE_CORNER_BOT_LEFT:
                    slice = new Rectangle(256, 0, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.STAGE_CORNER_BOT_RIGHT:
                    slice = new Rectangle(320, 0, (int)elementSize.X, (int)elementSize.Y);
                    return slice;
                case ObstacleType.STAGE_BOT_MID:
                    slice = new Rectangle(288, 0, (int)elementSize.X, (int)elementSize.Y);
                    return slice;

            }
            return new Rectangle(0, 0, 0, 0);
        }
        #endregion

    }
}
