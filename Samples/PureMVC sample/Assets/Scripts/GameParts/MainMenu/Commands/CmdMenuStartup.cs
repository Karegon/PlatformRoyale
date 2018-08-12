using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;


namespace SampleGameNamespace
{
    /*
     * Пример команды. Данная команда запускает прокси и медиаторы, необходимые для сцены
     */ 
    public class CmdMenuStartup : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            Debug.Log("execute CmdMenuStartup " + this.ToString());

            Facade.RegisterProxy(new PrMenuScene());
            Facade.RegisterMediator(new MdMenuScene());
        }

    }
}
