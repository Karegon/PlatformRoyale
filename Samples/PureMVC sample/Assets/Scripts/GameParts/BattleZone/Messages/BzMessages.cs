using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleGameNamespace
{   
    /// <summary>
    /// Сообщения в боевой зоне
    /// </summary>
    class BzMessages : BaseMessages
    {
        // Состояния боевой зоны
        // не инициализировано
        public const string STATE_UNKNOWN = APP_PREFIX + "bz/state/unknown";
        // идет инициализация
        public const string STATE_INIT = APP_PREFIX + "bz/state/init";
        // идет игра
        public const string STATE_IN_PROGRESS = APP_PREFIX + "bz/state/inPrigress";
        // игра остановлена
        public const string STATE_PAUSE = APP_PREFIX + "bz/state/pause";
        // игра окончена
        public const string STATE_END = APP_PREFIX + "bz/state/end";

        // Изменить состояние игровой зоны. Типом передается новое состояние
        public const string STATE_CHANGE = APP_PREFIX + "bz/note/changeState";
        // состояние игровой зоны было изменено. Типом передается установившееся состояние
        public const string STATE_WAS_CHANGED = APP_PREFIX + "bz/note/stateWasChanged";

        // здоровье игрока изменилось
        public const string HEALTH_CHANGED = APP_PREFIX + "bz/note/healthWasChanged";
        // требуется развернуть аватар
        public const string AVATAR_FLIP = APP_PREFIX + "bz/note/avatarFlip";
        // аватар прыгает
        public const string AVATAR_JUMP = APP_PREFIX + "bz/note/avatarJump";
        // аватар движется 
        public const string AVATAR_MOVE = APP_PREFIX + "bz/note/avatarMove";
        // сменить анимацию аватара
        public const string AVATAR_ANIM_CHANGE = APP_PREFIX + "bz/note/avatarAnimChange";
        // сменить анимацию аватара
        public const string AVATAR_DEAD = APP_PREFIX + "bz/note/avatarDead";

        // сменить оружие
        public const string WEAPON_CHANGE = APP_PREFIX + "bz/note/weaponChange";
        // оружие было изменено
        public const string WEAPON_WAS_CHANGED = APP_PREFIX + "bz/note/weaponWasChanged";
        // огонь!
        public const string WEAPON_FIRE = APP_PREFIX + "bz/note/weaponFire";
        // создать пулю
        public const string WEAPON_CREATE_BULLET = APP_PREFIX + "bz/note/WeaponCreateBullet";
        // осечка
        public const string WEAPON_MISFIRE = APP_PREFIX + "bz/note/WeaponMisfire";
        

        // Команды боевой зоны
        public const string CMD_BATTLE_ZONE_STARTUP = APP_PREFIX + "bz/cmd/startup";
        public const string CMD_BATTLE_ZONE_SHUTDOWN = APP_PREFIX + "bz/cmd/shutdown";



    }
}
