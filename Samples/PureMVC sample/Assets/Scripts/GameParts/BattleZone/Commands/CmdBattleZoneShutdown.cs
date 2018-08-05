using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

namespace SampleGameNamespace
{
    /// <summary>
    /// Освобождаем ресурсы игровой зоны
    /// </summary>
    class CmdBattleZoneShutdown : SimpleCommand
    {

        public override void Execute(INotification notification)
        {
            Debug.Log("execute CmdBattleZoneShutdown");
            Facade.RemoveMediator(MdBattleZone.NAME);
            Facade.RemoveProxy(PrBattleZone.NAME);
        }
    }
}
