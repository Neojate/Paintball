using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Proyecto
{
    public class Component
    {
        protected Rectangle rectangle;
        protected Texture2D texture;
        protected Boolean visibility;
        protected Boolean enabled;
        
        public Component(Rectangle rectangle, Texture2D texture)
        {
            this.rectangle = rectangle;
            this.texture = texture;
            visibility = true;
            enabled = true;
        }

        public virtual void update()
        {

        }

        public virtual void draw()
        {

        }

        #region GETTERS Y SETTERS

        public Boolean getVisibility() { return this.visibility; }
        public void setVisibility(Boolean visibility) { this.visibility = visibility; }

        public Boolean getEnabled() { return this.enabled; }
        public void setEnabled(Boolean enabled) { this.enabled = enabled; }

        #endregion
    }
}
