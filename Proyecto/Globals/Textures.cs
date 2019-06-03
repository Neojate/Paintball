using Microsoft.Xna.Framework.Graphics;

namespace Proyecto
{
    class Textures
    {
        public static Texture2D prova_imatge;        
        public static Texture2D shoot;

        //pixel blanco y negro
        public static Texture2D white;
        public static Texture2D black;

        //texturas de titleScreen
        public static Texture2D background_menu;
        public static Texture2D p2w_title;

        //texturas del mapa
        public static Texture2D grassMap;
        public static Texture2D podMap;

        //texturas de los obstaculos
        public static Texture2D obstacle;

        //texturas de gameinterface
        public static Texture2D pod;

        //texturas del player
        public static Texture2D headAnim;
        public static Texture2D maskAnim;
        public static Texture2D bodyAnim;
        public static Texture2D markerAnim;

        //texturas de OptionsScreen
        public static Texture2D rightArrow;
        public static Texture2D leftArrow;
        public static Texture2D rightArrowHover;
        public static Texture2D leftArrowHover;

        //texturas de Button
        public static Texture2D hoverButton;
        public static Texture2D alfaHover;

        //texturas de Alert
        public static Texture2D alert_background;

        //imágenes armas
        public static Texture2D markerSpyderVictor;
        public static Texture2D markerTippmann98;
        public static Texture2D markerTippmannA5;
        public static Texture2D markerEmpireAxe;
        public static Texture2D markerEclipseCSR;
        public static Texture2D markerT68;

        //imágenes habilidades
        public static Texture2D skill1;
        public static Texture2D skill2;
        public static Texture2D skill3;
        public static Texture2D skill4;
        public static Texture2D skill5;

        public static Texture2D skillLine;        

        //Tienda
        public static Texture2D marketShop;

        //Icons
        public static Texture2D icon;

        //Imágen teclado
        public static Texture2D keyboard;

        public static void Load()
        {
            shoot = Globals.content.Load<Texture2D>("GFX/shoot");
            prova_imatge = Globals.content.Load<Texture2D>("GFX/prova");            

            //pixel blanco y negro
            white = Globals.content.Load<Texture2D>("GFX/white_pixel");
            black = Globals.content.Load<Texture2D>("GFX/black_pixel");

            //texturas de titleScreen
            background_menu = Globals.content.Load<Texture2D>("GFX/components/menu_background");
            p2w_title = Globals.content.Load<Texture2D>("GFX/p2w_title");

            //texturas del mapa
            grassMap = Globals.content.Load<Texture2D>("GFX/map/grassmap");
            podMap = Globals.content.Load<Texture2D>("GFX/map/podmap");

            //texturas de los obstaculos
            obstacle = Globals.content.Load<Texture2D>("GFX/obstacles/obstacles");

            //texturas de gameinterface
            pod = Globals.content.Load<Texture2D>("GFX/map/pod");

            //texturas del player
            headAnim = Globals.content.Load<Texture2D>("GFX/player/headtotalanim");
            maskAnim = Globals.content.Load<Texture2D>("GFX/player/masktotalanim");
            bodyAnim = Globals.content.Load<Texture2D>("GFX/player/bodytotalanim");            

            //texturas de OptionsScreen
            rightArrow = Globals.content.Load<Texture2D>("GFX/options/rightArrow");
            leftArrow = Globals.content.Load<Texture2D>("GFX/options/leftArrow");
            rightArrowHover = Globals.content.Load<Texture2D>("GFX/options/rightArrowHover");
            leftArrowHover = Globals.content.Load<Texture2D>("GFX/options/leftArrowHover");

            //texturas de Button
            hoverButton = Globals.content.Load<Texture2D>("GFX/components/hoverButton");
            alfaHover = Globals.content.Load<Texture2D>("GFX/alfaHover");

            //texturas de Alert
            alert_background = Globals.content.Load<Texture2D>("GFX/components/alert_bck");

            //imágenes armas
            markerSpyderVictor = Globals.content.Load<Texture2D>("GFX/markers/marker_spydervictor");
            markerTippmann98 = Globals.content.Load<Texture2D>("GFX/markers/marker_tp98");
            markerTippmannA5 = Globals.content.Load<Texture2D>("GFX/markers/marker_tpa5");
            markerEmpireAxe = Globals.content.Load<Texture2D>("GFX/markers/marker_empireaxe");
            markerEclipseCSR = Globals.content.Load<Texture2D>("GFX/markers/marker_eclipsegeo");
            markerT68 = Globals.content.Load<Texture2D>("GFX/markers/marker_t68");

            //habilidades
            skill1 = Globals.content.Load<Texture2D>("GFX/player/skill1");
            skill2 = Globals.content.Load<Texture2D>("GFX/player/skill2");
            skill3 = Globals.content.Load<Texture2D>("GFX/player/skill3");
            skill4 = Globals.content.Load<Texture2D>("GFX/player/skill4");
            skill5 = Globals.content.Load<Texture2D>("GFX/player/skill5");
            skillLine = Globals.content.Load<Texture2D>("GFX/components/skillline");

            //Tienda
            marketShop = Globals.content.Load<Texture2D>("GFX/bckShop");

            //Icons
            icon = Globals.content.Load<Texture2D>("GFX/icons");

            //Imágen teclado
            keyboard = Globals.content.Load<Texture2D>("GFX/keyboard");
        }
    }
}
