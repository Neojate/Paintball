using Microsoft.Xna.Framework.Graphics;
using System;

namespace Proyecto
{
    public class BaseEquip
    {
        //Idioma
        protected Language language;

        //atributos
        protected String name;                      //nombre
        protected String description;               //descripcion
        protected String price;                     //precio
        protected Texture2D texture;                //textura

        #region GETTERS

        public String getName() { return name; }

        public String getDescription() { return description; }

        public String getPrice() { return price; }

        public Texture2D getTexture() { return texture; }

        #endregion
    }
}
