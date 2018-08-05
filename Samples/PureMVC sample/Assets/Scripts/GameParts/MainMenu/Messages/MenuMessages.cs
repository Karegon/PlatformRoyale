using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SampleGameNamespace
{
    /// <summary>
    /// Глобальный класс для агрегации констант нотификаций
    /// </summary>
    public class MenuMessages : BaseMessages
    {
        public const string CMD_MENU_STARTUP = APP_PREFIX + "note/startUp";
        public const string CMD_MENU_SHUTDOWN = APP_PREFIX + "note/shutdown";

        // состояния игры
        public const string STATE_UNKNOWN = APP_PREFIX + "state/unknown";
        public const string STATE_MAIN_MENU = APP_PREFIX + "state/mainmenu";
        public const string STATE_BATTLE_ZONE = APP_PREFIX + "state/battleZone";
        public const string STATE_SETTINGS = APP_PREFIX + "state/settings";
        public const string STATE_QUIT = APP_PREFIX + "state/quit";

       
        // Нажата клавиша. Телом - код клавиши
        public const string NOTE_KEY_PRESSED = APP_PREFIX + "note/keyPressed";

        // изменение состояния, третьим аргументом идет STATE_UNKNOWN
        public const string NOTE_STATE_SWITCH = APP_PREFIX + "note/stateSwitch";

        // громкость, вторым аргументом float
        public const string NOTE_SETTINGS_VOLUME = APP_PREFIX + "note/settings/volume";
        // Использовать ли аудиао, вторым аргументом идет bool
        public const string NOTE_SETTINGS_AUDIO = APP_PREFIX + "note/settings/audio";
        // Использовать ли музыку, вторым аргументом идет bool
        public const string NOTE_SETTINGS_MUSIC = APP_PREFIX + "note/settings/music";
        // уровень сложности, вторым аргументом идет bool
        public const string NOTE_SETTINGS_LEVEL = APP_PREFIX + "note/settings/level";
        
      
    }

}