using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto
{
    class TicketScreen : BaseScreen
    {
        //Constantes
        private const int TICKET_PRICE = 100;

        //Botón
        private Vector2 buttonSize;

        //Filas y columnas
        private float[] columns;
        private float[] rows;               

        //Player
        private static Player player;

        //Entrada
        private static int price;

        //Texto
        private SpriteFont font;
        private String text;
        private Vector2 textPos;

        public TicketScreen(Player p)
        {
            Name = "TicketScreen";
            State = ScreenState.Active;

            //Botón
            buttonSize = new Vector2(Globals.gameSize.X * 0.12f, Globals.gameSize.Y * 0.06f);

            //Filas y columnas
            columns = new float[] { Globals.gameSize.X * 0.3f, Globals.gameSize.X * 0.4f, Globals.gameSize.X * 0.7f - buttonSize.X - 20 };
            rows = new float[] { Globals.gameSize.Y * 0.3f, Globals.gameSize.Y * 0.4f, Globals.gameSize.Y * 0.45f, Globals.gameSize.Y * 0.6f };

            //Player
            player = p;

            //Entrada
            price = p.getLvlGame() * TICKET_PRICE;

            //Texto
            font = Fonts.arial_12;
            text = !player.getTicket() ? language.getMessage("ticket_price") + price : language.getMessage("ticket_buyed");            

            //Creación de la interfaz
            createInterface();
        }
        
        public override void Update()
        {
            foreach (Component c in components) c.update();
        }

        public override void Draw()
        {
            //Posición texto
            textPos = new Vector2(
                    PaintToWinUtils.centerTextX(new Vector2(columns[0], columns[0] + columns[1]), font, text),
                    PaintToWinUtils.centerTextY(new Vector2(rows[0], rows[0] + rows[1]), font, text));

            Globals.spriteBatch.Begin();            

            //Dibuja todos los componentes
            foreach (Component c in components) c.draw();

            //Texto
            Globals.spriteBatch.DrawString(Fonts.arial_12, text, textPos, Color.Black);

            Globals.spriteBatch.End();
        }

        #region LISTENERS

        /** Comprar entrada */
        public static void acceptBuy()
        {
            if(player.getMoney() >= price && !player.getTicket())
            {
                player.setMoney(player.getMoney() - price);
                player.setTicket(true);
                exit();
            }
        }

        /** Salir de TicketScreen y cancelar la compra */
        public static void exit()
        {
            ScreenManager.UnloadScreen("TicketScreen");
        }

        #endregion

        #region METODOS PRIVADOS

        /** Crear interfaz */
        private void createInterface()
        {
            //Panel 
            components.Add(new Panel(new Rectangle((int)columns[0], (int)rows[0], (int)columns[1], (int)rows[1]), Textures.alert_background));

            //Botón comprar y salir
            components.Add(new Button<TicketScreen>(new Rectangle((int)columns[0] + 20, (int)(rows[3]), (int)buttonSize.X, (int)buttonSize.Y), Textures.background_menu, Textures.hoverButton, "acceptBuy", null, "market_buy", true));
            components.Add(new Button<TicketScreen>(new Rectangle((int)columns[2], (int)(rows[3]), (int)buttonSize.X, (int)buttonSize.Y), Textures.background_menu, Textures.hoverButton, "exit", null, "optionsscreen_exit", true));
        }

        #endregion
    }
}
