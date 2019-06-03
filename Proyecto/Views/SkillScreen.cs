using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Proyecto
{
    class SkillScreen : BaseScreen
    {
        //Botón
        private Vector2 buttonSize;

        //Filas y columnas
        private float[] columns;
        private float[] rows;
        private Vector2[] skillPosition;

        //Player
        private static Player player;

        //Habilidades
        private static List<Skill> skills;
        private static Skill actualSkill;

        public SkillScreen(Player p)
        {
            Name = "SkillScreen";
            State = ScreenState.Active;

            //Botón
            buttonSize = new Vector2(Globals.gameSize.X * 0.12f, Globals.gameSize.Y * 0.06f);

            //Filas y columnas
            columns = new float[] { Globals.gameSize.X * 0.1f, Globals.gameSize.X * 0.2f, Globals.gameSize.X * 0.68f, Globals.gameSize.X * 0.7f };
            rows = new float[] { Globals.gameSize.Y * 0.1f, Globals.gameSize.Y * 0.7f, Globals.gameSize.Y * 0.71f, Globals.gameSize.Y * 0.74f, Globals.gameSize.Y * 0.9f };
            skillPosition = new Vector2[] { new Vector2(150, 200), new Vector2(350, 90), new Vector2(550, 90), new Vector2(350, 200), new Vector2(350, 310) };

            //Player
            player = p;

            //Habilidades
            skills = player.getSkills();
            actualSkill = skills[0];

            //Creación de la interfaz
            createInterface();
        }

        public override void Update()
        {
            foreach (Component c in components) c.update();
        }

        public override void Draw()
        {
            Globals.spriteBatch.Begin();

            //dibujado de la interfaz
            foreach (Component c in components) c.draw();

            //dibujado de las lineas
            drawSkillLines();

            //dibujado de las descripciones de las habilidades
            Globals.spriteBatch.DrawString(Fonts.arial_12, spliceText(actualSkill.getDescription()), new Vector2(columns[1], rows[3]), Color.White);

            //dibujado precio para subir la habilidad actual
            Globals.spriteBatch.DrawString(Fonts.arial_12, language.getMessage("market_price") + actualSkill.getPrice() + " pln", new Vector2(columns[3], rows[2]), Color.White);

            String text = language.getMessage("skill_lvl") + actualSkill.getLevel().X + language.getMessage("skill_preposition") + actualSkill.getLevel().Y;

            //dibujado nivel actual de la habilidad seleccionada
            Globals.spriteBatch.DrawString(Fonts.arial_12, text, new Vector2(columns[1], rows[2]), Color.White);

            Globals.spriteBatch.End();
        }

        #region LISTENERS

        /** Actualiza la información de la habilidad actual */
        /** skill = habilidad actual */
        public static void skillInfo(Skill skill)
        {
            actualSkill = skill;
        }

        /** Confirmar la compra de la habilidad seleccionada */
        public static void acceptSkill()
        {
            int price = int.Parse(actualSkill.getPrice());
            if (player.getMoney() < price) return;
            if (actualSkill.getLevel().X >= actualSkill.getLevel().Y) return;

            if (actualSkill != skills[0])
            {
                if (checkSkill().getLevel().X < actualSkill.getRequirements()) return;
            }
            
            Vector2 auxLvl = actualSkill.getLevel();
            auxLvl.X++;
            if (auxLvl.X > auxLvl.Y) auxLvl.X = auxLvl.Y;
            actualSkill.setLevel(auxLvl);
            player.setMoney(player.getMoney() - price);
        }

        /** Sale de la pantalla de compra */
        public static void exit()
        {
            ScreenManager.UnloadScreen("SkillScreen");
        }

        #endregion

        #region METODOS PRIVADOS
        /** Creación de la interfaz */
        private void createInterface()
        {
            //Panel
            components.Add(new Panel(new Rectangle((int)columns[0], (int)rows[0], (int)(Globals.gameSize.X * 0.8f), (int)(Globals.gameSize.Y * 0.8f)), Textures.white));
            components.Add(new Panel(new Rectangle((int)columns[0] + 1, (int)rows[0] + 1, (int)(Globals.gameSize.X * 0.8f - 2), (int)(Globals.gameSize.Y * 0.6f - 2)), Textures.background_menu));
            components.Add(new Panel(new Rectangle((int)columns[0] + 1, (int)rows[1], (int)(Globals.gameSize.X * 0.8f - 2), (int)(Globals.gameSize.Y * 0.2f - 1)), Textures.background_menu));

            //Botones habilidades
            for(int i = 0; i < skills.Count; i++)
            {
                components.Add(new Button<SkillScreen>(new Rectangle((int)skillPosition[i].X, (int)skillPosition[i].Y, 64, 64), skills[i].getTexture(), Textures.alfaHover, "skillInfo", new Object[] { skills[i] }, null, true));
            }

            //Botón comprar y salir
            components.Add(new Button<SkillScreen>(new Rectangle((int)columns[1], (int)(rows[4] - buttonSize.Y - 10), (int)buttonSize.X, (int)buttonSize.Y), Textures.background_menu, Textures.hoverButton, "acceptSkill", null, "market_buy", true));
            components.Add(new Button<SkillScreen>(new Rectangle((int)columns[2], (int)(rows[4] - buttonSize.Y - 10), (int)buttonSize.X, (int)buttonSize.Y), Textures.background_menu, Textures.hoverButton, "exit", null, "optionsscreen_exit", true));

        }

        /** Dibuja las lineas entre skills */
        private void drawSkillLines()
        {
            int textX;

            //de primera a segunda
            Color c1T2 = calculateColorSkillLine(skills[0], skills[1]);
            Globals.spriteBatch.Draw(Textures.skillLine, new Rectangle(150 + 32, 90 + 32, 3, 3), new Rectangle(0, 3, 3, 3), c1T2);
            Globals.spriteBatch.Draw(Textures.skillLine, new Rectangle(150 + 32, 93 + 32, 3, 75), new Rectangle(3, 0, 3, 3), c1T2);
            Globals.spriteBatch.Draw(Textures.skillLine, new Rectangle(153 + 32, 90 + 32, 168 - 3, 3), new Rectangle(0, 0, 3, 3), c1T2);
            textX = PaintToWinUtils.centerTextX(new Vector2(150 + 32, 150 + 32 + 168), Fonts.arial_12, "0 / 0");
            Globals.spriteBatch.DrawString(Fonts.arial_12, skills[0].getLevel().X + " / " + skills[1].getRequirements(), new Vector2(textX, 106), c1T2);

            //de segunda a tercera
            Color c2T3 = calculateColorSkillLine(skills[1], skills[2]);
            Globals.spriteBatch.Draw(Textures.skillLine, new Rectangle(350 + 64, 90 + 32, 136, 3), new Rectangle(0, 0, 3, 3), c2T3);
            textX = PaintToWinUtils.centerTextX(new Vector2(350 + 64, 350 + 64 + 136), Fonts.arial_12, "0 / 0");
            Globals.spriteBatch.DrawString(Fonts.arial_12, skills[1].getLevel().X + " / " + skills[2].getRequirements(), new Vector2(textX, 106), c2T3);

            //de primera a cuarta
            Color c1T4 = calculateColorSkillLine(skills[0], skills[3]);
            Globals.spriteBatch.Draw(Textures.skillLine, new Rectangle(150 + 64, 200 + 32, 136, 3), new Rectangle(0, 0, 3, 3), c1T4);
            textX = PaintToWinUtils.centerTextX(new Vector2(150 + 64, 150 + 64 + 136), Fonts.arial_12, "0 / 0");
            Globals.spriteBatch.DrawString(Fonts.arial_12, skills[0].getLevel().X + " / " + skills[3].getRequirements(), new Vector2(textX, 216), c1T4);

            //de primera a quinta
            Color c1T5 = calculateColorSkillLine(skills[0], skills[4]);
            Globals.spriteBatch.Draw(Textures.skillLine, new Rectangle(150 + 32, 310 + 32, 3, 3), new Rectangle(3, 3, 3, 3), c1T5);
            Globals.spriteBatch.Draw(Textures.skillLine, new Rectangle(150 + 32, 200 + 64, 3, 78), new Rectangle(3, 0, 3, 3), c1T5);
            Globals.spriteBatch.Draw(Textures.skillLine, new Rectangle(153 + 32, 310 + 32, 165, 3), new Rectangle(0, 0, 3, 3), c1T5);
            textX = PaintToWinUtils.centerTextX(new Vector2(150 + 32, 150 + 32 + 168), Fonts.arial_12, "0 / 0");
            Globals.spriteBatch.DrawString(Fonts.arial_12, skills[0].getLevel().X + " / " + skills[4].getRequirements(), new Vector2(textX, 326), c1T5);
        }

        /** Método que devuevle el color de la linea de habilidades en funcion de si supera los requisitos */
        /** first = habilidad de la que depende */
        /** second = habilidad que queremos subir */
        private Color calculateColorSkillLine(Skill first, Skill second)
        {
            if (first.getLevel().X >= second.getRequirements()) return Color.Yellow;
            return Color.White;
        }

        /** Método que retorna la habilidad de la que depende su actual */
        private static Skill checkSkill()
        {
            if (actualSkill == skills[2]) return skills[1];
            return skills[0];
        }

        /** Hace un salto de línia cada X longitud de texto */
        /** text = texto al que queremos hacer un salto de línia cada X longitud */
        private static string spliceText(string text)
        {
            int lineLength = text.Length;
            if (Globals.gameSize.X == 800) lineLength = 70;

            return Regex.Replace(text, "(.{" + lineLength + "})", "$1" + Environment.NewLine);
        }

        #endregion

    }
}
