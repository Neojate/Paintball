using System;

namespace Proyecto
{
    public enum TriggerType
    {
        SHOP_EQUIP,
        SHOP_SKILLS,
        SHOP_TOURNAMENT,
        DOOR_ENTRANCE,
        DOOR_EXIT,
        SCORE_BOARD,
        TICKET_SELLER
    }

    public class Trigger
    {
        //instancia de language
        private Language language;

        //tipo de trigger
        private TriggerType type;

        //mensaje del trigger
        private String message;

        public Trigger(TriggerType type)
        {
            language = new Language();

            this.type = type;
            calculateMessage();
        }

        #region MÉTODOS PRIVADOS
        
        /** Texto del trigger */
        private void calculateMessage()
        {
            switch(type)
            {
                case TriggerType.SHOP_EQUIP:
                case TriggerType.SHOP_SKILLS:
                case TriggerType.SHOP_TOURNAMENT:
                    message = language.getMessage("trigger_talk");
                    break;
                case TriggerType.DOOR_ENTRANCE:
                    message = language.getMessage("trigger_in");
                    break;
                case TriggerType.DOOR_EXIT:
                    message = language.getMessage("trigger_exit");
                    break;
                case TriggerType.SCORE_BOARD:
                    message = language.getMessage("trigger_view");
                    break;
                case TriggerType.TICKET_SELLER:
                    message = language.getMessage("trigger_talk");
                    break;
            }
        }

        #endregion

        #region GETTERS Y SETTERS

        public TriggerType getTriggerType() { return type; }

        public String getTriggermessage() { return message; }

        #endregion
    }
}
