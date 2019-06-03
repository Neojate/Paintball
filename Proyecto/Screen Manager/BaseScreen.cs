using System;
using System.Collections.Generic;

namespace Proyecto
{
    public abstract class BaseScreen
    {
        public String Name = "";
        public ScreenState State = ScreenState.Active;

        public Single Position = 0;

        //Pantalla activa
        public Boolean Focused = false;
        public Boolean GrabFocus = true;

        //Components
        protected List<Component> components;

        //Idioma
        protected Language language;

        public BaseScreen()
        {
            //Instancia language
            language = new Language();

            //Instancia components
            components = new List<Component>();
        }

        public virtual void HandleInput()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {

        }

        public virtual void Unload()
        {
            State = ScreenState.ShutDown;
        }
    }
}
