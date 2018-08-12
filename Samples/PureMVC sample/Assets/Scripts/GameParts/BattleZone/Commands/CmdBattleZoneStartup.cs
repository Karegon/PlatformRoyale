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
       

        public override void Execute(INotification notification)
        {
            Debug.Log("execute CmdBattleZoneInit");

            SendNotification(BzMessages.STATE_INIT);

            // нужно создать на сцене необходимые игровые элементы, которые по умолчанию не создаются 

            Transform canvas = Tools.FindObjectByName(MyResources.DEF_CANVAS_NAME).transform;

            // создадим медиатор, обслуживающий нажатые игроком клавиши
            Tools.instantiateObject(MyResources.PLAYER_INPUT);
            Debug.Log(MyResources.PLAYER_INPUT + " created");

            // Наэкранный индикатор (HUD)
            Tools.instantiateObject(MyResources.PLAYER_HUD, canvas);
            Debug.Log(MyResources.PLAYER_HUD + " created");

            // персонаж
            Tools.instantiateObject(MyResources.PLAYER_HERO);
            Debug.Log(MyResources.PLAYER_HERO + " created");

            // регистрируем боевую зону и медиатор к ней
            Facade.RegisterProxy(new PrBattleZone());
            //Bootstrap data = UnityEngine.Object.FindObjectOfType<Bootstrap>() as Bootstrap;
            Facade.RegisterMediator(new MdBattleZone());

        }

    }
}
