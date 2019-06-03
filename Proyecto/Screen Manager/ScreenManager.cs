using System;
using System.Collections.Generic;

namespace Proyecto
{
    public enum ScreenState
    {
        Active,
        ShutDown,
        Hidden
    }

    class ScreenManager
    {
        //Lista de pantallas
        private static List<BaseScreen> screens;
        private static List<BaseScreen> newScreens;

        //Debug
        private Debug debugScreen;

        public ScreenManager()
        {
            //Instancia lista de pantallas
            screens = new List<BaseScreen>();
            newScreens = new List<BaseScreen>();

            //Instancia debug
            debugScreen = new Debug();

            //Añade debugScreen
            AddScreen(debugScreen);
        }

        public void Update()
        {
            debugScreen.screens = "Screens: ";

            //Lista de pantallas para eliminar
            List<BaseScreen> removeScreens = new List<BaseScreen>();

            foreach (BaseScreen foundScreen in screens)
            {
                //Añadimos a la lista de pantallas a eliminar todas las pantallas que no esten activas
                if (foundScreen.State.Equals(ScreenState.ShutDown))
                {
                    removeScreens.Add(foundScreen);
                }
                else
                {
                    debugScreen.screens += foundScreen.Name + ", ";
                    foundScreen.Focused = false;
                }
            }

            //Eliminamos las pantallas que no están activas
            foreach (BaseScreen foundScreen in removeScreens)
            {
                screens.Remove(foundScreen);
            }

            //Añadir pantallas
            foreach (BaseScreen foundScreen in newScreens)
            {
                screens.Add(foundScreen);
            }
            newScreens.Clear();

            //Volvemos a poner debugScreen al principio de la lista
            screens.Remove(debugScreen);
            screens.Add(debugScreen);

            //Pantallas activas
            if (screens.Count > 0)
            {
                for (int i = screens.Count - 1; i >= 0; i--)
                {
                    if (screens[i].GrabFocus)
                    {
                        screens[i].Focused = true;
                        debugScreen.focusScreen = "Focused Screen: " + screens[i].Name;
                    }
                }
            }

            //Handle input de las pantallas activas
            foreach (BaseScreen foundScreen in screens)
            {
                if (Globals.windowFocused)
                {
                    foundScreen.HandleInput();
                }
                foundScreen.Update();
            }
        }

        public void Draw()
        {
            foreach (BaseScreen foundScreen in screens)
            {
                if (foundScreen.State.Equals(ScreenState.Active))
                {
                    foundScreen.Draw();
                }
            }
        }

        /** Añade una nueva pantalla */
        public static void AddScreen(BaseScreen screen)
        {
            newScreens.Add(screen);
        }

        /** Elimina una pantalla */
        /** nameScreen = pantalla que queremos eliminar */
        public static void UnloadScreen(String nameScreen)
        {
            foreach (BaseScreen foundScreen in screens)
            {
                if (foundScreen.Name.Equals(nameScreen))
                {
                    foundScreen.Unload();
                }
            }
        }

        /** Devuelve una lista con las Screens activas */
        public static List<BaseScreen> activeScreens()
        {
            return screens;
        }
    }
}

