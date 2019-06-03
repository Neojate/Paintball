using Microsoft.Xna.Framework.Graphics;
using System;

namespace Proyecto
{
    public class Pod : BaseEquip
    {
        //Cantidad de munición del cargador
        private int capacity;

        public Pod(String name, String description, Texture2D image, String price, int capacity)
        {
            this.capacity = capacity;
        }

        #region GETTERS Y SETTERS

        public int getCapacity() { return capacity; }

        #endregion
    }
}
