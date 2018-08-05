using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

namespace SampleGameNamespace
{
    /*
     * Команда запускает лоадер игры
     */
    public class CmdLoaderStartup : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            Debug.Log("execute CmdLoaderStartup");
            Bootstrap data = notification.Body as Bootstrap;
            Facade.RegisterMediator(new MdSceneController(data));
        }

    }
}

