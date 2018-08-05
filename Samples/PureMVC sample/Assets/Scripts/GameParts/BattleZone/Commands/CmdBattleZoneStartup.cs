using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SampleGameNamespace
{
    /// <summary>
    /// Инициализирует игровую зону
    /// </summary>
    class CmdBattleZoneStartup : SimpleCommand
    {
        private const string PLAYER_INPUT = "prefabs/components/PlayerInput";
        private const string PLAYER_HUD = "prefabs/components/Hud";
        private const string PLAYER_HERO = "prefabs/components/Hero";
        

        public override void Execute(INotification notification)
        {
            Debug.Log("execute CmdBattleZoneInit");

            // регистрируем боевую зону и медиатор к ней
            PrBattleZone zone = new PrBattleZone();
            Facade.RegisterProxy(zone);
            Bootstrap data = UnityEngine.Object.FindObjectOfType<Bootstrap>() as Bootstrap;
            Facade.RegisterMediator(new MdBattleZone(data));

            SendNotification(BzMessages.STATE_INIT);

            // нужно создать на сцене необходимые игровые элементы, которые по умолчанию не создаются 

            // создадим медиатор, обслуживающий нажатые игроком клавиши
            Tools.instantiateObject(PLAYER_INPUT);
            Debug.Log(PLAYER_INPUT + " created");

            // Наэкранный индикатор (HUD)
            Tools.instantiateObject(PLAYER_HUD, Tools.FindObjectByName("Canvas").transform);
            Debug.Log(PLAYER_HUD + " created");

            // персонаж
            Tools.instantiateObject(PLAYER_HERO);
            Debug.Log(PLAYER_HERO + " created");

        }

    }
}
