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
     * Команда выгружает игру
     */
    public class CmdLoaderShutdown : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            Debug.Log("execute CmdLoaderShutdown");
            Facade.RemoveMediator(MdSceneController.NAME);
        }

    }
}
