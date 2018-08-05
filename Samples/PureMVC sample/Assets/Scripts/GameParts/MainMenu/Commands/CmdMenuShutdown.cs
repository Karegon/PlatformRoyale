using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using SampleGameNamespace;
using UnityEngine;

namespace SampleGameNamespace
{
    /// <summary>
    /// Команда заканчивает работать со сценой главного меню
    /// </summary>
    class CmdMenuShutdown : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            Debug.Log("execute CmdMenuShutdown");
            Facade.RemoveMediator(MdMenuScene.NAME);
            Facade.RemoveProxy(PrMenuScene.NAME);
        }



    }
}
