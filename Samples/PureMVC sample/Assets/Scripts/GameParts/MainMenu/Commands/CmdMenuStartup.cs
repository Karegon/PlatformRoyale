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
            Debug.Log("execute CmdMenuStartup");
            registerProxies();
            registerMediators(notification);
        }

        private void registerProxies()
        {
            Facade.RegisterProxy(new PrMenuScene());
        }

        private void registerMediators(INotification note)
        {
            Bootstrap data = UnityEngine.Object.FindObjectOfType<Bootstrap>() as Bootstrap;
            Facade.RegisterMediator(new MdMenuScene(data));

        }
    }
}
