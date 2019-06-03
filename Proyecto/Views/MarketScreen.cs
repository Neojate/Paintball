using Microsoft.Xna.Framework;
using System;
using System.Text.RegularExpressions;

namespace Proyecto
{
    //Pestañas del menú
    public enum TabType
    {
        WEAPONS, ARMOR
    }

    class MarketScreen : BaseScreen
    {
        //Marcadoras
        private Marker[] markers;

        //Botón
        private Vector2 tabBtnSize;
        private Vector2 buttonSize;

        //Filas y columnas
        private float[] columns;
        private float[] rows;

        //Alert
        private static BaseEquip baseEquip;

        //Player
        private static Player player;

        //Fisicas
        private static bool haveChange;

        //Alerta
        private static Boolean showAlert = false;
        private static String text;

        public MarketScreen(Player p)
        {
            Name = "MarketScreen";
            State = ScreenState.Active;           

            //Player
            player = p;

            //Filas y columnas
            columns = new float[] { Globals.gameSize.X * 0.1f, Globals.gameSize.X * 0.352f, Globals.gameSize.X * 0.4f, Globals.gameSize.X * 0.65f, Globals.gameSize.X * 0.7f };
            rows = new float[] { Globals.gameSize.Y * 0.1f, Globals.gameSize.Y * 0.18f, Globals.gameSize.Y * 0.26f, Globals.gameSize.Y * 0.34f, Globals.gameSize.Y * 0.42f, Globals.gameSize.Y * 0.5f, Globals.gameSize.Y * 0.7f };

            //Marcadoras
            getMarkers();

            //Botón
            tabBtnSize = new Vector2(Globals.gameSize.X * 0.25f, Globals.gameSize.Y * 0.08f);
            buttonSize = new Vector2(Globals.gameSize.X * 0.16f, Globals.gameSize.Y * 0.08f);            

            //Equipo
            baseEquip = markers[0];

            //Creación de la interfaz
            createInterface();

            //Fisicas
            haveChange = true;
        }

        public override void Update()
        {
            foreach (Component c in components) c.update();
            updateComponents();

            //Mensaje de alerta
            if (showAlert)
            {                
                foreach (Component c in components) c.setEnabled(false);
                components.Add(new Alert<MarketScreen>(new Rectangle(0, 0, 400, 200), Textures.background_menu, language.getMessage(text), "exit", null));
                showAlert = false;
            }
        }

        public override void Draw()
        {
            Globals.spriteBatch.Begin();

            //dibujado de la interfaz
            foreach (Component c in components) c.draw();

            Globals.spriteBatch.Draw(baseEquip.getTexture(), new Rectangle((int)columns[1], (int)rows[0], 200, 200), new Rectangle(0, 0, 32, 32), Color.White);
            Globals.spriteBatch.DrawString(Fonts.arial_14, language.getMessage("market_price") + baseEquip.getPrice() + " pln", new Vector2(columns[3], rows[1]), Color.White);
            Globals.spriteBatch.DrawString(Fonts.arial_12, spliceText(baseEquip.getDescription()), new Vector2(columns[2], rows[4]), Color.White);

            foreach (Component c in components) if (c is Alert<MarketScreen>) c.draw();

            Globals.spriteBatch.End();
        }

        #region LISTENERS

        /** Actualiza la información de la marcadora actual */
        /** equip = marcadora actual */
        public static void weaponInfo(BaseEquip equip)
        {
            haveChange = true;
            baseEquip = equip;
        }

        /** Confirmar la compra de la marcadora seleccionada */
        public static void acceptBuy()
        {
            int price = int.Parse(baseEquip.getPrice());

            if (player.getMoney() >= price)
            {
                player.getBuyedMarkers().Add((Marker)baseEquip);
                text = "market_buyed";
                player.setMoney(player.getMoney() - price);
                acceptEquip();
            }
            else
            {
                text = "market_money";
            }            
            showAlert = true;
        }

        /** Equipa la marcadora seleccionada */
        public static void acceptEquip()
        {            
            player.setMarker((Marker)baseEquip);
            text = "market_equiped";
            showAlert = true;
        }

        /** Sale de la pantalla de compra */
        public static void exit()
        {
            ScreenManager.UnloadScreen("MarketScreen"); 
        }

        /** Sale del alert */
        public static void exitAlert()
        {
            showAlert = false;
            exit();
        }

        #endregion

        #region METODOS PRIVADOS

        /** Dibujado de la interfaz */
        private void createInterface()
        {
            String[] menuCode = new string[] { "spyderVictor_name", "tippmann98_name", "tippmannA5_name", "empireAxe_name", "eclipseGeo_name", "rap4_name", "rap4_name" };

            //Panel
            components.Add(new Panel(new Rectangle((int)columns[0], (int)rows[0], (int)(Globals.gameSize.X * 0.8f), (int)(Globals.gameSize.Y * 0.8f)), Textures.marketShop));
            components.Add(new Panel(new Rectangle((int)columns[1], (int)rows[0] + 2, (int)(Globals.gameSize.X * 0.5475f), (int)(Globals.gameSize.Y * 0.8f - 4)), Textures.background_menu));
            components.Add(new Button<MarketScreen>(new Rectangle((int)columns[3], (int)rows[6], (int)buttonSize.X, (int)buttonSize.Y), Textures.background_menu, Textures.hoverButton, "exit", null, "optionsscreen_exit", true));

            for (int i = 0; i < markers.Length; i++)
            {                
                components.Add(new Button<MarketScreen>(new Rectangle((int)columns[0] + 1, (int)rows[i] + 1, (int)tabBtnSize.X, (int)tabBtnSize.Y), Textures.background_menu, Textures.hoverButton, "weaponInfo", new Object[] { markers[i] }, menuCode[i], true));                
            }            
        }

        /** Carga todas las marcadoras disponibles para comprar/equipar */
        private void getMarkers()
        {
            markers = new Marker[] {
                new Marker(MarkerModel.SPYDER_VICTOR),
                new Marker(MarkerModel.TIPPMANN_98),
                new Marker(MarkerModel.TIPPMANN_A5),
                new Marker(MarkerModel.EMPIRE_AXE),
                new Marker(MarkerModel.ECLIPSE_CSR),
                new Marker(MarkerModel.RAP4_T68),
                new Marker(MarkerModel.RAP4_T68)
            };
        }

        /** No se utiliza */
        /** Para cambiar entre las diferentes pestañas de la tienda */
        /** tab = pestaña seleccionada */
        private static void selectTab(TabType tab)
        {
            switch (tab)
            {
                case TabType.WEAPONS:

                    break;
                case TabType.ARMOR:
                    break;
            }
        }
        
        /** Comprueba si ya tenemos comprada la marcadora actual */
        private Boolean isWeaponBuyed()
        {
            foreach(Marker marker in player.getBuyedMarkers())
            {
                if (marker.getName().Equals(baseEquip.getName())) return true;
            }
            return false;
        }

        /** Controla el update de los botones comprar/equipar para que funcione correctamente el hover */
        private void updateComponents()
        {
            if (!haveChange) return;
            haveChange = false;
            Button<MarketScreen> cmd;
            if (isWeaponBuyed())
            {
                cmd = new Button<MarketScreen>(new Rectangle((int)columns[2], (int)rows[6], (int)buttonSize.X, (int)buttonSize.Y), Textures.background_menu, Textures.hoverButton, "acceptEquip", null, "market_equip", true);
            }
            else
            {
                cmd = new Button<MarketScreen>(new Rectangle((int)columns[2], (int)rows[6], (int)buttonSize.X, (int)buttonSize.Y), Textures.background_menu, Textures.hoverButton, "acceptBuy", null, "market_buy", true);
            }
            components[components.Count - 1] = cmd;            
        }

        /** Hace un salto de línia cada X longitud de texto */
        /** text = texto al que queremos hacer un salto de línia cada X longitud */        
        private static string spliceText(string text)
        {
            int lineLength = (Globals.gameSize.X == 800) ? 50 : (Globals.gameSize.X == 1024) ? 54 : (Globals.gameSize.X == 1200) ? 60 : 69;
            return Regex.Replace(text, "(.{" + lineLength + "})", "$1" + Environment.NewLine);
        }

        #endregion
    }
}
