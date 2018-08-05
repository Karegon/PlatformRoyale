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
        public const string CMD_LOADER_STARTUP = APP_PREFIX + "note/loader/startUp";
        // погасить лоадер (и игру, видимо)
        public const string CMD_LOADER_SHUTDOWN = APP_PREFIX + "note/loader/shutdown";
        // показать стандартную сцену игры (запускается после лоадера)
        public const string CMD_SHOW_DEFALUT_SCENE = APP_PREFIX + "note/showDefaultScene";
    }
}
