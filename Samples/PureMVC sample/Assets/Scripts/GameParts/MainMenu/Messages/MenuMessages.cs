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
       
        // Нажата клавиша. Телом - код клавиши
        //public const string NOTE_KEY_PRESSED = APP_PREFIX + "note/keyPressed";

        // громкость, вторым аргументом float
        public const string NOTE_SETTINGS_VOLUME = APP_PREFIX + "note/settings/volume";
        // Использовать ли аудиао, вторым аргументом идет bool
        public const string NOTE_SETTINGS_AUDIO = APP_PREFIX + "note/settings/audio";
        // Использовать ли музыку, вторым аргументом идет bool
        public const string NOTE_SETTINGS_MUSIC = APP_PREFIX + "note/settings/music";
        // уровень сложности, вторым аргументом идет bool
        public const string NOTE_SETTINGS_LEVEL = APP_PREFIX + "note/settings/level";



        // Состояния главного меню
        // не инициализировано
        public const string STATE_UNKNOWN = APP_PREFIX + "mm/state/unknown";
        // идет инициализация
        public const string STATE_INIT = APP_PREFIX + "mm/state/init";
        // главное меню
        public const string STATE_MENU = APP_PREFIX + "mm/state/menu";
        // меню настроек
        public const string STATE_SETTINGS = APP_PREFIX + "mm/state/settings";


        // Изменить состояние меню. Типом передается новое состояние
        public const string STATE_CHANGE = APP_PREFIX + "mm/note/changeState";
        // состояние было изменено. Типом передается установившееся состояние
        public const string STATE_WAS_CHANGED = APP_PREFIX + "mm/note/stateWasChanged";

    }

}