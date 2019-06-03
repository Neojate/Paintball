using Microsoft.Xna.Framework;

namespace Proyecto
{
    public enum SkillType
    {
        GENERIC, CHARGER, RELOAD, AIM, STAMINA
    }

    public class Skill : BaseEquip
    {      
        //Nivel de mejora de la habilidad
        private Vector2 level;

        //Nivel necesario de la habilidad anterior
        private int requirements;

        public Skill(SkillType skill)
        {
            language = new Language();

            //Cargar habilidad
            loadSkill(skill);
        }

        #region METODOS PRIVADOS

        /** Metodo que carga las características de cada habilidad */
        private void loadSkill(SkillType skill)
        {
            switch (skill)
            {
                case SkillType.GENERIC:
                    name = language.getMessage("generic_name");
                    description = language.getMessage("generic_description");
                    price = "100";
                    texture = Textures.skill1;
                    level = new Vector2(0, 3);
                    requirements = 0;
                    break;
                case SkillType.CHARGER:
                  name = language.getMessage("charger_name");
                    description = language.getMessage("charger_description");
                    price = "150";
                    texture = Textures.skill2;
                    level = new Vector2(0, 3);
                    requirements = 2;
                    break;
                case SkillType.RELOAD:
                    name = language.getMessage("reload_name");
                    description = language.getMessage("reload_description");
                    price = "150";
                    texture = Textures.skill3;
                    level = new Vector2(0, 3);
                    requirements = 3;
                    break;
                case SkillType.AIM:
                    name = language.getMessage("aim_name");
                    description = language.getMessage("aim_description");
                    price = "250";
                    texture = Textures.skill4;
                    level = new Vector2(0, 5);
                    requirements = 3;
                    break;
                case SkillType.STAMINA:
                    name = language.getMessage("stamina_name");
                    description = language.getMessage("stamina_description");
                    price = "150";
                    texture = Textures.skill5;
                    level = new Vector2(0, 5);
                    requirements = 1;
                    break;
            }
        }

        #endregion

        #region GETTERS Y SETTERS

        public Vector2 getLevel() { return level; }
        public void setLevel(Vector2 level) { this.level = level; } 

        public int getRequirements() { return requirements; }

        #endregion
    }
}
