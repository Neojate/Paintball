using System;
using System.Collections.Generic;

namespace Proyecto
{
    public class Language
    {
        public const int SPANISH = 0;
        public const int CATALAN = 1;
        public const int ENGLISH = 2;

        private Dictionary<String, String> traduction;

        public Language()
        {
            traduction = new Dictionary<String, String>();

            switch (Globals.language)
            {
                #region CASTELLANO
                case SPANISH:
                    //TitleScreen
                    traduction.Add("menutitle_option_newgame", "Nuevo Juego");
                    traduction.Add("menutitle_option_continue", "Continuar");
                    traduction.Add("menutitle_option_option", "Opciones");
                    traduction.Add("menutitle_option_credits", "Creditos");
                    traduction.Add("menutitle_option_exit", "Salir");
                    traduction.Add("menutitle_confirm_newgame", "Los datos guardados se borrarán.");

                    //OptionsScreen
                    traduction.Add("optionsscreen_title", "Opciones");
                    traduction.Add("optionsscreen_resolution", "Resolución");
                    traduction.Add("optionsscreen_mode", "Modo");
                    traduction.Add("optionsscreen_dificult", "Dificultad");
                    traduction.Add("optionsscreen_language", "Idioma");

                    traduction.Add("optionsscreen_mode_fs", "Pantalla Completa");
                    traduction.Add("optionsscreen_mode_w", "Ventana");

                    traduction.Add("optionsscreen_difficult_easy", "Fácil");
                    traduction.Add("optionsscreen_difficult_medium", "Media");
                    traduction.Add("optionsscreen_difficult_hard", "Difícil");

                    traduction.Add("optionsscreen_language_spn", "Español");
                    traduction.Add("optionsscreen_language_cat", "Catalán");
                    traduction.Add("optionsscreen_language_eng", "Inglés");

                    traduction.Add("optionsscreen_accept", "Aceptar");
                    traduction.Add("optionsscreen_exit", "Salir");

                    traduction.Add("optionsscreen_alert", "Los cambios se efectuarán al reniciar la aplicación");

                    //CreditsScreen
                    traduction.Add("creditsScreen_name", "Créditos");
                    traduction.Add("creditsScreen_develop", "EQUIPO DE DESARROLLO");
                    traduction.Add("creditsScreen_producer", "Productor y Director del Proyecto");
                    traduction.Add("creditsScreen_programmers", "Programadores");
                    traduction.Add("creditsScreen_designers", "Diseñadores");
                    traduction.Add("creditsScreen_tester", "Control de Errores");
                    traduction.Add("creditsScreen_thanks", "Agradecimientos Especiales a");

                    //GameScreen
                    traduction.Add("gamescreen_stamina", "Estamina");

                    //InGameOptionsScreen
                    traduction.Add("menuInGame_continue", "Continuar");
                    traduction.Add("menuInGame_surrender", "Rendirse");

                    //MarketScreen
                    traduction.Add("market_buy", "Comprar");
                    traduction.Add("market_price", "Precio: ");
                    traduction.Add("market_equip", "Equipar");
                    traduction.Add("market_buyed", "Has comprado el arma.");
                    traduction.Add("market_equiped", "Has equipado el arma.");
                    traduction.Add("market_money", "Dinero insuficiente.");

                    //StartEndScreen
                    traduction.Add("startEnd_win", "Has Ganado");
                    traduction.Add("startEnd_gameOver", "Has Perdido");

                    //StageMap
                    traduction.Add("stageMap_lvl", "Nivel actual: ");

                    //TutorialScreen
                    traduction.Add("tutorial_controls", "Controles");
                    traduction.Add("tutorial_reload", "Recargar.");
                    traduction.Add("tutorial_w_key", "Avanzar.");
                    traduction.Add("tutorial_s_key", "Retroceder.");
                    traduction.Add("tutorial_d_key", "Moverse a la derecha.");
                    traduction.Add("tutorial_a_key", "Moverse a la izquiera.");
                    traduction.Add("tutorial_shift_key", "Correr.");
                    traduction.Add("tutorial_e_key", "Hablar.");
                    traduction.Add("tutorial_q_key", "Centrar la cámara.");
                    traduction.Add("tutorial_f1_key", "Mostrar FPS.");
                    traduction.Add("tutorial_esc_key", "Salir.");

                    //--------------Markers-----------------
                    //Spyder Victor
                    traduction.Add("spyderVictor_name", "Spyder Victor");
                    traduction.Add("spyderVictor_description", "Modelo básico de iniciación. Esta marcadora semiautomática de pobre precisión y cadencia, es una compra seguro para todos aquellos jugadores nóveles que desean iniciarse en el mundo del paintball.");

                    //Tippmann 98
                    traduction.Add("tippmann98_name", "Tippmann 98");
                    traduction.Add("tippmann98_description", "Marcadora semiautomática equilibrada. Pese a no tener mucha cadena de disparo ni una gran precisión, se ha convertido en un arma mundialmente conocido por su increible robustez.");

                    //Tippmann A5
                    traduction.Add("tippmannA5_name", "Tippmann A5 AK Model");
                    traduction.Add("tippmannA5_description", "Marcadora automática, evolución directa del modelo 98. No es tan robusta como su predecesora, sin embargo mejora en precisión y frecuencia de disparo. Su único punto flaco es un cargador de sólo 50 cargas.");

                    //Empryre Axe
                    traduction.Add("empireAxe_name", "Empire Axe 2.0");
                    traduction.Add("empireAxe_description", "Marcadora electrónica de competición. Ligera, buena precisión, muy buen ratio de disparo y un cargador de competición. Amada por todos los jugadores de pista, su elevado precio compensa sus prestaciones.");

                    //Eclipse Geo
                    traduction.Add("eclipseGeo_name", "Eclipse Geo CSR");
                    traduction.Add("eclipseGeo_description", "Marcadora electrónica de altísimas prestación. Es extremadamente liviana, muy precisa, con un ratio de disparo insuperable y un cargador de competición. Aunque quizás peca de cara, muchos jugadores la consideran la mejor marcadora de paintball pista.");

                    //Rap4
                    traduction.Add("rap4_name", "Rap4 T68");
                    traduction.Add("rap4_description", "Marcadora semiautomática milsig. Es pesada, con un ratio de disparo muy bajo y un cargador de sólo 20 cargas. Por contra, es una arma extremadamente precisa a larga distancia. Sólo puede usar munición First Strike.");

                    //--------------Skills-----------------
                    //Generic
                    traduction.Add("generic_name", "Maestría paintball.");
                    traduction.Add("generic_description", "Habilidad genérica pero necesaria para aumentar el resto de habilidades del personaje.");

                    //Charger
                    traduction.Add("charger_name", "Cargador ampliado.");
                    traduction.Add("charger_description", "Esta habilidad aumenta el número de cargadores disponibles por partida para todas las marcadoras.");

                    //Reload
                    traduction.Add("reload_name", "Maestro de la recarga.");
                    traduction.Add("reload_description", "Esta habilidad aumenta la barra de recarga, haciendo que sea mucho más fácil recargar la marcadora.");

                    //Aim
                    traduction.Add("aim_name", "Puntería.");
                    traduction.Add("aim_description", "Esta habilidad aumenta la precisión al disparar.");

                    //Stamina
                    traduction.Add("stamina_name", "Resistencia.");
                    traduction.Add("stamina_description", "Esta habilidad aumenta la barra de aguante del personaje, lo que permite correr durante más tiempo.");

                    traduction.Add("skill_lvl", "Nivel actual: ");

                    //TicketScreen
                    traduction.Add("ticket_price", "Precio de la entrada: ");
                    traduction.Add("ticket_buyed", "Ya has comprado la entrada.");

                    //Trigger
                    traduction.Add("trigger_talk", "Pulsa <<E>> para hablar.");
                    traduction.Add("trigger_in", "Pulsa <<E>> para entrar.");
                    traduction.Add("trigger_exit", "Pulsa <<E>> para salir.");
                    traduction.Add("trigger_view", "Pulsa <<E>> para ver la pizarra.");

                    //Invulnerabilidad
                    traduction.Add("player_mode", "Modo Dios");

                    //Preposiciones
                    traduction.Add("skill_preposition", " de ");

                    break;
                #endregion

                #region CATALAN
                case CATALAN:
                    //TitleScreen
                    traduction.Add("menutitle_option_newgame", "Nou Joc");
                    traduction.Add("menutitle_option_continue", "Continuar");
                    traduction.Add("menutitle_option_option", "Opcions");
                    traduction.Add("menutitle_option_credits", "Credits");
                    traduction.Add("menutitle_option_exit", "Sortir");
                    traduction.Add("menutitle_confirm_newgame", "Les dades guardades s'esborraran.");

                    //OpctionsScreen
                    traduction.Add("optionsscreen_title", "Opcions");
                    traduction.Add("optionsscreen_resolution", "Resolucio");
                    traduction.Add("optionsscreen_mode", "Mode");
                    traduction.Add("optionsscreen_dificult", "Dificultat");
                    traduction.Add("optionsscreen_language", "Llengua");

                    traduction.Add("optionsscreen_mode_fs", "Pantalla completa");
                    traduction.Add("optionsscreen_mode_w", "Finestra");

                    traduction.Add("optionsscreen_difficult_easy", "Fàcil");
                    traduction.Add("optionsscreen_difficult_medium", "Mitja");
                    traduction.Add("optionsscreen_difficult_hard", "Difícil");

                    traduction.Add("optionsscreen_language_spn", "Espanyol");
                    traduction.Add("optionsscreen_language_cat", "Català");
                    traduction.Add("optionsscreen_language_eng", "Anglès");

                    traduction.Add("optionsscreen_accept", "Acceptar");
                    traduction.Add("optionsscreen_exit", "Sortir");

                    traduction.Add("optionsscreen_alert", "Els canvis s'efectuaran en reiniciar l'aplicació");

                    //CreditsScreen
                    traduction.Add("creditsScreen_name", "Crèdits");
                    traduction.Add("creditsScreen_develop", "EQUIP DE DESENVOLUPAMENT");
                    traduction.Add("creditsScreen_producer", "Productor i Director del Projecte");
                    traduction.Add("creditsScreen_programmers", "Programadors");
                    traduction.Add("creditsScreen_designers", "Dissenyadors");
                    traduction.Add("creditsScreen_tester", "Control d'Errors");
                    traduction.Add("creditsScreen_thanks", "Un Agraïment Especial a");

                    //GameScreen
                    traduction.Add("gamescreen_stamina", "Estamina");

                    //InGameOptionsScreen
                    traduction.Add("menuInGame_continue", "Continuar");
                    traduction.Add("menuInGame_surrender", "Rendirse");

                    //MarketScreen
                    traduction.Add("market_buy", "Comprar");
                    traduction.Add("market_price", "Preu: ");
                    traduction.Add("market_equip", "Equipar");
                    traduction.Add("market_buyed", "Has comprat l'arma.");
                    traduction.Add("market_equiped", "Has equipat l'arma.");
                    traduction.Add("market_money", "Diners insuficients.");

                    //StartEndScreen
                    traduction.Add("startEnd_win", "Has Guanyat");
                    traduction.Add("startEnd_gameOver", "Has Perdut");

                    //StageMap
                    traduction.Add("stageMap_lvl", "Nivell actual: ");

                    //TutorialScreen
                    traduction.Add("tutorial_controls", "Controls");
                    traduction.Add("tutorial_reload", "Recarregar.");
                    traduction.Add("tutorial_w_key", "Avançar.");
                    traduction.Add("tutorial_s_key", "Retrocedir.");
                    traduction.Add("tutorial_d_key", "Moures a la dreta.");
                    traduction.Add("tutorial_a_key", "Moures a l'esquerra.");
                    traduction.Add("tutorial_shift_key", "Córrer.");
                    traduction.Add("tutorial_e_key", "Parlar.");
                    traduction.Add("tutorial_q_key", "Centrar la càmera.");
                    traduction.Add("tutorial_f1_key", "Mostrar FPS.");
                    traduction.Add("tutorial_esc_key", "Sortir.");

                    //--------------Markers-----------------
                    //Spyder Victor
                    traduction.Add("spyderVictor_name", "Spyder Victor");
                    traduction.Add("spyderVictor_description", "Model bàsic d'initciació. Aquesta marcadora semiautomàtica de pobre precisió i cadència, és una compra segura per a tots aquells jugadors novells que volen iniciar-se en el món del paintball.");

                    //Tippmann 98
                    traduction.Add("tippmann98_name", "Tippmann 98");
                    traduction.Add("tippmann98_description", "Marcadora semiautomàtica equilibrada. Tot i no teindre molta cadència de tret ni una gran precisió, s'ha convertit en una arma mundialment coneguda per la seva increïble robustesa.");

                    //Tippmann A5
                    traduction.Add("tippmannA5_name", "Tippmann A5 AK Model");
                    traduction.Add("tippmannA5_description", "Marcadora automàtica, evolució directa del model 98.No és tan robusta com la seva predecessora, però millora en precisió i freqüència de tret.El seu únic punt feble és un carregador de només 50 càrregues.");

                    //Empryre Axe
                    traduction.Add("empireAxe_name", "Empire Axe 2.0");
                    traduction.Add("empireAxe_description", "Marcadora electrònica de competició. Lleugera, bona precisió, molt bon ràtio de tret i un carregador de competició. Estimada per tots els jugadors de pista, el seu elevat preu compensa les seves prestacions.");

                    //Eclipse Geo
                    traduction.Add("eclipseGeo_name", "Eclipse Geo CSR");
                    traduction.Add("eclipseGeo_description", "Marcadora electrònica d'altíssimes prestacions. És extremadament lleugera, molt precisa, amb una ràtio de tret insuperable i un carregador de competició. Encara que potser peca de cara, molts jugadors la consideren la millor marcadora de paintball pista.");

                    //Rap4
                    traduction.Add("rap4_name", "Rap4 T68");
                    traduction.Add("rap4_description", "Marcadora semiautomàtica milsig. És pesada, amb una ràtio de tret molt baix i un carregador de només 20 càrregues. Per contra, és una arma extremadament precisa a llarga distància. Només pot usar munició First Strike.");

                    //--------------Skills-----------------
                    //Generic
                    traduction.Add("generic_name", "Mestratge paintball.");
                    traduction.Add("generic_description", "Habilitat genèrica però necessària per augmentar la resta d'habilitats del personatge.");

                    //Charger
                    traduction.Add("charger_name", "Carregador ampliat.");
                    traduction.Add("charger_description", "Aquesta habilitat augmenta el nombre de carregadors disponibles per partida per a totes les marcadores.");

                    //Reload
                    traduction.Add("reload_name", "Mestre de la recàrrega.");
                    traduction.Add("reload_description", "Aquesta habilitat augmenta la barra de recàrrega, fent que sigui molt més fàcil recarregar la marcadora.");

                    //Aim
                    traduction.Add("aim_name", "Punteria.");
                    traduction.Add("aim_description", "Aquesta habilitat augmenta la precisió en disparar.");

                    //Stamina
                    traduction.Add("stamina_name", "Resistència.");
                    traduction.Add("stamina_description", "Aquesta habilitat augmenta la barra de resistència del personatge, el que permet córrer durant més temps.");

                    traduction.Add("skill_lvl", "Nivell actual: ");

                    //TicketScreen
                    traduction.Add("ticket_price", "Preu de l'entrada: ");
                    traduction.Add("ticket_buyed", "Ja has comprat l'entrada.");

                    //Trigger
                    traduction.Add("trigger_talk", "Prem <<E>> per parlar.");
                    traduction.Add("trigger_in", "Prem <<E>> per entrar.");
                    traduction.Add("trigger_exit", "Prem <<E>> per sortir.");
                    traduction.Add("trigger_view", "Prem <<E>> per veure la pissarra.");

                    //Invulnerabilidad
                    traduction.Add("player_mode", "Mode Deu");

                    //Preposiciones
                    traduction.Add("skill_preposition", " de ");

                    break;
                #endregion

                #region INGLES
                case ENGLISH:
                    //TitleScreen
                    traduction.Add("menutitle_option_newgame", "New Game");
                    traduction.Add("menutitle_option_continue", "Continue");
                    traduction.Add("menutitle_option_option", "Options");
                    traduction.Add("menutitle_option_credits", "Credits");
                    traduction.Add("menutitle_option_exit", "Exit");
                    traduction.Add("menutitle_confirm_newgame", "The saved data will be deleted.");

                    //OptionsScreen
                    traduction.Add("optionsscreen_title", "Options");
                    traduction.Add("optionsscreen_resolution", "Resolution");
                    traduction.Add("optionsscreen_mode", "Mode");
                    traduction.Add("optionsscreen_dificult", "Difficulty");
                    traduction.Add("optionsscreen_language", "Language");

                    traduction.Add("optionsscreen_mode_fs", "Fullscreen");
                    traduction.Add("optionsscreen_mode_w", "Windowed");

                    traduction.Add("optionsscreen_difficult_easy", "Easy");
                    traduction.Add("optionsscreen_difficult_medium", "Medium");
                    traduction.Add("optionsscreen_difficult_hard", "Hard");

                    traduction.Add("optionsscreen_language_spn", "Spanish");
                    traduction.Add("optionsscreen_language_cat", "Catalan");
                    traduction.Add("optionsscreen_language_eng", "English");

                    traduction.Add("optionsscreen_accept", "Accept");
                    traduction.Add("optionsscreen_exit", "Exit");

                    traduction.Add("optionsscreen_alert", "Changes will be applied when the application restarts");

                    //CreditsScreen
                    traduction.Add("creditsScreen_name", "Credits");
                    traduction.Add("creditsScreen_develop", "DEVELOPMENT TEAM");
                    traduction.Add("creditsScreen_producer", "Producer and Project Director");
                    traduction.Add("creditsScreen_programmers", "Programmers");
                    traduction.Add("creditsScreen_designers", "Designers");
                    traduction.Add("creditsScreen_tester", "Testers");
                    traduction.Add("creditsScreen_thanks", "Special Thanks To");

                    //GameScreen
                    traduction.Add("gamescreen_stamina", "Stamina");

                    //pantalla InGameOptions
                    traduction.Add("menuInGame_continue", "Continue");
                    traduction.Add("menuInGame_surrender", "Surrender");

                    //MarketScreen
                    traduction.Add("market_buy", "Buy");
                    traduction.Add("market_price", "Price: ");
                    traduction.Add("market_equip", "Equip");
                    traduction.Add("market_buyed", "You have bought the weapon.");
                    traduction.Add("market_equiped", "You have equipped the weapon.");
                    traduction.Add("market_money", "Insufficient money.");

                    //StartEndScreen
                    traduction.Add("startEnd_win", "You Win");
                    traduction.Add("startEnd_gameOver", "Game Over");

                    //StageMap
                    traduction.Add("stageMap_lvl", "Current level: ");

                    //TutorialScreen
                    traduction.Add("tutorial_controls", "Controls");
                    traduction.Add("tutorial_reload", "Reload.");
                    traduction.Add("tutorial_w_key", "Move.");
                    traduction.Add("tutorial_s_key", "Move back.");
                    traduction.Add("tutorial_d_key", "Move right.");
                    traduction.Add("tutorial_a_key", "Move left.");
                    traduction.Add("tutorial_shift_key", "Run.");
                    traduction.Add("tutorial_e_key", "Talk.");
                    traduction.Add("tutorial_q_key", "Center camera.");
                    traduction.Add("tutorial_f1_key", "Show FPS.");
                    traduction.Add("tutorial_esc_key", "Exit.");

                    //--------------Markers-----------------
                    //Spyder Victor
                    traduction.Add("spyderVictor_name", "Spyder Victor");
                    traduction.Add("spyderVictor_description", "Basic model of initiation. This semi-automatic marker of poor precision and cadence is a safe purchase for all those new players who want to start in the world of paintball.");

                    //Tippmann 98
                    traduction.Add("tippmann98_name", "Tippmann 98");
                    traduction.Add("tippmann98_description", "Balanced semiautomatic marker. Despite not having much chain of shot nor a great precision, it has become a weapon known worldwide for its incredible robustness.");

                    //Tippmann A5
                    traduction.Add("tippmannA5_name", "Tippmann A5 AK Model");
                    traduction.Add("tippmannA5_description", "Automatic marker, direct evolution of the model 98. It is not as robust as its predecessor, however it improves in accuracy and firing frequency. Its only weak point is a charger of only 50 charges.");

                    //Empryre Axe
                    traduction.Add("empireAxe_name", "Empire Axe 2.0");
                    traduction.Add("empireAxe_description", "Electronic competition marker. Light weight, good precision, very good shooting ratio and a competition loader. Loved by all track players, its high price compensates for its benefits.");

                    //Eclipse Geo
                    traduction.Add("eclipseGeo_name", "Eclipse Geo CSR");
                    traduction.Add("eclipseGeo_description", "High performance electronic marker. It is extremely light, very precise, with an unbeatable shooting ratio and a competition loader. Although it may be very expensive, many players consider it the best trackball marker..");

                    //Rap4
                    traduction.Add("rap4_name", "Rap4 T68");
                    traduction.Add("rap4_description", "Semi-automatic marker milsig. It is heavy, with a very low shoot ratio and a charger of only 20 charges. On the other hand, it is an extremely accurate long-range weapon. You can only use First Strike ammunition.");

                    //--------------Skills-----------------
                    //Generic
                    traduction.Add("generic_name", "Paintball master.");
                    traduction.Add("generic_description", "Generic ability but necessary to increase the rest of skills of the character.");

                    //Charger
                    traduction.Add("charger_name", "Extended charger.");
                    traduction.Add("charger_description", "This ability will increase the number of loaders/game available for all markers.");

                    //Reload
                    traduction.Add("reload_name", "Recharger master.");
                    traduction.Add("reload_description", "This ability will increase target's size on recharge bar , making it much easier to recharge the marker.");

                    //Aim
                    traduction.Add("aim_name", "Aim.");
                    traduction.Add("aim_description", "This ability will increase the accuracy when shooting.");

                    //Stamina
                    traduction.Add("stamina_name", "Endurance");
                    traduction.Add("stamina_description", "This ability will increase the character's stamina bar, allowing you to run longer.");

                    traduction.Add("skill_lvl", "current level: ");

                    //TicketScreen
                    traduction.Add("ticket_price", "Ticket price: ");
                    traduction.Add("ticket_buyed", "You have already bought the ticket");

                    //Trigger
                    traduction.Add("trigger_talk", "Press <<E>> to talk.");
                    traduction.Add("trigger_in", "Press <<E>> per enter.");
                    traduction.Add("trigger_exit", "Press <<E>> to exit.");
                    traduction.Add("trigger_view", "Press <<E>> to see the board.");

                    //Invulnerabilidad
                    traduction.Add("player_mode", "God Mode");

                    //Preposiciones
                    traduction.Add("skill_preposition", " of ");

                    break;
                    #endregion
            }
        }

        public String getMessage(String code)
        {
            return traduction[code];
        }
    }
}