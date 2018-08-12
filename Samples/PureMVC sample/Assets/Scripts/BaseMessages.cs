using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleGameNamespace
{
    /// <summary>
    /// Базовый класс для всех нотификаций, посылаемых в шину данных
    /// Наследуй от него классы с доменной областью действия. 
    /// Например, требуется работать с сетью (домен операци - сетевые операции).
    /// Создаем класс NetMessages и описываем в нем необходимые события.
    /// В этом случае, чтобы получить все сетевые нотифицации, достаточно 
    /// обратиться к константам класса NetMessages 
    /// </summary>
    public class BaseMessages
    {
        public const string APP_PREFIX = "MyGame/";
        // запусить лоадер
        public const string CMD_GAME_STARTUP = APP_PREFIX + "note/game/startup";
        // погасить лоадер (и игру, видимо)
        public const string CMD_GAME_SHUTDOWN = APP_PREFIX + "note/game/shutdown";

        // сообщение для контроллера сцены о переключени на другую сцену
        public const string NOTE_SCENE_PREPARE = APP_PREFIX + "note/scenePrepare";
        // сообщение о том, что контроллер сцены загрузил сцену
        public const string NOTE_SCENE_LOADED = APP_PREFIX + "note/SceneLoaded";

        // сообщение о том, что приложение закрывается
        // сообщение приходит, перед тем, как будет выключен медиатор всех сцен
        public const string NOTE_APP_QUIT = APP_PREFIX + "note/appQuit";
        // Запрос от пользователя на закрытие приложения 
        public const string NOTE_APP_QUIT_REQUEST = APP_PREFIX + "note/appQuitRequest";

        // переключение между состояниями приложения, третьим аргументом идет SCENE_UNKNOWN
        // по факту это ключевые экраны приложения
        public const string NOTE_SWITCH_SCENE = APP_PREFIX + "note/switchScene";
        // сцены игры
        public const string SCENE_UNKNOWN = APP_PREFIX + "scene/unknown";
        public const string SCENE_MAIN_MENU = APP_PREFIX + "scene/mainmenu";
        public const string SCENE_BATTLE_ZONE = APP_PREFIX + "scene/battleZone";
    }
}
